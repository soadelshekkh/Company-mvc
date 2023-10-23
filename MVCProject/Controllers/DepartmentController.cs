using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IdepartmentRepositary departmentRepositary;
        public DepartmentController(IdepartmentRepositary departmentRepositary)
        {
            this.departmentRepositary = departmentRepositary; 
        }
        public IActionResult Index()
        {
            var department = departmentRepositary.GetAll();
            return View(department);
        }
        public IActionResult Details(int? id, string ViewName = "Details") 
        {
            if(id== null)
                return NotFound();
            var department = departmentRepositary.GetById(id.Value);
            if (department == null)
                return NotFound();
            return View(ViewName,department);
        }
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
            //if (id == null)
            //    return NotFound();
            //var department = departmentRepositary.GetById(id.Value);
            //if(department ==null)
            //    return NotFound();
            //return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int id,Department department)
        {
            if (id != department.Dnum)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    departmentRepositary.updateDepartment(department);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department.Dnum);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id,Department department)
        {
            if (id != department.Dnum)
                return BadRequest();
                try { 
                        departmentRepositary.deleteDepartment(department);
                        return RedirectToAction(nameof(Index));
                     }
                catch(System.Exception ex) 
                    { 
                        return View(department); 
                    }
            
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepositary.AddDeparment(department);
                TempData["Message"] = "Department is created";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(department);
            }
            
        }
    }
}
