using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Add these namespaces
using System.Windows;
using System.Windows.Controls;

namespace pokemon
{
    //This will be your custom window class which is derieved    
    //from the base class Window.
    public partial class wpf : Window
    {
        public wpf()
        {
            //This is created just to show a reference , the 
            //below code can aswell be witten wihin this     
            //constructor.
            InitializeComponent();
        }

    }
}