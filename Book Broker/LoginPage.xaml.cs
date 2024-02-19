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
    public partial class LoginPage : ContentPage
    {
        App globalref = (App)Application.Current;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            //Check that the user exists in the user dictionary
            if (globalref.UsersDict.ContainsKey(UsernameEntry.Text))
            {

                //makes sure that the password matches the password of the user found in the user
                //dictionary
                if(globalref.UsersDict[UsernameEntry.Text].password == PasswordEntry.Text)
                {
                    globalref.CurrentUser = globalref.UsersDict[UsernameEntry.Text];

                    await Navigation.PushAsync(new StorePage());
                }
                else
                {
                    await DisplayAlert("Access Denied", "Incorrect username and password entered. Please try again", "ok");
                }
            }
            else
            {
                await DisplayAlert("Access Denied", "Incorrect username and password entered. Please try again", "ok");
            }
        }

        private void ShowPasswordButton_Clicked(object sender, EventArgs e)
        {
            //If the password is hidden
            if (PasswordEntry.IsPassword)
            {
                //show the password
                PasswordEntry.IsPassword = false;
                ShowPasswordButton.Text = "Hide Password";
            }

            //if the password is shown
            else
            {
                //hide the password
                PasswordEntry.IsPassword = true;
                ShowPasswordButton.Text = "Show Password";
            }
        }
    }
}