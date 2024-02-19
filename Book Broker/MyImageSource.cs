using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BookBroker
{
    //I made a useless class just to appease carousel views
    public class MyImageSource
    {
        public ImageSource imageSource { get; set; }
        public MyImageSource(string loc)
        {
            imageSource = ImageSource.FromResource(loc);
        }
    }
}
