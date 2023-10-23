using AutoMapper;
using BLL.Repositaries;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Helper;
using MVCProject.Models;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<UserRegister> userManager;
        private readonly IMapper mapper;
        public UserController(UserManager<UserRegister> userManager, IMapper mapper)
		{
			this.userManager = userManager;
            this.mapper = mapper;
		}
        public async Task<IActionResult> Index(string email)
        {
            var user = Enumerable.Empty<UserRegister>().ToList();
            if (string.IsNullOrEmpty(email))
                user.AddRange( userManager.Users);
            else
                user.Add(await userManager.FindByEmailAsync(email));
            return View(user);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            
            return View(ViewName, user);
        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id, UserRegister User)
        {
            if (id != User.Id) 
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Orginuser =await userManager.FindByIdAsync(User.Id);
                    Orginuser.PhoneNumber = User.PhoneNumber;
                    Orginuser.Email = User.Email;
                    Orginuser.SecurityStamp = User.SecurityStamp;
                    await userManager.UpdateAsync(Orginuser);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(User.Id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(UserRegister User)
        {
            try
            {
                var user = await userManager.FindByIdAsync(User.Id);
                await userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex) 
            {
                return View(User); 
            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewBag.department = DepartmentRepositary.GetAll();
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(RegisterViewModel User)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mappeData = mapper.Map<RegisterViewModel, userre>(User);
        //        EmployeeRepositary.Add(Emp);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        ViewBag.department = DepartmentRepositary.GetAll();
        //        ViewBag.Errors = ModelState.Values.Where(v => v.Errors.Count > 0);
        //        return View(Employee);
        //    }

        //}
    }
}
