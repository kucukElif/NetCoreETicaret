using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.ViewModels;

namespace MVC.Controllers
{
    public class MemberController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserVM appUserVM)
        {
            //Todo: Gender yapılacak.
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = appUserVM.UserName,
                    FirstName = appUserVM.FirstName,
                    LastName = appUserVM.LastName,
                    Email = appUserVM.Email,
                    Gender = appUserVM.Gender,
                    City = appUserVM.City
                };
              var result= await userManager.CreateAsync(user, appUserVM.Password);
                
                if (result.Succeeded)
                {
                    //Todo: Kayıt olan herbir kullanıcının rolü "member" olarak atanacak.
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginVM loginVM)//kullanıcı adı ve şifre
        {
            if (ModelState.IsValid)
            {
                AppUser user =await userManager.FindByNameAsync(loginVM.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
                    if (result.Succeeded)
                    {
                        //Todo: Member isimli Area içerisinde bulunan Home/Index'e yönlendirilecek.
                        return Redirect("/Home/Index");
                    }
                }
            }
            return View();
        }

        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}