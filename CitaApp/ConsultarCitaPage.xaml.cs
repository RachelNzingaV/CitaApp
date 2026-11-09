using System.Text.Json;
using CitaApp.Models;

namespace CitaApp;

public partial class ConsultarCitaPage : ContentPage
{
    public ConsultarCitaPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarCitas();
    }

    private void CargarCitas()
    {
        string rutaCitas = Path.Combine(FileSystem.AppDataDirectory, "citas.json");

        if (File.Exists(rutaCitas))
        {
            string json = File.ReadAllText(rutaCitas);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var listaCitas = JsonSerializer.Deserialize<List<Citas>>(json);
                cvCitas.ItemsSource = listaCitas;
            }
        }
    }
}