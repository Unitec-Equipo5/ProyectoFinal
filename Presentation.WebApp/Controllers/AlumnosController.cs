using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class AlumnosController : Controller
{
      private readonly AlumnosDbContext _alumnosDbContext;
      public AlumnosController(IConfiguration configuration)
      {
            _alumnosDbContext = new AlumnosDbContext(configuration.GetConnectionString("DefaultConnection"));
      }

      public IActionResult Index()
      {
            var data = _alumnosDbContext.List();
            return View(data);
      }

      public IActionResult Details(Guid id)
      {
            var data = _alumnosDbContext.Details(id);
            return View(data);
      }

      public IActionResult Create()
      {
            return View();
      }
      [HttpPost]
      public IActionResult Create(Alumno data)
      {
            data.Id = Guid.NewGuid();
            _alumnosDbContext.Create(data);
            return RedirectToAction("Index");
      }

      public IActionResult Edit(Guid id)
      {
            var data = _alumnosDbContext.Details(id);
            return View(data);
      }
      [HttpPost]
      public IActionResult Edit(Alumno data)
      {
            _alumnosDbContext.Edit(data);
            return RedirectToAction("Index");
      }

      public IActionResult Delete(Guid id)
      {
            _alumnosDbContext.Delete(id);
            return RedirectToAction("Index");
      }
}