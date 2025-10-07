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
using OyoAgro.DataAccess.Layer.Request;

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

        
        public async Task<TData<List<Userprofile>>> GetList()
        {
            var response = new TData<List<Userprofile>>();
            var obj = await _unitOfWork.Users.GetList();            
            obj = obj.Where(x=> x.Roleid == 2).ToList();
            response.Data = obj;
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
                string message = "";

                var sendMail = EmailHelper.IsPasswordEmailSent(mailParameter, out message);


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
