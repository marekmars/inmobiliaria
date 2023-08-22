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
                TempData["AlertMessage"] = "No se pudo crear el inquilino, Error en la base de datos.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else if (res == -2)
            {
                TempData["AlertMessage"] = "No se pudo crear el inquilino, Ingrese un numero telefonico valido.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else if (res == -3)
            {
                TempData["AlertMessage"] = "No se pudo crear el inquilino, Ingrese un email valido.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else if (res == -4)
            {
                TempData["AlertMessage"] = "No se pudo crear el inquilino, Ingrese un DNI valido.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["AlertMessage"] = "No se pudo modificar el inquilino, Ya existe un inquilino con ese DNI.";
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
                TempData["AlertMessage"] = "No se pudo modificar el inquilino, Error en la base de datos.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else if (res == -2)
            {
                TempData["AlertMessage"] = "No se pudo modificar el inquilino, Ingrese un numero telefonico valido.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else if (res == -3)
            {
                TempData["AlertMessage"] = "No se pudo modificar el inquilino, Ingrese un email valido.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else if (res == -4)
            {
                TempData["AlertMessage"] = "No se pudo modificar el inquilino, Ingrese un DNI valido.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["AlertMessage"] = "No se pudo modificar el inquilino, Ya existe un inquilino con ese DNI.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }
        }
        catch (System.Exception)
        {

            throw;
        }

    }

     public IActionResult Details(int id)
    {
        InquilinosRepository repo = new();
        var inquilino = repo.GetInquilinoById(id);
        return View(inquilino);
    }

    //APIS

    [HttpGet("api/Inquilios/GetInquilino/{dni}")]
    public IActionResult GetInquilino(string dni)
    {
        // Aquí, realiza la lógica para buscar el propietario por DNI
        InquilinosRepository repo = new();
        Inquilino inquilinoEncontrado = repo.GetInquilinoByDni(dni); // Tu lógica de búsqueda

        if (inquilinoEncontrado != null)
        {
            // Retorna el objeto Propietario como JSON
            return Ok(inquilinoEncontrado);
        }
        else
        {
            return NotFound();
        }
    }

}