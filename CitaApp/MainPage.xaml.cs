namespace CitaApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSolicitarCitaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SolicitarCitaPage());
    }

    private async void OnConsultaCitasClicked(object sender, EventArgs e)
    {
       
        await Navigation.PushAsync(new ConsultarCitaPage());
    }
}
