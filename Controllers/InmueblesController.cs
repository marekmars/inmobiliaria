using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class InmueblesController : Controller
{
    private readonly ILogger<InmueblesController> _logger;

    public InmueblesController(ILogger<InmueblesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        InmueblesRepository repo = new();
        List<Inmueble> inmueble = repo.GetAllInmuebles();
        return View(inmueble);
    }
    public IActionResult Create()
    {
        InmueblesRepository repo = new();
        var enumTipo = repo.GetEnumsTipes("tipo");
        var enumUso = repo.GetEnumsTipes("uso");
        ViewBag.enumTipo = enumTipo;
        ViewBag.enumUso = enumUso;
        return View();
    }



    [HttpPost]
    public IActionResult Create(Inmueble inmueble)
    {
        try
        {
            InmueblesRepository repo = new();
            var res = repo.CreateInmueble(inmueble);
            if (res > 0)
            {

                TempData["AlertMessage"] = "Inmueble creado correctamente.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "No se pudo crear el inmueble.";
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
            InmueblesRepository repo = new();
            repo.DeleteInmueble(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public IActionResult Update(int id)
    {
        InmueblesRepository repo = new();
        var inmueble = repo.GetInmuebleById(id);
        var enumTipo = repo.GetEnumsTipes("tipo");
        var enumUso = repo.GetEnumsTipes("uso");
        ViewBag.enumTipo = enumTipo;
        ViewBag.enumUso = enumUso;
        return View(inmueble);
    }

    [HttpPost]
    public IActionResult Update(Inmueble inmueble)
    {
        InmueblesRepository repo = new();
        int res = repo.UpdateInmueble(inmueble);
        if (res > 0)
        {

            TempData["AlertMessage"] = "Inmueble modificado correctamente.";
            TempData["AlertType"] = "success";
            return RedirectToAction("Index");
        }
        else
        {
            TempData["AlertMessage"] = "No se pudo modificar el inmueble.";
            TempData["AlertType"] = "error";
            return RedirectToAction("Update");
        }

    }

    public IActionResult Details(int id)
    {
        InmueblesRepository repo = new();
        var propietario = repo.GetInmuebleById(id);
        return View(propietario);
    }

    [HttpGet("api/Inmuebles/GetInmueble/{id}")]
    public IActionResult GetInmueble(int id)
    {
        // Aquí, realiza la lógica para buscar el propietario por DNI
        InmueblesRepository repo = new();
        Inmueble inmuebleEncontrado = repo.GetInmuebleById(id); // Tu lógica de búsqueda

        if (inmuebleEncontrado != null)
        {
            // Retorna el objeto Propietario como JSON
            return Ok(inmuebleEncontrado);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("api/Inmuebles/GetInmuebles")]
    public IActionResult GetAllInmuebles()
    {

        InmueblesRepository repo = new();
        List<Inmueble> inmuebles = repo.GetAllInmuebles();

        if (inmuebles.Count > 0)
        {

            return Ok(inmuebles);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("Inmuebles/FiltrarInmuebles")]
    public IActionResult FiltrarInmuebles([FromQuery] string searchTerm)
    {
        Console.WriteLine("ENTRO");
        IActionResult result = GetAllInmuebles(); // Obtener el resultado

        if (result is OkObjectResult okObjectResult)
        {
            var inmueblesResponse = okObjectResult.Value; // Obtener el contenido del resultado
            if (inmueblesResponse is List<Inmueble> inmuebles)
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return Json(inmuebles); // Retorna todos los propietarios si no hay término de búsqueda
                }

                // Filtra los propietarios por el término de búsqueda (puedes personalizar la lógica según tus necesidades)
                var propietariosFiltrados = inmuebles.Where(p =>
                    p.Propietario.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    p.Propietario.Apellido.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    p.Propietario.Dni.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    p.Direccion.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                     p.Tipo.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)


                ).ToList();

                return Json(propietariosFiltrados);
            }
        }

        // Si algo salió mal o el tipo no coincide, regresa un error u otra respuesta
        return BadRequest("Error al obtener los propietarios");
    }


}