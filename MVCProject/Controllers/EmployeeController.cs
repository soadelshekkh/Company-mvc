using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Helper;
using MVCProject.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVCProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepositry EmployeeRepositary;

        private readonly IdepartmentRepositary DepartmentRepositary;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepositry EmployeeRepositary, IdepartmentRepositary departmentRepositary, IMapper _mapper)
        {
            this.EmployeeRepositary = EmployeeRepositary;
            this.DepartmentRepositary = departmentRepositary;
            this._mapper = _mapper;
        }
        public IActionResult Index(string Name)
        {
            var employee = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(Name))
                employee = EmployeeRepositary.GetAll();
            else
                employee = EmployeeRepositary.GetEmployeeByEmployeeName(Name);
            var mappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
            return View(mappedEmployee);
        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var Employee = EmployeeRepositary.GetById(id.Value);
            if (Employee == null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(ViewName, MappedEmployee);
        }
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, EmployeeViewModel Employee)
        {
            if (id != Employee.Id)
                return BadRequest();
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(Employee);
            if (ModelState.IsValid)
            {
                try
                {
                    EmployeeRepositary.update(MappedEmployee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Employee.Id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete( EmployeeViewModel Employee)
        {
            //if (id != Employee.Id)
            //    return BadRequest();
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(Employee);
            try
            {
                int count = EmployeeRepositary.delete(MappedEmployee);
                if (count>0)
                {
                    DocumentSetting.DeleteFile("Files", Employee.ImageName);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return View(Employee);
            }

        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.department = DepartmentRepositary.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel Employee)
        {
            if (ModelState.IsValid)
            {
                Employee.ImageName = DocumentSetting.UploadFile(Employee.Image, "imges");
                var Emp = _mapper.Map<EmployeeViewModel, Employee>(Employee);
                EmployeeRepositary.Add(Emp);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.department = DepartmentRepositary.GetAll();
                ViewBag.Errors = ModelState.Values.Where(v => v.Errors.Count > 0);
                return View(Employee);
            }

        }
    }
}
