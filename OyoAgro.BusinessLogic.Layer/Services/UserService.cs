using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OyoAgro.BusinessLogic.Layer.Helpers;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Helpers;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Models.ViewModels;
using OyoAgro.DataAccess.Layer.Request;
using static OyoAgro.DataAccess.Layer.Helpers.EmailHelper;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public string GenerateDefaultPassword()
        {
            return new Random().Next(1, 100000).ToString();
        }

        // Password MD5 processing
        // <param name="password"></param>
        // <param name="salt"></param>
        // <returns></returns>
        public string EncryptUserPassword(string password, string salt)
        {
            string md5Password = SecurityHelper.MD5ToHex(password);
            string encryptPassword = SecurityHelper.MD5ToHex(md5Password.ToLower() + salt).ToLower();
            return encryptPassword;
        }

        // password salt
        // <returns></returns>
        public string GetPasswordSalt()
        {
            return new Random().Next(1, 100000).ToString();
        }

        public async Task<TData<string>> CheckLogin(string userName, string password)
        {
            try
            {
                var _context = new AppDbContext();
                TData<string> response = new TData<string>();

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    response.Message = "Username or password cannot be empty!";
                    response.Tag = 0;
                    return response;
                }

                Useraccount? user = await _unitOfWork.Users.GetUserByUserName(userName);

                if (user == null)
                {
                    response.Message = "Invalid username. Please check your credentials.";
                    response.Tag = 0;
                    return response;
                }

                if (user.Status != (int)StatusEnum.Yes)
                {
                    response.Message = "The account is disabled, please contact the administrator.";
                    response.Tag = 0;
                    return response;
                }
                //if (user.Isactive == true)
                //{
                //    response.Message = "User already active, please logout from other session";
                //    response.Tag = 0;
                //    return response;
                //}

                if (password != EncryptionHelper.Decrypt(user.Passwordhash, user.Salt))
                {
                    response.Message = "Incorrect password. Please try again.";
                    response.Tag = 0;
                    return response;
                }

                string token = await GenerateJwtToken(user);
                user.Logincount++;
                user.Apitoken = token;
                user.Isactive = true;
                //user.Lastlogindate = DateTime.UtcNow.Date;
                _context.Useraccounts.Update(user);
                _context.SaveChanges();

                    response.Message = "You have successfully logged in";
                response.Data = token;
                response.Tag = 1;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<TData<Useraccount>> GetUserByToken(string token)
        {
            try
            {
                var obj = new TData<Useraccount>();
                var user = await _unitOfWork.Users.GetUserByToken(token);
                obj.Data = user;
                obj.Tag = 1;
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public async Task<TData<List<UsersViewModel>>> GetList()
        {
            var response = new TData<List<UsersViewModel>>();
            var obj = await _unitOfWork.Users.GetList();  
            var LGAs = await _unitOfWork.LgaRepository.GetList();
            var farmers = await _unitOfWork.FarmerRepository.GetList();
            var association = await _unitOfWork.AssociationRepository.GetList();
            var farms = await _unitOfWork.FarmRepository.GetList();
            var address = await _unitOfWork.AddressRepository.GetList();
            var res = (from user in obj
                           select new UsersViewModel
                           {
                               Userid = user.Userid,
                               Firstname = user.Firstname,
                               Lastname = user.Lastname,
                               Email = user.Email,
                               Middlename = user.Middlename,
                               Phonenumber = user.Phonenumber,
                               Lga = LGAs.Where(x=> x.Lgaid == user.Lgaid).FirstOrDefault()?.Lganame,
                               Gender = user.Gender,
                               Roleid = user.Roleid,
                               FarmerCount = farmers.Count(x=> x.UserId == user.Userid),
                               Address = address.Where(x => x.Userid == user.Userid).Select(AD => new AddressParam
                               {
                                   Streetaddress = AD.Streetaddress,
                                   Latitude = AD.Latitude,
                                   Longitude = AD.Longitude,
                                   Postalcode = AD.Postalcode,
                                   Town = AD.Town,
                                   Addressid = AD.Addressid,
                                   Lgaid = AD.Lgaid

                               }).FirstOrDefault(),
                               Farmers = farmers.Where(x=> x.UserId == user.Userid).Select(f =>  new FarmerViewModel
                               {
                                   Farmerid = f.Farmerid,
                                   Availablelabor = f.Availablelabor,
                                   Dateofbirth = f.Dateofbirth,
                                   Email = f.Email,
                                   Firstname  = f.Firstname,
                                   Lastname = f.Lastname,
                                   Middlename= f.Middlename,
                                   Association = association.Where(x=> x.Associationid == f.Associationid).FirstOrDefault()?.Name,
                                   Gender = f.Gender,
                                   Householdsize = f.Householdsize,
                                   Lga = LGAs.Where(x => x.Lgaid == user.Lgaid).FirstOrDefault()?.Lganame,
                                   
                                   Farms = farms.Where(x=> x.Farmerid == f.Farmerid).Select(t => new FarmsViewModel
                                   {
                                       Farmid = t.Farmerid,
                                       Farmsize = t.Farmsize,
                                       Streetaddress = address.Where(x=> x.Farmid == t.Farmid).FirstOrDefault()?.Streetaddress,
                                       Latitude = address.Where(x => x.Farmid == t.Farmid).FirstOrDefault()?.Latitude,
                                       Longitude = address.Where(x => x.Farmid == t.Farmid).FirstOrDefault()?.Longitude,
                                       Town = address.Where(x => x.Farmid == t.Farmid).FirstOrDefault()?.Town,
                                   })
                                   .ToList(),
                                   FarmCount = farms.Where(x=> x.Farmerid == f.Farmerid).Count()

                               })
                               .ToList(),
                           })

                           .ToList();

            foreach (var user in res)
            {
                user.FarmCount = user.Farmers.Sum(f => f.Farms.Count);
            }

            response.Data = res;
            response.Total = res.Count();
            return response;
        }


        
        
        public async Task<TData<List<UsersViewModel>>> GetOfficer(int userId)
        {
            var response = new TData<List<UsersViewModel>>();
            var obj = await _unitOfWork.Users.GetList();  
            var LGAs = await _unitOfWork.LgaRepository.GetList();
            var farmers = await _unitOfWork.FarmerRepository.GetList();
            var association = await _unitOfWork.AssociationRepository.GetList();
            var farms = await _unitOfWork.FarmRepository.GetList();
            var address = await _unitOfWork.AddressRepository.GetList();
            var res = (from user in obj.Where(x=> x.Userid == userId)   
                           select new UsersViewModel
                           {
                               Userid = user.Userid,
                               Firstname = user.Firstname,
                               Lastname = user.Lastname,
                               Email = user.Email,
                               Middlename = user.Middlename,
                               Phonenumber = user.Phonenumber,
                               Lga = LGAs.Where(x=> x.Lgaid == user.Lgaid).FirstOrDefault()?.Lganame,
                               Gender = user.Gender,
                               Roleid = user.Roleid,
                               FarmerCount = farmers.Count(x=> x.UserId == user.Userid),
                               Address = address.Where(x=> x.Userid == userId).Select(AD => new AddressParam
                               {
                                   Streetaddress =AD.Streetaddress,
                                   Latitude = AD.Latitude,
                                   Longitude = AD.Longitude,    
                                   Postalcode = AD.Postalcode,
                                   Town = AD.Town,
                                   Addressid = AD.Addressid,
                                   Lgaid = AD.Lgaid

                               }).FirstOrDefault(),
                               Farmers = farmers.Where(x=> x.UserId == userId).Select(f =>  new FarmerViewModel
                               {
                                   Farmerid = f.Farmerid,
                                   Availablelabor = f.Availablelabor,
                                   Dateofbirth = f.Dateofbirth,
                                   Email = f.Email,
                                   Firstname  = f.Firstname,
                                   Lastname = f.Lastname,
                                   Middlename= f.Middlename,
                                   Association = association.Where(x=> x.Associationid == f.Associationid).FirstOrDefault()?.Name,
                                   Gender = f.Gender,
                                   Householdsize = f.Householdsize,
                                   Lga = LGAs.Where(x => x.Lgaid == user.Lgaid).FirstOrDefault()?.Lganame,
                                   
                                   Farms = farms.Where(x=> x.Farmerid == f.Farmerid).Select(t => new FarmsViewModel
                                   {
                                       Farmid = t.Farmerid,
                                       Farmsize = t.Farmsize,
                                       Streetaddress = address.Where(x=> x.Farmid == t.Farmid).FirstOrDefault()?.Streetaddress,
                                       Latitude = address.Where(x => x.Farmid == t.Farmid).FirstOrDefault()?.Latitude,
                                       Longitude = address.Where(x => x.Farmid == t.Farmid).FirstOrDefault()?.Longitude,
                                       Town = address.Where(x => x.Farmid == t.Farmid).FirstOrDefault()?.Town,
                                   })
                                   .ToList(),
                                   FarmCount = farms.Where(x=> x.Farmerid == f.Farmerid).Count()

                               })
                               .ToList(),
                           })

                           .ToList();

            foreach (var user in res)
            {
                user.FarmCount = user.Farmers.Sum(f => f.Farms.Count);
            }

            response.Data = res;
            response.Total = res.Count();
            return response;
        }



        public async Task<TData<Useraccount>> Logout(int userId)
        {
            try
            {
                var context = new AppDbContext();

                var obj = new TData<Useraccount>();
                var userInfo = await context.Useraccounts.Where(x => x.Userid == userId).FirstOrDefaultAsync();
                userInfo.Isactive = false;
                userInfo.Apitoken = null;
                context.Useraccounts.Update(userInfo);
                context.SaveChanges();

                obj.Tag = 1;
                obj.Message = "Logout successfully";
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
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
                var expiresAt = DateTime.UtcNow.AddHours(24);

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
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var bytes = new byte[64];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
            }
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
                    ResetLink = resetLink
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

        public async Task<TData<string>> SaveForm(UserParam entity)
        {
            try
            {
                var obj = new TData<string>();
                if (string.IsNullOrEmpty(entity.EmailAddress))
                {
                    obj.Message = "Email must be provided!";
                    obj.Tag = 0;
                    return obj;
                }
                if (string.IsNullOrEmpty(entity.Phonenumber))
                {
                    obj.Message = "Mobile Number must be provided!";
                    obj.Tag = 0;
                    return obj;
                }
                if (string.IsNullOrEmpty(entity.Firstname))
                {
                    obj.Message = "First Name must be provided!";
                    obj.Tag = 0;
                    return obj;
                }
                if (string.IsNullOrEmpty(entity.Lastname))
                {
                    obj.Message = "Last Name must be provided!";
                    obj.Tag = 0;
                    return obj;
                }

                if (string.IsNullOrEmpty(entity.Streetaddress))
                {
                    obj.Message = "Street Address must be provided!";
                    obj.Tag = 0;
                    return obj;
                }
                if (string.IsNullOrEmpty(entity.Town))
                {
                    obj.Message = "Town must be provided!";
                    obj.Tag = 0;
                    return obj;
                }
                if (string.IsNullOrEmpty(entity.Postalcode))
                {
                    obj.Message = "Postal Code must be provided!";
                    obj.Tag = 0;
                    return obj;
                }

                if (entity.Latitude == 0)
                {
                    obj.Message = "Latitude must be provided!";
                    obj.Tag = 0;
                    return obj;
                }
                if (entity.Longitude == 0)
                {
                    obj.Message = "Longitude must be provided!";
                    obj.Tag = 0;
                    return obj;
                }


                if (entity.Lgaid == 0)
                {
                    obj.Message = "LGA must be provided!";
                    obj.Tag = 0;
                    return obj;
                }

                if (entity.Regionid == 0)
                {
                    obj.Message = "Region must be provided!";
                    obj.Tag = 0;
                    return obj;
                }


                if (entity.Phonenumber.Length != 11)
                {
                    obj.Message = "Mobile Number must be 11 digits long!";
                    obj.Tag = 0;
                    return obj;
                }


                var UserExist = await _unitOfWork.Users.GetUserByUserName(entity.EmailAddress);
                if (UserExist != null)
                {
                    obj.Message = "Email already exists!";
                    obj.Tag = 0;
                    return obj;
                }
                var MobileExists = await _unitOfWork.UserProfile.GetUserByPhone(entity.Phonenumber);
                if (MobileExists != null)
                {
                    obj.Message = "Mobile No already exists!";
                    obj.Tag = 0;
                    return obj;
                }

                var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(entity.EmailAddress, emailPattern))
                {
                    obj.Message = "Please provide a valid Email Address!";
                    obj.Tag = 0;
                    return obj;
                }

                entity.Salt = new UserService(_unitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_unitOfWork).GenerateDefaultPassword();
                entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);

                var UserEntity = new Useraccount
                {
                    Email = entity.EmailAddress,
                    DecryptedPassword = entity.DecryptedPassword,
                    Salt = entity.Salt,
                    Passwordhash = entity.Password,
                    Lgaid = entity.Lgaid,
                    Username = entity.UserName,
                    Status = 1
                };
                await _unitOfWork.Users.SaveForm(UserEntity);
                var userInfp = await _unitOfWork.Users.GetUserById(UserEntity.Userid);
                var userprofile = new Userprofile
                {
                    Userid = userInfp.Userid,
                    Firstname = entity.Firstname,
                    Lastname = entity.Lastname,
                    Phonenumber = entity.Phonenumber,
                    Roleid = (int)RoleEnum.User,
                    Lgaid = entity.Lgaid,
                    Middlename = entity.Middlename,
                    Email = entity.EmailAddress,
                    Version = 1,
                };
                await _unitOfWork.UserProfile.SaveForm(userprofile);
                var userAddress = new Address
                {
                    Userid = userInfp.Userid,
                    Streetaddress = entity.Streetaddress,
                    Longitude = entity.Longitude,
                    Latitude = entity.Latitude,
                    Postalcode = entity.Postalcode,
                    Lgaid = entity.Lgaid
                };
                await _unitOfWork.AddressRepository.SaveForm(userAddress);
                var region = new Userregion
                {
                    Userid = userInfp.Userid,
                    Regionid = Convert.ToInt32(entity.Regionid),

                };

                await _unitOfWork.UserRegionRepository.SaveForm(region);

                MailParameter mailParameter = new()
                {
                    UserEmail = userprofile.Email,
                    RealName = entity.Firstname + " " + entity.Lastname,
                    UserName = entity.EmailAddress,
                    UserPassword = entity.DecryptedPassword,
                    UserCompany = GlobalConstant.COMPANY
                };

                var sendMail = await EmailHelper.IsPasswordEmailSent(mailParameter);

                if (sendMail.Success)
                {
                    Console.WriteLine("✅ Email sent successfully!");
                }
                else
                {
                    Console.WriteLine($"❌ Failed to send email: {sendMail.Message}");
                }


                obj.Data = UserEntity.Userid.ToString();
                obj.Message = "User Created Successfully";
                obj.Tag = 1;
                return obj;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        private async Task<string> GenerateJwtToken(Useraccount toLogIn)
        {

            var userProfile = await _unitOfWork.UserProfile.GetUserProfile(toLogIn.Userid);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SystemConfig.JWTSecret!.Key);
            var param = new ProfileadditionalactivityParam();
            param.UserId = toLogIn.Userid;
            var userActivities = await _unitOfWork.ProfileAdditionalActivities.GetList(param);
            var userActivitiesString = string.Join(",", userActivities);
            var userrole = await _unitOfWork.UserProfile.GetUserProfile(param.UserId);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("UserId", toLogIn.Userid.ToString()),
                        new Claim("UserName", toLogIn.Username!.ToString()),
                        new Claim("UserStatus", toLogIn.Status.ToString()),
                        new Claim("userActivities", userActivitiesString),
                        new Claim("role", userrole.Roleid.ToString()),
                        new Claim("fullname", userProfile.Firstname + " " + userProfile.Lastname),
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
