namespace VeterinariaBDOO_SegundoParcial_NombreDeUnIntegrante.Modelo
{
    public abstract class Persona
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Telefono { get; set; }
    }
}
