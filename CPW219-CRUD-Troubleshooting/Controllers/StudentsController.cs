﻿using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(p, context);
                ViewData["Message"] = $"{p.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student p = StudentDb.GetStudent(context, id);

            //show it on web page
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);
                TempData["Message"] = "Student Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);

            StudentDb.Delete(context, p);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Student studentDetails = StudentDb.GetStudent(context, id);
            if (studentDetails == null)
            {
                return NotFound();
            }
            return View(studentDetails);
        }
    }
}
