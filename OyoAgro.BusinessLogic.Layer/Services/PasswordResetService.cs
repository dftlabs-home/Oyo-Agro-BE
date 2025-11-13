using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Helpers;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Request;
using static OyoAgro.DataAccess.Layer.Helpers.EmailHelper;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private const int TOKEN_EXPIRY_HOURS = 24;
        private const int TOKEN_LENGTH = 64;

        public PasswordResetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TData<string>> ForgotPassword(ForgotPasswordParam param)
        {
            try
            {
                var response = new TData<string>();

                // Validate email format
                if (string.IsNullOrWhiteSpace(param.Email))
                {
                    response.Message = "Email address is required.";
                    response.Tag = 0;
                    return response;
                }

                // Find user by email
                var user = await _unitOfWork.Users.GetUserByEmail(param.Email);
                if (user == null)
                {
                    // For security, don't reveal if email exists or not
                    response.Message = "If the email address exists in our system, you will receive a password reset link.";
                    response.Tag = 1;
                    return response;
                }

                // Check if user is active
                if (user.Status != (int)StatusEnum.Yes)
                {
                    response.Message = "Account is disabled. Please contact administrator.";
                    response.Tag = 0;
                    return response;
                }

                // Generate secure token
                var token = GenerateSecureToken();
                var expiresAt = DateTime.UtcNow.AddHours(TOKEN_EXPIRY_HOURS);

                // Invalidate any existing tokens for this user
                await InvalidateExistingTokens(user.Userid);

                // Store token in database
                var resetToken = new PasswordResetToken
                {
                    UserId = user.Userid,
                    Token = token,
                    ExpiresAt = expiresAt,
                    IsUsed = false,
                    Createdat = DateTime.UtcNow
                };

                await _unitOfWork.PasswordResetTokens.SaveForm(resetToken);

                // Update user with token info (for quick lookup)
                user.PasswordResetToken = token;
                user.PasswordResetTokenExpires = expiresAt;
                await _unitOfWork.Users.UpdateUser(user);

                // Send email
                var emailResult = await SendPasswordResetEmail(user, token);
                if (!emailResult.Success)
                {
                    response.Message = "Failed to send password reset email. Please try again.";
                    response.Tag = 0;
                    return response;
                }

                response.Message = "If the email address exists in our system, you will receive a password reset link.";
                response.Tag = 1;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing forgot password request: {ex.Message}", ex);
            }
        }

        public async Task<TData<string>> ResetPassword(ResetPasswordParam param)
        {
            try
            {
                var response = new TData<string>();

                // Validate input
                if (string.IsNullOrWhiteSpace(param.Token))
                {
                    response.Message = "Reset token is required.";
                    response.Tag = 0;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(param.NewPassword))
                {
                    response.Message = "New password is required.";
                    response.Tag = 0;
                    return response;
                }

                if (param.NewPassword != param.ConfirmPassword)
                {
                    response.Message = "Passwords do not match.";
                    response.Tag = 0;
                    return response;
                }

                if (param.NewPassword.Length < 6)
                {
                    response.Message = "Password must be at least 6 characters long.";
                    response.Tag = 0;
                    return response;
                }

                // Find valid token
                var resetToken = await _unitOfWork.PasswordResetTokens.GetValidToken(param.Token);
                if (resetToken == null)
                {
                    response.Message = "Invalid or expired reset token.";
                    response.Tag = 0;
                    return response;
                }

                // Get user
                var user = await _unitOfWork.Users.GetUserById(resetToken.UserId);
                if (user == null)
                {
                    response.Message = "User not found.";
                    response.Tag = 0;
                    return response;
                }

                // Check if user is active
                if (user.Status != (int)StatusEnum.Yes)
                {
                    response.Message = "Account is disabled. Please contact administrator.";
                    response.Tag = 0;
                    return response;
                }

                // Generate new salt and hash password
                var salt = GetPasswordSalt();
                var hashedPassword = EncryptUserPassword(param.NewPassword, salt);

                // Update user password
                user.Salt = salt;
                user.Passwordhash = hashedPassword;
                user.LastPasswordReset = DateTime.UtcNow;
                user.PasswordResetToken = null;
                user.PasswordResetTokenExpires = null;

                // Mark token as used
                resetToken.IsUsed = true;
                resetToken.UsedAt = DateTime.UtcNow;

                // Save changes
                await _unitOfWork.Users.UpdateUser(user);
                await _unitOfWork.PasswordResetTokens.UpdateToken(resetToken);

                // Invalidate all other tokens for this user
                await InvalidateExistingTokens(user.Userid, resetToken.Id);

                response.Message = "Password has been reset successfully. You can now login with your new password.";
                response.Tag = 1;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error resetting password: {ex.Message}", ex);
            }
        }

        public async Task<TData<bool>> ValidateResetToken(string token)
        {
            try
            {
                var response = new TData<bool>();

                if (string.IsNullOrWhiteSpace(token))
                {
                    response.Message = "Token is required.";
                    response.Tag = 0;
                    response.Data = false;
                    return response;
                }

                var resetToken = await _unitOfWork.PasswordResetTokens.GetValidToken(token);
                if (resetToken == null)
                {
                    response.Message = "Invalid or expired reset token.";
                    response.Tag = 0;
                    response.Data = false;
                    return response;
                }

                response.Message = "Token is valid.";
                response.Tag = 1;
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating reset token: {ex.Message}", ex);
            }
        }

        private string GenerateSecureToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[TOKEN_LENGTH];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
            }
        }

        private string GetPasswordSalt()
        {
            return new Random().Next(1, 100000).ToString();
        }

        private string EncryptUserPassword(string password, string salt)
        {
            string md5Password = SecurityHelper.MD5ToHex(password);
            string encryptPassword = SecurityHelper.MD5ToHex(md5Password.ToLower() + salt).ToLower();
            return encryptPassword;
        }

        private async Task InvalidateExistingTokens(int userId, int? excludeTokenId = null)
        {
            var existingTokens = await _unitOfWork.PasswordResetTokens.GetUserTokens(userId);
            foreach (var token in existingTokens)
            {
                if (excludeTokenId == null || token.Id != excludeTokenId)
                {
                    token.IsUsed = true;
                    token.UsedAt = DateTime.UtcNow;
                    await _unitOfWork.PasswordResetTokens.UpdateToken(token);
                }
            }
        }

        private async Task<EmailResult> SendPasswordResetEmail(Useraccount user, string token)
        {
            try
            {
                var userProfile = await _unitOfWork.UserProfile.GetUserProfile(user.Userid);
                var resetLink = $"{GlobalConstant.API_BASE_URL}/reset-password?token={token}";

                var mailParameter = new MailParameter
                {
                    UserEmail = user.Email!,
                    RealName = $"{userProfile?.Firstname} {userProfile?.Lastname}".Trim(),
                    UserName = user.Username,
                    UserPassword = "", // Not needed for reset email
                    UserCompany = GlobalConstant.COMPANY,
                    ResetLink = resetLink,
                    UserToken = token
                };

                return await EmailHelper.SendPasswordResetEmail(mailParameter);
            }
            catch (Exception ex)
            {
                return new EmailResult
                {
                    Success = false,
                    Message = $"Failed to send password reset email: {ex.Message}"
                };
            }
        }
    }
}
