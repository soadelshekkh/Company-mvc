using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager; 
        }
        public async Task<IActionResult> Index(string Name)
        {
            var roles = Enumerable.Empty<IdentityRole>().ToList();
            if (string.IsNullOrEmpty(Name))
                roles.AddRange(roleManager.Roles);
            else
                roles.Add(await roleManager.FindByNameAsync(Name));
            return View(roles);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(ViewName, role);
        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id,IdentityRole  role)
        {
            if (id != role.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var OrginRole = await roleManager.FindByIdAsync(role.Id);
                    OrginRole.Name = role.Name;
                    await roleManager.UpdateAsync(OrginRole);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(role.Id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IdentityRole role)
        {
            try
            {
                var Role = await roleManager.FindByIdAsync(role.Id);
                await roleManager.DeleteAsync(Role);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return View(role);
            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpGet]
        public IActionResult Create()
        {
        
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                await roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
                return View(role);
        }
    }
}
