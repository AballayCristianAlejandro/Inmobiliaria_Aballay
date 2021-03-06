using Inmobiliaria_Aballay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_Aballay.Controllers
{
    public class PropietariosController : Controller
    {
        RepositorioPropietario repositorio;

        public PropietariosController()
        {
            repositorio = new RepositorioPropietario();
        }

        // GET: PropietariosController
        public ActionResult Index()
        {
            try
            {
                var lista = repositorio.ObtenerTodos();
                ViewBag.Id = TempData["Id"];
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];
                return View(lista);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        // GET: PropietariosController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: PropietariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropietariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropietariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropietariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropietariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropietariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
