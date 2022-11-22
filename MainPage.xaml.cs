using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VideoDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Add_Subtitle(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.Add_Subtitle();
        }

        private void Button_Click_Set_Color(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.Set_Color();
        }

        private void Button_Click_Font_Size(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.Set_Font_Size();
        }

        private void Button_Click_Set_Encode(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.Set_Encode();
        }


    }
}
