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
    public partial class MyListingsPage : ContentPage
    {
        App globalref = (App)Application.Current;
        
        int selectedIndex;

        public MyListingsPage()
        {
            InitializeComponent();

            UsernameDisplay.Text = globalref.CurrentUser.username;
            BindingContext = globalref.CurrentUser.MyListingsList;
            MyListingsListView.ItemsSource = globalref.CurrentUser.MyListingsList;
            NavigationPage.SetHasNavigationBar(this, false);
        }


        private async void MyListingsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //get the selected item
            Listing selectedListing = MyListingsListView.SelectedItem as Listing;

            //deselect on the list view
            MyListingsListView.SelectedItem = null;

            //make sure that the listing is not null
            if (selectedListing != null)
            {
                await Navigation.PushAsync(new EditPage(selectedListing, selectedIndex, MyListingsListView));
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddListingPage(MyListingsListView));
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //format the search term
            string searchTerm = SearchBar.Text.ToLower();

            //get the listings that have the search term in their title or author name
            var reutrnedListings =
                from Listing listing in globalref.CurrentUser.MyListingsList
                where listing.Title.Contains(searchTerm) || listing.Author.Contains(searchTerm)
                select listing;

            //Set the items source
            MyListingsListView.ItemsSource = reutrnedListings;
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            globalref.CurrentUser = null;
            await Navigation.PushAsync(new StorePage());
        }
    }
}