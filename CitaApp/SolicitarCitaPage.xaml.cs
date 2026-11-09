using System.Text.Json;
using CitaApp.Models;

namespace CitaApp;

public partial class SolicitarCitaPage : ContentPage
{
    public SolicitarCitaPage()
    {
        InitializeComponent();
        CargarEspecialidades();
        CargarDatosGuardados();
    }

    private void CargarEspecialidades()
    {
        var lista = new List<Especialidades>
        {
            new Especialidades { Imagen = "cardiologia.png", Nombre = "Cardiología", Doctor = "Dra. Laura Martín", DiasEspera = 25 },
            new Especialidades { Imagen = "dermatologia.png", Nombre = "Dermatología", Doctor = "Dr. Javier Ruiz", DiasEspera = 40 },
            new Especialidades { Imagen = "oftalmologia.png", Nombre = "Oftalmología", Doctor = "Dra. Elena Gómez", DiasEspera = 32 },
            new Especialidades { Imagen = "traumatologia.png", Nombre = "Traumatología", Doctor = "Dr. Carlos Navarro", DiasEspera = 55 },
            new Especialidades { Imagen = "pediatria.png", Nombre = "Pediatría", Doctor = "Dra. Marta Sánchez", DiasEspera = 7 },
            new Especialidades { Imagen = "neurologia.png", Nombre = "Neurología", Doctor = "Dr. Andrés López", DiasEspera = 60 },
            new Especialidades { Imagen = "ginecologia.png", Nombre = "Ginecología", Doctor = "Dra. Sofía Torres", DiasEspera = 21 },
            new Especialidades { Imagen = "otorrino.png", Nombre = "Otorrinolaringología", Doctor = "Dr. Pablo Hernández", DiasEspera = 35 }
        };
        cvEspecialidades.ItemsSource = lista;
    }

    private void CargarDatosGuardados()
    {
        if (Preferences.ContainsKey("nombre"))
        {
            txtNombre.Text = Preferences.Get("nombre", "");
            txtApellidos.Text = Preferences.Get("apellidos", "");
            txtNumPaciente.Text = Preferences.Get("numPaciente", "");
            chkRecuerda.IsChecked = Preferences.Get("recuerda", false);
        }
    }

    private async void OnEnviarCitaClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(txtNumPaciente.Text))
        {
            await DisplayAlert("Faltan datos", "Introduce nombre, apellidos y número de paciente.", "OK");
            return;
        }

        if (pickerTurno.SelectedItem == null)
        {
            await DisplayAlert("Faltan datos", "Selecciona una preferencia de turno.", "OK");
            return;
        }

        if (cvEspecialidades.SelectedItem == null)
        {
            await DisplayAlert("Faltan datos", "Selecciona una especialidad.", "OK");
            return;
        }

        if (chkRecuerda.IsChecked)
        {
            Preferences.Set("nombre", txtNombre.Text);
            Preferences.Set("apellidos", txtApellidos.Text);
            Preferences.Set("numPaciente", txtNumPaciente.Text);
            Preferences.Set("recuerda", true);
        }
        else
        {
            Preferences.Remove("nombre");
            Preferences.Remove("apellidos");
            Preferences.Remove("numPaciente");
            Preferences.Remove("recuerda");
        }

        var espSeleccionada = (Especialidades)cvEspecialidades.SelectedItem;

        Citas nuevaCita = new Citas
        {
            Nombre = txtNombre.Text,
            Apellidos = txtApellidos.Text,
            NumPaciente = txtNumPaciente.Text,
            Turno = pickerTurno.SelectedItem.ToString(),
            Especialidad = espSeleccionada.Nombre,
            Doctor = espSeleccionada.Doctor,
            DiasEspera = espSeleccionada.DiasEspera,
            FechaSolicitud = DateTime.Now
        };

        string rutaCitas = Path.Combine(FileSystem.AppDataDirectory, "citas.json");
        List<Citas> listaCitas = new List<Citas>();

        if (File.Exists(rutaCitas))
        {
            string jsonExistente = File.ReadAllText(rutaCitas);
            listaCitas = JsonSerializer.Deserialize<List<Citas>>(jsonExistente) ?? new List<Citas>();
        }

        listaCitas.Add(nuevaCita);

        string jsonActualizado = JsonSerializer.Serialize(listaCitas);
        File.WriteAllText(rutaCitas, jsonActualizado);

        string mensaje = $"{txtNombre.Text} {txtApellidos.Text}, a la mayor brevedad le informaremos de su cita en {espSeleccionada.Nombre}. Puede consultar sus citas desde la sección Consulta de citas.";
        await DisplayAlert("Cita solicitada", mensaje, "OK");

        await Navigation.PopAsync();
    }
}