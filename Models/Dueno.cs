using VeterinariaBDOO_SegundoParcial_NombreDeUnIntegrante.Modelo;

// Clase que representa a un dueño de mascotas, hereda de Persona
public class Dueno : Persona
{
    public required string Direccion { get; set; }           // Dirección del dueño
    public List<Mascota> Mascotas { get; set; } = new List<Mascota>(); // Lista de mascotas asociadas
}
