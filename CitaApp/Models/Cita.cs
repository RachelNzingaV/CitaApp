namespace CitaApp.Models;

public class Citas
{
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string NumPaciente { get; set; }
    public string Turno { get; set; }
    public string Especialidad { get; set; }
    public string Doctor { get; set; }
    public int DiasEspera { get; set; }
    public DateTime FechaSolicitud { get; set; }
}
