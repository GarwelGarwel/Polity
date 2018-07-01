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
    /// Interaction logic for PolityEconomy.xaml
    /// </summary>
    public partial class PolityEconomy : Page
    {
        public PolityEconomy()
        {
            InitializeComponent();
            DataContext = Game.TheGame.Country;
        }

        private void Click_IncomeTaxUp(object sender, RoutedEventArgs e)
        {
            Effect eff = new ChangeParameterEffect(Game.TheGame.Country, ParameterIds.IncomeTaxRate, 1, 0.02);
            eff.Run();
            //MainWindow.Game.Country.IncomeTaxRate += 0.02;
            incomeTax.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        private void Click_IncomeTaxDown(object sender, RoutedEventArgs e)
        {
            Game.TheGame.Country.IncomeTaxRate.Value -= 0.02;
            incomeTax.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        private void Click_WelfareSubsidyUp(object sender, RoutedEventArgs e)
        {
            Game.TheGame.Country.WelfareSubsidy.Value += 0.02;
            welfareSubsidy.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        private void Click_WelfareSubsidyDown(object sender, RoutedEventArgs e)
        {
            Game.TheGame.Country.WelfareSubsidy.Value -= 0.02;
            welfareSubsidy.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        private void Click_BureaucratSalaryUp(object sender, RoutedEventArgs e)
        {
            Game.TheGame.Country.BureaucratSalary.Value += 0.02;
            bureaucratSalary.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        private void Click_BureaucratSalaryDown(object sender, RoutedEventArgs e)
        {
            Game.TheGame.Country.BureaucratSalary.Value -= 0.02;
            bureaucratSalary.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }
    }
}
