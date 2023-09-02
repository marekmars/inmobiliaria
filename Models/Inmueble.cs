
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models;
[Table("inmuebles")]
public class Inmueble
{   
    [Required]
    public int Id { get; set; }
    [Required]
    public int IdPropietario { get; set; }
    [ForeignKey(nameof(IdPropietario))]
    public Propietario? Propietario { get; set; }
    [Required]
    public string Direccion { get; set; }="";
    [Required]
    public string Uso { get; set; }="";
    [Required]
    public string Tipo { get; set; }="";
    [Required]
    public int CantAmbientes { get; set; }
    [Required]
    public double Latitud { get; set; }
    [Required]
    public double Longitud { get; set; }
    [Required]
    public double Precio { get; set; }
    [Required]
    public bool Estado { get; set; }=true;
    [Required]
    public bool Disponible { get; set; }=true;
}
