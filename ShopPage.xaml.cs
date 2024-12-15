using BerarDenisaLab7.Models;
using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;
namespace BerarDenisaLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

       
        if (string.IsNullOrEmpty(shop.Adress))
        {
            await DisplayAlert("Error", "Magazinul nu are o adresa specificata.", "OK");
            return;
        }

        var shoplocation = new Location(46.7492379, 23.5745597); 
        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };

        
        var myLocation = new Location(46.7731796, 23.6213886);

        
        var distance = myLocation.CalculateDistance(shoplocation, DistanceUnits.Kilometers);

        if (distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = "Magazinul este la mai putin de 5 km distanta.",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }

  
        await Map.OpenAsync(shoplocation, options);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        if (shop == null || shop.ID == 0)
        {
            await DisplayAlert("Error", "Magazinul nu exista sau nu poate fi sters.", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirmare", "Sigur doresti sa stergi acest magazin?", "Da", "Nu");
        if (answer)
        {
            await App.Database.DeleteShopAsync(shop); 
            await Navigation.PopAsync(); 
        }
    }

}