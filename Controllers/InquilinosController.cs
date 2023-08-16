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
            repo.CreateInquilino(inquilino);
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
        InquilinosRepository repo = new();
        repo.UpdateInquilino(inquilino);   
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}