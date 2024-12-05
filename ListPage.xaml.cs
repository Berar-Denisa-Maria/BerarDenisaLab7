namespace BerarDenisaLab7;
using BerarDenisaLab7.Models;
public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        // Verifica daca un produs este selectat din ListView
        var selectedProduct = listView.SelectedItem as Product;

        if (selectedProduct != null)
        {
            // sterge produsul selectat din baza de date
            await App.Database.DeleteProductAsync(selectedProduct);

            // Actualizeaza lista de produse afi?at? în ListView
            var shopList = (ShopList)BindingContext;
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
        }
        else
        {
            // Afiseaza un mesaj de eroare daca nu este selectat niciun produs
            await DisplayAlert("Eroare", "Selectati un produs pentru a-l sterge.", "OK");
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}