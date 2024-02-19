using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Book_Broker
{
    public partial class App : Application
    {
        //Creates two users as test data
        public User TestCade = new User("Cade", "CadeIsCool");
        public User TestSean = new User("Sean", "SeanIsCool");

        //A dictionary to contain all of the users.
        //This means that a user can be found by using their username
        public IDictionary<string, User> UsersDict = new Dictionary<string, User>();

        //A dictionairy to contain the store page listings.
        //This means that I don't have to use a search function to find a listing when it is editted in
        //the edit page
        public IDictionary<int, Listing> StorePageListings = new Dictionary<int, Listing>();
       
        //Contains the integer that will be used for the ID when a new listing is created.
        //Incrementing this number whenever a listing is created means that two listings will never have the same ID
        public int NextID = 11;

        //The logged in user
        public User CurrentUser;

        public App()
        {
            InitializeComponent();
            
            //Adds the created users to the user dictionary
            UsersDict.Add("Cade", TestCade);
            UsersDict.Add("Sean", TestSean);


            //Adds listings to Cade's MyListings list and also the store page dictionary
            TestCade.MyListingsList.Add(new Listing(0, "Cade's Book 1", "Cade Alcindor", "Cade", 1));
            StorePageListings.Add(0, new Listing(0, "Cade's Cool Book 1", "Cade Alcindor", "Cade", 1));
            TestCade.MyListingsList.Add(new Listing(1, "Cade's Cool Book 2", "Cade Alcindor", "Cade", 2));
            StorePageListings.Add(1, new Listing(1, "Cade's Cool Book 2", "Cade Alcindor", "Cade", 2));
            TestCade.MyListingsList.Add(new Listing(2, "Cade's Cool Book 3", "Cade Alcindor", "Cade", 3));
            StorePageListings.Add(2, new Listing(2, "Cade's Cool Book 3", "Cade Alcindor", "Cade", 3));

            //Adds listings to Sean's MyListings list and also the store page dictionary
            TestSean.MyListingsList.Add(new Listing(4, "Sean's Cool Book 1", "Sean Ashfield", "Sean", 1));
            StorePageListings.Add(4, new Listing(4, "Sean's Cool Book 1", "Sean Ashfield", "Sean", 1));
            TestSean.MyListingsList.Add(new Listing(5, "Sean's Cool Book 2", "Sean Ashfield", "Sean", 2));
            StorePageListings.Add(5, new Listing(5, "Sean's Cool Book 2", "Sean Ashfield", "Sean", 2));
            TestSean.MyListingsList.Add(new Listing(6, "Sean's Cool Book 3", "Sean Ashfield", "Sean", 3));
            StorePageListings.Add(6, new Listing(6, "Sean's Cool Book 3", "Sean Ashfield", "Sean", 3));
            TestSean.MyListingsList.Add(new Listing(7, "Sean's Cool Book 4", "Sean Ashfield", "Sean", 4));
            StorePageListings.Add(7, new Listing(7, "Sean's Cool Book 4", "Sean Ashfield", "Sean", 4));
            TestSean.MyListingsList.Add(new Listing(8, "Sean's Cool Book 5", "Sean Ashfield", "Sean", 5));
            StorePageListings.Add(8, new Listing(8, "Sean's Cool Book 5", "Sean Ashfield", "Sean", 5));

            //Adds listings to Cade's MyListings list and also the store page dictionary. Done in this
            //order so that it will refelct in the most recent carousel view
            TestCade.MyListingsList.Add(new Listing(9, "Cade's Cool Book 4", "Cade Alcindor", "Cade", 4));
            StorePageListings.Add(9, new Listing(9, "Cade's Cool Book 4", "Cade Alcindor", "Cade", 4));
            TestCade.MyListingsList.Add(new Listing(10, "Cade's Cool Book 5", "Cade Alcindor", "Cade", 5));
            StorePageListings.Add(10, new Listing(10, "Cade's Cool Book 5", "Cade Alcindor", "Cade", 5));

            MainPage = new NavigationPage(new StorePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
