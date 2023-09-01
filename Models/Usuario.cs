using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;

public class Usuario : Persona {
    public int Id { get ; set ; }
    public string? Clave { get ; set ; }
    public string? Avatar { get ; set ; }
    public bool? Estado{get ; set ; }
    public IFormFile? AvatarFile { get ; set ; }
    public string? Rol { get ; set ; }
   
}

