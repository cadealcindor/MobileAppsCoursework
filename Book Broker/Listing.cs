using BookBroker;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Book_Broker
{

//A pretty basic class to contain information about a listing.ID is used to differentiate the listings.
    public class Listing
    {
        public int ID { set; get; }
        public string Title { set; get; }
        public string Author { set; get; }
        public string Seller { set; get; }
        public double Price { set; get; }
        public string imageLoaction { get; set; }

        //CoverImageSource is an attribute to turn the string location of an image into an image source
        //that is useable by xaml.
        public ImageSource CoverImageSource { get; set; }

        public string[] ImagesLocationList { get; set; } = { "BookBroker.Images.genericBookCover.jpg", "BookBroker.Images.genericBookCover1.jpg", "BookBroker.Images.genericBookCover2.jpg" };

        //One oddity that might stand out is the MyImageSource class used here.Why would I have to make
        //my own image source class when I’ve already used to prebuilt ImageSource class? This is
        //because(and I don’t know why) I was having trouble binding an image source in a carousel view
        //to an item in a list.But I knew how to bind an image source in a carousel view to an
        //attribute of an item in a list! So I made a new class to contain an image source  as an
        //attribute, so that I can make a list of them and bind it to a carousel view.
        public List<MyImageSource> ImagesSourceList { get; set; }


        public Listing(int id, string title, string author, string seller, double price)
        {
            ID = id;
            Title = title;
            Author = author;
            Seller = seller;
            Price = price;
            imageLoaction = "BookBroker.Images.genericBookCover.jpg";
            CoverImageSource = ImageSource.FromResource(imageLoaction);
            ImagesSourceList = new List<MyImageSource>();
            for(int i  = 0; i < ImagesLocationList.Length; i++)
            {
                ImagesSourceList.Add(new MyImageSource(ImagesLocationList[i]));
                
            }
        }
    }
}
