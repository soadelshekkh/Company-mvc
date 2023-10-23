using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Helper;
using MVCProject.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace MVCProject.Controllers
{
    public class AcountController : Controller 
    {
        private readonly UserManager<UserRegister> userManager;
        private readonly SignInManager<UserRegister> signInManager;
        private readonly IMapper _mapper;
        public AcountController(UserManager<UserRegister> userManager, SignInManager<UserRegister> signInManager, IMapper _mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._mapper = _mapper;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel Registerdata)
        {
            if (ModelState.IsValid)
            {
                var mappedRegisterData = _mapper.Map<RegisterViewModel, UserRegister>(Registerdata);
                var result = await userManager.CreateAsync(mappedRegisterData,Registerdata.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(Registerdata);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel LoginData)
        {
            if(ModelState.IsValid)
            {
                var UserCheckMail = await userManager.FindByEmailAsync(LoginData.Email);
                if(UserCheckMail != null)
                {
                    bool flag =  await userManager.CheckPasswordAsync(UserCheckMail, LoginData.PassWord);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(UserCheckMail, LoginData.PassWord, LoginData.RemmeberMe, false);
                        if(result.Succeeded)
                        { return RedirectToAction( "Index","Home"); }
                    }
                    ModelState.AddModelError(string.Empty, "Password not correct");
                }
                ModelState.AddModelError(string.Empty, "This Mail not found");
 
            }
            return View();
        }
        public new async Task<IActionResult> Singout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(ForgetPasswordViewModel Model)
        {
            if(ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(Model.Email);
                if(User != null)
                {
                    var TokenResetPassword = await userManager.GeneratePasswordResetTokenAsync(User); 
                    var ResetPasswordLink = Url.Action("ResetPassWord", "Acount",new {Email =Model.Email , Token  = TokenResetPassword },Request.Scheme) ;
                    Email email = new Email {
                        To = Model.Email,
                        Subject = "Reset password",
                        Body = "reset Password from hire ( " + ResetPasswordLink+ " )"
                    };
                    EmailSetting.SendMail(email);
                    return RedirectToAction("checkYourInbox");
                }
                ModelState.AddModelError(string.Empty, "This Email Not found");
            }
            ModelState.AddModelError(string.Empty, "Please Enter Valid Mail");
            return View(Model);
        }
        public IActionResult checkYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassWord(string Email, string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassWord(ResetPassordVieModel Model)
        {
            if(ModelState.IsValid)
            {
                string mail = TempData["Email"] as string;
                string Token = TempData["Token"] as string;
                var user = await userManager.FindByEmailAsync(mail);
                if(user !=null)
                {
                    var result =  await userManager.ResetPasswordAsync(user, Token, Model.Password);
                    if(result.Succeeded)
                    return RedirectToAction("Login");
                    foreach(var Error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,Error.Description);
                    }
                }
                ModelState.AddModelError(string.Empty, "Not Valid mail");

            }
            return View(Model);
        }
    }
}
