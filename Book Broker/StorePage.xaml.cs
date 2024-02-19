using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace Book_Broker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        App globalref = (App)Xamarin.Forms.Application.Current;
        
        IEnumerable<KeyValuePair<int, Listing>> StorePageItemSource;
        List<Listing> recentsList = new List<Listing>();

        public StorePage()
        { 
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

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

            //remove listings that are being sold by the current user
            if (globalref.CurrentUser != null)
            {

                //Removes the user's own listings from the item source
                var returnedListings =
                    from KeyValuePair<int, Listing> listing in globalref.StorePageListings
                    where listing.Value.Seller != globalref.CurrentUser.username
                    select listing;


                StorePageItemSource = returnedListings;
            }
            else
            {
                //If the user isn't logged in show all items
                StorePageItemSource = globalref.StorePageListings;
            }

            //Add the 5 most recent items to the most recent list
            StorePageListView.ItemsSource = StorePageItemSource;
            int numRecents = 0;
            int i = 0;
            while (numRecents < 5 && i < globalref.StorePageListings.Count())
            {
                i++;
                if (globalref.StorePageListings[globalref.NextID - i] != null)
                {
                    if (globalref.CurrentUser != null)
                    {
                        if (globalref.CurrentUser.username != globalref.StorePageListings[globalref.NextID - i].Seller)
                        {
                            recentsList.Add(globalref.StorePageListings[globalref.NextID - i]);
                            numRecents++;
                        }
                    }
                    else
                    {
                        recentsList.Add(globalref.StorePageListings[globalref.NextID - i]);
                        numRecents++;
                    }
                }
            }

            //If there are less than 5 items in the recent list, fill the rest with blank items
            while(recentsList.Count < 5) 
            {
                recentsList.Add(new Listing(-1, "N/A", "N/A", "N/A", 0));
            }

            //set the items source
            MostRecentCarouselView.ItemsSource = recentsList;            
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {   
            //format the search term
            string searchTerm = SearchBar.Text.ToLower();

            //get all listings that contain the search term in their name or author name
            var reutrnedListings =
                from KeyValuePair<int, Listing> listing in StorePageItemSource
                where listing.Value.Title.ToLower().Contains(searchTerm) || listing.Value.Author.ToLower().Contains(searchTerm) //Change if filter is added
                select listing;

            //set the item source
            StorePageListView.ItemsSource = reutrnedListings;
        }

        private void StorePageListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (KeyValuePair<int, Listing>)e.SelectedItem;
            //Listing SelectedListing = e.SelectedItem as Listing;
            Navigation.PushAsync(new ListingDetailsPage(selectedItem.Key, selectedItem.Value, StorePageListView));
            
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

            //lazy method of refreshing
            await Navigation.PushAsync(new StorePage());
        }
    }
}