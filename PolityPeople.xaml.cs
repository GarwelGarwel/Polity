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

namespace Polity
{
    /// <summary>
    /// Interaction logic for PolityPeople.xaml
    /// </summary>
    public partial class PolityPeople : Page
    {
        public PolityPeople()
        {
            InitializeComponent();
            DataContext = Game.TheGame;
        }

        MainWindow Win
        {
            get { return (MainWindow) Window.GetWindow(this); }
        }

        private void Click_ViewMarket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PolityMarket ());
        }

        private void Click_ViewParams(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PolityParams());
        }

        private void Click_ViewEconomy(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PolityEconomy());
        }

        private void Click_NextTurn(object sender, RoutedEventArgs e)
        {
            Win.NextTurn();
        }

        private void Click_NextMonth(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 30; i++) Win.NextTurn();
        }

        private void Click_RunStop(object sender, RoutedEventArgs e)
        {
            if (Win.IsRunning)
            {
                runStopButton.Content = "Run";
                Win.IsRunning = false;
            }
            else
            {
                runStopButton.Content = "Stop";
                Win.IsRunning = true;
            }
        }

        private void Click_Decisions(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PolityDecisions());
        }
    }
}
