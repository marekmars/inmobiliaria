
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models;
[Table("inmuebles")]
public class Inquilino : Persona
{
    
     [Required]
      [Key]
    public int Id { get; set; }
    
    
 public override string ToString()
    {
        return Apellido + " " + Nombre;
    }
}
