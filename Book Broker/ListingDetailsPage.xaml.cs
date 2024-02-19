using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Book_Broker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListingDetailsPage : ContentPage
    {
        App globalref = (App)Application.Current;
        
        ImageSource getImageSource;

        int key;
        Listing SelectedListing { get; set; }
        ListView StorePageListingsListView = null;

        public ListingDetailsPage(int k, Listing L, ListView SPLLV)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            SelectedListing = L;
            key = k;
            BindingContext = SelectedListing;
            PriceLable.Text = "£" + Convert.ToString(SelectedListing.Price);
            StorePageListingsListView = SPLLV;

            //Set up the bar showing the user information correctly
            if (globalref.CurrentUser != null)
            {
                UsernameDisplay.Text = globalref.CurrentUser.username;

                LoginButton.IsVisible = false;
                SignUpButton.IsVisible = false;
                LogoutButton.IsVisible = true;
                MyListingsButton.IsVisible = true;
            }
            else
            {
                UsernameDisplay.Text = "Guest";
                SignUpButton.IsVisible = true;
                LoginButton.IsVisible = true;
                LogoutButton.IsVisible = false;
                MyListingsButton.IsVisible = false;
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BuyButton_Clicked(object sender, EventArgs e)
        {

            //make sure that the user wants to buy this book
            var respons = await DisplayActionSheet("Are you sure you want to buy this book?", "Yes", "No");
            if (respons == "Yes")
            {
                await DisplayAlert("Alert", "You have bought " + SelectedListing.Title, "ok");

                //remove the listing from all locations
                globalref.UsersDict[SelectedListing.Seller].MyListingsList.Remove(SelectedListing);
                globalref.StorePageListings.Remove(key);



                //refresh the list view
                if (StorePageListingsListView != null)
                {
                    StorePageListingsListView.ItemsSource = null;
                    StorePageListingsListView.ItemsSource = globalref.StorePageListings;
                }

                await Navigation.PopAsync();
            }
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        private async void MyListingsButton_Clicked(object sender, EventArgs e)
        {
            //Check that the user is logged in
            if (globalref.CurrentUser == null)
            {
                //if not, inform the user that they must be logged in, and give them the option to
                //log in
                var response = await DisplayActionSheet("You must be logged in to access 'My Listings'!", "Log in", "Go Back");
                if (response == "Log in")
                {
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            else
            {
                await Navigation.PushAsync(new MyListingsPage());
            }
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            globalref.CurrentUser = null;
          
            await Navigation.PushAsync(new StorePage());
        }
    }
}