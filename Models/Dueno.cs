using VeterinariaBDOO_SegundoParcial_NombreDeUnIntegrante.Modelo;

public class Dueno : Persona
{
    public required string Direccion { get; set; }
    public List<Mascota> Mascotas { get; set; } = new List<Mascota>();
}