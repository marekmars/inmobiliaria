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
            repo.CreatePropietario(propietario);
            return RedirectToAction("Index");
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
        var inquilino = repo.GetPropietarioById(id);   
        return View(inquilino);
    }

    public IActionResult Privacy()
    {
        return View();
    }

}