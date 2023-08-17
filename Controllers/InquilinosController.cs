using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class InquilinosController : Controller
{
    private readonly ILogger<InquilinosController> _logger;

    public InquilinosController(ILogger<InquilinosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        InquilinosRepository repo = new InquilinosRepository();
        List<Inquilino> inquilinos = repo.GetAllInquilinos();

        return View(inquilinos);
    }

    public IActionResult Create()
    {


        return View();
    }
    [HttpPost]
    public IActionResult Create(Inquilino inquilino)
    {
        try
        {
            InquilinosRepository repo = new();
            var res = repo.CreateInquilino(inquilino);
            if (res > 0)
            {

                TempData["AlertMessage"] = "Inquilino creado correctamente.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index");
            }
            else if (res == -1)
            {
                TempData["AlertMessage"] = "No se pudo crear el inquilino.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["AlertMessage"] = "Ya existe un inquilino con ese DNI.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }

        }
        catch (System.Exception)
        {


            throw;
        }
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        try
        {

            InquilinosRepository repo = new();
            repo.DeleteInquilino(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public IActionResult Update(int id)
    {
        InquilinosRepository repo = new();
        var inquilino = repo.GetInquilinoById(id);
        return View(inquilino);
    }


    [HttpPost]
    public IActionResult Update(Inquilino inquilino)
    {
        try
        {
            InquilinosRepository repo = new();
            int res = repo.UpdateInquilino(inquilino);

            if (res > 0)
            {
                TempData["AlertMessage"] = "Inquilino modificado correctamente.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index");
            }
            else if (res == -1)
            {
                TempData["AlertMessage"] = "No se pudo modificar el inquilino.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Update");
            }
            else 
            {
                TempData["AlertMessage"] = "No se pudo modificar, ya existe un inquilino con ese DNI.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Update");
            }
        }
        catch (System.Exception)
        {

            throw;
        }

    }

    public IActionResult Privacy()
    {
        return View();
    }
}