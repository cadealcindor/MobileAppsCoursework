using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Broker
{
    //This is a pretty basic class to contain a users information. Each user will have a list of
    //Listings which will contain the listings that they themselves have added. This way they can be
    //displayed in the My Listings page and hidden in the Store Page
    public class User
    {
        public string username;
        public string password;

        //Stores the listings created by the user
        public List<Listing> MyListingsList { get; set; }

        public User(string name, string pass)
        {
            username = name;
            password = pass;

            MyListingsList = new List<Listing>();
        }

    }
}
