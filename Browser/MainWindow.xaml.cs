using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp;
using CefSharp.Wpf;

namespace Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> oldalak;
        int jelenlegi = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        void Kezdooldal()
        {
            textbox.Text = "https://www.google.hu";
            browser.Address = "https://www.google.hu";
            oldalak.Add("https://www.google.hu");
        }

        void WeboldalBetoltes(string Link, bool addToList = true)
        {
            textbox.Text = Link;
            browser.Address = Link;
            MenuItem history2 = new MenuItem();
            history2.Click += HistoryClicked;
            history2.Header = Link;
            history2.Width = 200;
            history.Items.Add(history2);
            if (addToList)
            {
                jelenlegi++;
                oldalak.Add(Link);
            }
        }

        void WeboldalValtas(string option)
        {
            if (option == "→")
            {
                if ((oldalak.Count - jelenlegi - 1) != 0)
                {
                    jelenlegi++;
                    WeboldalBetoltes(oldalak[jelenlegi], false);
                }
            }

            else
            {
                if ((oldalak.Count + jelenlegi - 1) >= oldalak.Count)
                {
                    jelenlegi--;
                    WeboldalBetoltes(oldalak[jelenlegi], false);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button gomb = (Button)sender;
            WeboldalValtas(gomb.Content.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WeboldalBetoltes(oldalak[jelenlegi]);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            browser.Stop();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WeboldalBetoltes(oldalak[0]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            oldalak = new List<string>();
            Kezdooldal();
        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WeboldalBetoltes(textbox.Text);
            }
        }

        private void HistoryClicked(object sender, RoutedEventArgs e)
        {
            MenuItem history = (MenuItem)sender;
            WeboldalBetoltes(history.Header.ToString());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (oldalak.Count != 0)
            {
                history.PlacementTarget = historygomb;
                history.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                history.HorizontalOffset = -155;
                history.IsOpen = true;
            }
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
