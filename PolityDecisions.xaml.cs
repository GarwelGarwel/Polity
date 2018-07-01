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
    /// Interaction logic for PolityDecisions.xaml
    /// </summary>
    public partial class PolityDecisions : Page
    {
        public PolityDecisions()
        {
            InitializeComponent();
            DataContext = Game.TheGame;
        }

        private void Click_Decision(object sender, RoutedEventArgs e)
        {
            if (DecisionList.SelectedItem == null) return;
            ((Decision)DecisionList.SelectedItem).CheckAndRun();
        }
    }
}
