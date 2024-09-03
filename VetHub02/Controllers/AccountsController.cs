using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Servises;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Errors;
using VetHub02.Helpers;

namespace VetHub02.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService token;
        private readonly IMapper mapper;
        private readonly IMailSettings mailSettings;
       
        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager 
                               , IUnitOfWork unitOfWork, ITokenService token, IMapper mapper, IMailSettings mailSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;           
            this.unitOfWork = unitOfWork;
            this.token = token;
            this.mapper = mapper;
            this.mailSettings = mailSettings;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromForm]LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null) return Unauthorized(new ApiError(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            
            if (!result.Succeeded) { return Unauthorized(new ApiResponse { Message = "Password InCorrect"}); }
           
            
            
           
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Name = user.UserName,
                Email = user.Email,
                Token = await token.CreateTokenAsnyc(user, userManager)
            });

        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register([FromForm] RegisterDto model)
        {   
            if(await userManager.FindByEmailAsync(model.Email) is not null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { Message = "User Already Exists" });

            }
            
            var user = new AppUser()
            {
                DisplayName = $"{model.Name}",
                UserName = model.Email.Split('@')[0],
                Email = model.Email,
                PhoneNumber = model.Phone
            };
            var tokenUserForEmail = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("ConfirmEmail", "Accounts", new { EmailUser = model.Email, EmailToken = tokenUserForEmail }, Request.Scheme);
            user.Code = tokenUserForEmail;
            var email = new MailSettingsModel()
            {
                Subject = "Confirm Email ",
                To = model.Email,
                Body = $"this is link for Confirm your Email :  {link}"
            };
            if (model.Email is not null)
                mailSettings.sendMail(email);

            var result = await userManager.CreateAsync(user, model.Password);  // appuser 

            if (!result.Succeeded) return BadRequest(new ApiError(400));
            if (result.Succeeded)
            {
                var mappedUser = mapper.Map<User>(model);
                unitOfWork.Repository<User>().Add(mappedUser);
                unitOfWork.Complete();
            }
            return Ok("Successfully Register");
        }

     
        [HttpPost("ToConfirmEmail")]
        public async Task<ActionResult> ToConfirmEmail(string Email)
        {
            if (Email is not null)
            {
                var user = await userManager.FindByEmailAsync(Email);
                var tokenUserForEmail = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action("ConfirmEmail", "Accounts", new { EmailUser = Email, EmailToken = tokenUserForEmail }, Request.Scheme);
                user.Code = tokenUserForEmail;
                var email = new MailSettingsModel()
                {
                    Subject = "Confirm Email ",
                    To = Email,
                    Body = $"this is link for Confirm your Email :  {link}"
                };
                if (Email is not null)
                    mailSettings.sendMail(email);

            }
            return Ok("please,Check your Email. ");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string EmailUser, string EmailToken)
        {
            if (EmailUser == null || EmailToken == null) BadRequest();
            if (EmailUser is not null) 
            {
                var user = await userManager.FindByEmailAsync(EmailUser);

                await userManager.ConfirmEmailAsync(user, EmailToken);
                user.Code = "";
                await userManager.UpdateAsync(user);
                
            }
            return Ok("Seccessfully Confirmed Email");
        }

    
        [HttpPost("ToChangePassword")]
        public async Task<ActionResult> ToChangePassword([FromForm] string Email)
        {
            if (Email is not null)
            {
                var user = await userManager.FindByEmailAsync(Email);
                user.Code = GetCodeForConfirm().ToString();

                var email = new MailSettingsModel()
                {
                    Subject = "Confirm Email ",
                    To = Email,
                    Body = $"this is code to reset your Password :  { user.Code}"
                };
                if (Email is not null)
                    mailSettings.sendMail(email);

            }
            
            return Ok("please,Check your Email. ");
        }
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromForm] string code , string password , string newPassword , string email)
        {
            if (email is not null)
            {
              var user =   await userManager.FindByEmailAsync (email);
                if (code ==  user.Code)
                {
                  var result =  await userManager.ChangePasswordAsync(user,password,newPassword);
                    if (!result.Succeeded) return BadRequest(new ApiError(400));
                    if (result.Succeeded)
                    {
                        await userManager.UpdateAsync(user);
                        user.Code = null;
                        return Ok("Password Seccussfully Changed ");
                    }
                    

                }
            }
            if (email is null) return BadRequest();
            if (password == null) return BadRequest();
            if (newPassword == null) return BadRequest();
           

            return Ok("End "); ;
        }

    
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string email)
        {
            var userexist = await userManager.FindByEmailAsync(email);
            if (userexist is not null)
            {
                await userManager.DeleteAsync(userexist);
            }
            return Ok("deleted");
        }

        [HttpPost("TorestPassword")]
        public async Task<ActionResult> TorestPassword([FromForm] string Email)
        {
            if (Email is not null)
            {
                var user = await userManager.FindByEmailAsync(Email);

                user.Code = await userManager.GeneratePasswordResetTokenAsync(user);
                await userManager.UpdateAsync(user);
                //  user.Code = Url.Action("restpassword", "Accounts", new { EmailUser = Email, resetPasswordToken = tokenResetPassword }, Request.Scheme);
                //  user.Code = GetCodeForConfirm().ToString();

                var email = new MailSettingsModel()
                {
                    Subject = "Reset Password ",
                    To = Email,
                    Body = $"This is code to reset your Password :  {user.Code}"
                };
                if (Email is not null)
                    mailSettings.sendMail(email);

            }

            return Ok("Please,Check your Email. ");
        }


        [HttpPost("restPassword")]
        public async Task<ActionResult> restPassword([FromForm] ResetPasswordModel model)
        {
            if (model.Email is not null)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (model.Token == user.Code)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (!result.Succeeded) return BadRequest(new ApiError(400));
                    if (result.Succeeded)
                    {
                        await userManager.UpdateAsync(user);

                        user.Code = null;
                        return Ok("Password Seccussfully reset ");
                    }
                }
            }
            if (model.Email is null) return BadRequest();
            if (model.Password == null) return BadRequest();


            return Ok("End reset password"); ;
        }

       

        [HttpGet]
        public int GetCodeForConfirm()
        {
            Random rand = new Random();
            return rand.Next(1000, 1000000);
        }


        [HttpPost("GetUserCodeByEmail")]
        public async Task<ActionResult> GetUserCodeByEmail([FromQuery]string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
            var user = await userManager.FindByEmailAsync(email);
                if(user.Code is not null)
                      return Ok(user.Code);
            }



            return BadRequest();

        }



    }
}
