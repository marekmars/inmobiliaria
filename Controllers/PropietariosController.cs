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
            InmueblesRepository repo2 = new();
            var enumTipo = repo2.GetEnumsTipes("tipo");
            var enumUso = repo2.GetEnumsTipes("uso");
            ViewBag.enumTipo = enumTipo;
            ViewBag.enumUso = enumUso;
            
          

            string returnUrl = Request.Headers["Referer"].ToString();
            Uri refererUri = new Uri(returnUrl);
            string relativePath = refererUri.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
            Console.WriteLine(relativePath);
            if (res > 0)
            {

                TempData["AlertMessage"] = "Propietario creado correctamente.";
                TempData["AlertType"] = "success";


                if (relativePath == "/Propietarios/Create")
                {

                    return Redirect("index");
                }



            }
            else if (res == -1)
            {
                TempData["AlertMessage"] = "No se pudo crear el propietario, Error en la base de datos.";
                TempData["AlertType"] = "error";

            }
            else if (res == -2)
            {
                TempData["AlertMessage"] = "No se pudo crear el propietario, Ya existe uno con ese DNI..";
                TempData["AlertType"] = "error";

            }
            else if (res == -3)
            {
                TempData["AlertMessage"] = "No se pudo crear el propietario, Ingrese un email valido.";
                TempData["AlertType"] = "error";

            }
            else if (res == -4)
            {
                TempData["AlertMessage"] = "No se pudo crear el propietario, Ingrese un DNI valido.";
                TempData["AlertType"] = "error";

            }
            else
            {
                TempData["AlertMessage"] = "No se pudo crear el propietario, Ya existe un propietario con ese DNI.";
                TempData["AlertType"] = "error";

            }
            return Redirect(relativePath);
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

    public IActionResult Details(int id)
    {
        PropietariosRepository repo = new();
        var propietario = repo.GetPropietarioById(id);
        return View(propietario);
    }

    [HttpGet("api/Propietarios/GetPropietario/{dni}")]
    public IActionResult GetPropietario(string dni)
    {
        // Aquí, realiza la lógica para buscar el propietario por DNI
        PropietariosRepository repo = new();
        Propietario propietarioEncontrado = repo.GetPropietarioByDni(dni); // Tu lógica de búsqueda

        if (propietarioEncontrado != null)
        {
            // Retorna el objeto Propietario como JSON
            return Ok(propietarioEncontrado);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("api/Propietarios/GetPropietarios")]
    public IActionResult GetAllPropietarios()
    {
        // Aquí, realiza la lógica para obtener todos los propietarios
        PropietariosRepository repo = new();
        List<Propietario> propietarios = repo.GetAllPropietarios(); // Tu lógica para obtener todos los propietarios

        if (propietarios.Count > 0)
        {
            // Retorna la lista de propietarios como JSON
            return Ok(propietarios);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("Propietario/FiltrarPropietarios")]
    public IActionResult FiltrarPropietarios([FromQuery] string searchTerm)
    {
        Console.WriteLine("ENTRO");
        IActionResult result = GetAllPropietarios(); // Obtener el resultado

        if (result is OkObjectResult okObjectResult)
        {
            var propietariosResponse = okObjectResult.Value; // Obtener el contenido del resultado
            if (propietariosResponse is List<Propietario> propietarios)
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return Json(propietarios); // Retorna todos los propietarios si no hay término de búsqueda
                }

                // Filtra los propietarios por el término de búsqueda (puedes personalizar la lógica según tus necesidades)
                var propietariosFiltrados = propietarios.Where(p =>
                    p.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    p.Apellido.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    p.Dni.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
                ).ToList();

                return Json(propietariosFiltrados);
            }
        }

        // Si algo salió mal o el tipo no coincide, regresa un error u otra respuesta
        return BadRequest("Error al obtener los propietarios");
    }

}