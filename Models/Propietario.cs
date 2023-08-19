
using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models;
public class Propietario : Persona
{
    [Required]
    [Key]
    public int Id { get; set; }
    

}