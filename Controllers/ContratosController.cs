using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class ContratosController : Controller
{
    private readonly ILogger<ContratosController> _logger;

    public ContratosController(ILogger<ContratosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ContratosRepository repo = new();
        List<Contrato> contratos = repo.GetAllContratos();
        return View(contratos);
    }
    public IActionResult Create()
    {
        return View();
    }



    [HttpPost]
    public IActionResult Create(Contrato contrato)
    {
        try
        {
            ContratosRepository repo = new();
            var res = repo.CreateContrato(contrato);
            if (res > 0)
            {

                TempData["AlertMessage"] = "Contrato creado correctamente.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index");
            }
            else if(res == -1)
            {
                TempData["AlertMessage"] = "No se pudo crear el Contrato, debido a un error en los datos ingresados";
                TempData["AlertType"] = "error";
                return RedirectToAction("Create");
            }else{
                 TempData["AlertMessage"] = "No se pudo crear el Contrato, Ya existe uno para ese inmueble en la fecha seleccionada.";
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
            ContratosRepository repo = new();
            repo.DeleteContrato(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public IActionResult Update(int id)
    {
        ContratosRepository repo = new();
        var contrato = repo.GetContratoById(id);
        return View(contrato);
    }

    [HttpPost]
    public IActionResult Update(Contrato contrato)
    {
        ContratosRepository repo = new();
        int res = repo.UpdateContrato(contrato);
        if (res > 0)
            {

                TempData["AlertMessage"] = "Contrato modificado correctamente.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index");
            }
            else if(res == -1)
            {
                TempData["AlertMessage"] = "No se pudo modificar el Contrato, debido a un error en los datos ingresados";
                TempData["AlertType"] = "error";
                return RedirectToAction("Update");
            }else{
                 TempData["AlertMessage"] = "No se pudo modificar el Contrato, Ya existe uno para ese inmueble en la fecha seleccionada.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Update");
            }

    }

    public IActionResult Details(int id)
    {
        ContratosRepository repo = new();
        var contrato = repo.GetContratoById(id);
        return View(contrato);
    }

 [HttpGet("api/Contratos/GetContratos")]
    public IActionResult GetAllContratos()
    {
        // Aquí, realiza la lógica para obtener todos los contratos
        ContratosRepository repo = new();
        List<Contrato> contrato = repo.GetAllContratos(); // Tu lógica para obtener todos los contratos

        if (contrato.Count > 0)
        {
            // Retorna la lista de contrats como JSON
            return Ok(contrato);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("Contratos/FiltrarContratos")]
    public IActionResult FiltrarContratos([FromQuery] string searchTerm)
    {

        IActionResult result = GetAllContratos(); // Obtener el resultado

        if (result is OkObjectResult okObjectResult)
        {
            var contratosResponse = okObjectResult.Value; // Obtener el contenido del resultado
            if (contratosResponse is List<Contrato> contratos)
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return Json(contratos); 
                }

            
                var contratosFiltrados = contratos.Where(c =>
                    c.Inquilino.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    c.Inquilino.Apellido.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    c.Inquilino.Dni.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)||
                    c.Inmueble.Direccion.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)||
                    c.MontoMensual.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)


                ).ToList();

                return Json(contratosFiltrados);
            }
        }

        // Si algo salió mal o el tipo no coincide, regresa un error u otra respuesta
        return BadRequest("Error al obtener los contratos");
    }

}