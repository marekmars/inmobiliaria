using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models;

public class Usuario : Persona {
    public int Id { get ; set ; }
    public string? Clave { get ; set ; }
    public string? Avatar { get ; set ; }
    public IFormFile AvatarFile { get ; set ; }
    public int? Rol { get ; set ; }
    public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "" ;

    public static IDictionary<int, string> ObtenerRoles()
    {
        return Enum.GetValues(typeof(enRoles))
            .Cast<enRoles>()
            .ToDictionary(role => (int)role, role => role.ToString());
    }
}

public enum enRoles
{
    Administrador = 1,
    Empleado = 2 
     
}