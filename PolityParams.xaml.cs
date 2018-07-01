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
    /// Interaction logic for PolityParams.xaml
    /// </summary>
    public partial class PolityParams : Page
    {
        public PolityParams()
        {
            InitializeComponent();
            DataContext = Game.TheGame.Country;
        }

        double GetMultiplier(ParameterIds id)
        {
            return 1;
        }

        double GetAddend(ParameterIds id)
        {
            switch (id)
            {
                case ParameterIds.BureaucratSalary:
                case ParameterIds.Productivity:
                    return 0.05;
                case ParameterIds.ChanceConsiderJobChange:
                case ParameterIds.IncomeTaxRate:
                case ParameterIds.OptimalBureaucracySize:
                case ParameterIds.PercentBureaucrats:
                    return 0.01;
                case ParameterIds.ParliamentSize:
                    return 1;
                case ParameterIds.PartyDiscipline:
                case ParameterIds.WelfareSubsidy:
                    return 0.1;
            }
            return 0.01;
        }

        private void Click_Up(object sender, RoutedEventArgs e)
        {
            Parameter p = (Parameter) ParamsList.SelectedItem;
            Event ev = new Event(p.Name + " Increase");
            ev.Effect = new ChangeParameterEffect(Game.TheGame.Country, p.Id, GetMultiplier(p.Id), GetAddend(p.Id));
            ev.HappensOnce = true;
            Game.TheGame.Events.Add(ev);
            ParamsList.InvalidateVisual();
        }

        private void Click_Down(object sender, RoutedEventArgs e)
        {
            Parameter p = (Parameter)ParamsList.SelectedItem;
            Event ev = new Event(p.Name + " Decrease");
            ev.Effect = new ChangeParameterEffect(Game.TheGame.Country, p.Id, 1 / GetMultiplier(p.Id), - GetAddend(p.Id));
            ev.HappensOnce = true;
            Game.TheGame.Events.Add(ev);
            ParamsList.InvalidateVisual();
        }
    }
}
