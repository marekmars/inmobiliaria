using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;

    public PropietariosController(ILogger<PropietariosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        PropietariosRepository repo = new();
        List<Propietario> Propietarios = repo.GetAllPropietarios();

        return View(Propietarios);
    }
    public IActionResult Create()
    {


        return View();
    }
    [HttpPost]
    public IActionResult Create(Propietario propietario)
    {
        try
        {
            PropietariosRepository repo = new();
            var res = repo.CreatePropietario(propietario);
            if (res > 0)
            {

                TempData["AlertMessage"] = "Propietario creado correctamente.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index");
            }
            else if (res == -1)
            {
                TempData["AlertMessage"] = "No se pudo crear el propietario.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["AlertMessage"] = "Ya existe un propietario con ese DNI.";
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
            PropietariosRepository repo = new();
            repo.DeletePropietario(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public IActionResult Update(int id)
    {
        PropietariosRepository repo = new();
        var propietario = repo.GetPropietarioById(id);
        return View(propietario);
    }

    [HttpPost]
    public IActionResult Update(Propietario propietario)
    {
        PropietariosRepository repo = new();
        int res = repo.UpdatePropietario(propietario);
        if (res > 0)
        {

            TempData["AlertMessage"] = "Propietario modificado correctamente.";
            TempData["AlertType"] = "success";
            return RedirectToAction("Index");
        }
        else if (res == -1)
        {
            TempData["AlertMessage"] = "No se pudo modificar el propietario.";
            TempData["AlertType"] = "error";
            return RedirectToAction("Update");
        }
        else
        {
            TempData["AlertMessage"] = "Ya existe un modificar con ese DNI.";
            TempData["AlertType"] = "error";
            return RedirectToAction("Update");
        }
    }


    public IActionResult Privacy()
    {
        return View();
    }

}