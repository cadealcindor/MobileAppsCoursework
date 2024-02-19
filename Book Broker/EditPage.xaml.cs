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
    public partial class EditPage : ContentPage
    {
        App globalref = (App)Application.Current;

        int index;
        ListView MyListingsListView;

        public EditPage(Listing L, int i, ListView MLLV)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = L;
            index = i;
            MyListingsListView = MLLV;
            UsernameDisplay.Text = globalref.CurrentUser.username;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(BindingContext != null)
            {
                //Update the information in the user's list of listings
                globalref.CurrentUser.MyListingsList[index].Title = TitleEntry.Text;
                globalref.CurrentUser.MyListingsList[index].Author = AuthorEntry.Text;
                globalref.CurrentUser.MyListingsList[index].Price = Convert.ToDouble(PriceEntry.Text);

                //update the information in the storepage's list of listings
                globalref.StorePageListings[Convert.ToInt32(IDEntry.Text)].Title = TitleEntry.Text;
                globalref.StorePageListings[Convert.ToInt32(IDEntry.Text)].Author = AuthorEntry.Text;
                globalref.StorePageListings[Convert.ToInt32(IDEntry.Text)].Price = Convert.ToDouble(PriceEntry.Text);

                //refresh the MyListings list view
                if (MyListingsListView != null)
                {
                    MyListingsListView.ItemsSource = null;
                    MyListingsListView.ItemsSource = globalref.CurrentUser.MyListingsList;
                }

                await Navigation.PopAsync();
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            //Make sure that the user wants to delete the listing
            var response = await DisplayActionSheet("Are you sure that you want to delete this listing?", "yes", "no");
            if(response == "yes")
            {
                //remove the listing from all locations
                globalref.StorePageListings.Remove(Convert.ToInt32(IDEntry.Text));
                globalref.CurrentUser.MyListingsList.Remove(globalref.CurrentUser.MyListingsList[index]);

                //refresh the MyListings list view
                if (MyListingsListView != null)
                {
                    MyListingsListView.ItemsSource = null;
                    MyListingsListView.ItemsSource = globalref.CurrentUser.MyListingsList;
                }

                await Navigation.PopAsync();
            }
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            globalref.CurrentUser = null;
            await Navigation.PushAsync(new StorePage());
        }
    }
}