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
    public partial class SignUpPage : ContentPage
    {
        App globalref = (App)Application.Current;

        public SignUpPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SingUpButton_Clicked(object sender, EventArgs e)
        {
            //Makes sure that all of the information is filled in
            if (UsernameEntry.Text != null && PasswordEntry.Text != null && ConfirmPasswordEntry.Text != null)
            {
                //Make sure that the username doesn't already exist
                if (globalref.UsersDict.ContainsKey(UsernameEntry.Text))
                {
                    await DisplayAlert("Error", "This username already exists. Please try a different one.", "ok");
                }
                else
                {
                    //Check that the passwords match
                    if (PasswordEntry.Text == ConfirmPasswordEntry.Text)
                    {
                        //Add user to the User Dictionary
                        globalref.UsersDict.Add(UsernameEntry.Text, new User(UsernameEntry.Text, PasswordEntry.Text));

                        //Login as the newly created user
                        globalref.CurrentUser = globalref.UsersDict[UsernameEntry.Text];

                        await Navigation.PushAsync(new StorePage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Passwords did not match. Please try again.", "ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all fields", "ok");
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}