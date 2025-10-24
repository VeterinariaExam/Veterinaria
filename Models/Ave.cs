// Clase que representa una mascota del tipo Ave, hereda de Mascota
public class Ave : Mascota
{
    // Propiedad abstracta implementada para identificar especie
    public override string Especie => "Ave";

    // Envergadura del ave en centímetros
    public double EnvergaduraCm { get; set; }
}
