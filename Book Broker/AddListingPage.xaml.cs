using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Book_Broker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddListingPage : ContentPage
    {
        App globalref = (App)Application.Current;

        ListView MyListingsListView;

        public AddListingPage(ListView MLListView)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            UsernameDisplay.Text = globalref.CurrentUser.username;
            MyListingsListView = MLListView;
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (TitleEntry.Text != null && AuthorEntry.Text != null && PriceEntry.Text != null) {
                
                //Creates and adds a new listing to the user's list of listings
                globalref.CurrentUser.MyListingsList.Add(new Listing(globalref.NextID, TitleEntry.Text, AuthorEntry.Text, globalref.CurrentUser.username, Convert.ToDouble(PriceEntry.Text)));
                
                //Creates and adds a new listing to the store page's list of listings
                globalref.StorePageListings.Add(globalref.NextID,new Listing(globalref.NextID, TitleEntry.Text, AuthorEntry.Text, globalref.CurrentUser.username, Convert.ToDouble(PriceEntry.Text)));

                //increase the id of the next created listing by one
                globalref.NextID++;


                //Had an issue where the listview wouldn't update after the item was added.
                //This forces the listveiw to update
                if (MyListingsListView != null)
                {
                    MyListingsListView.ItemsSource = null;
                    MyListingsListView.ItemsSource = globalref.CurrentUser.MyListingsList;
                }

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all of the attributes", "ok");
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        
        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            globalref.CurrentUser = null;
            await Navigation.PushAsync(new StorePage());
        }
    }
}