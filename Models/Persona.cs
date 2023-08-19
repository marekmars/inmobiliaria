
using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models;
public abstract class Persona
{
    [Required]
    public string Dni { get; set; } = "";
    [Required]
    public string Apellido { get; set; } = "";
    [Required]
    public string Nombre { get; set; } = "";
    [Required]
    public string Telefono { get; set; } = "";
    [Required]
    public string Correo { get; set; } = "";

}