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
using System.Windows.Threading;
using System.Threading;

namespace Polity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        bool isRunning = false;
        public enum Speed { Slow = 5000000, Medium = 1000000, Fast = 100000 };
        Speed speed = Speed.Medium;
        DispatcherTimer dispatcher;

        public delegate void RunOneTurn();

        public MainWindow()
        {
            InitializeComponent();
            dispatcher = new DispatcherTimer(DispatcherPriority.SystemIdle);
            dispatcher.Tick += new EventHandler(TickHandler);
            GameSpeed = Speed.Slow;
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                if (value)
                {
                    dispatcher.Start();
                    //dispatcher.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new RunOneTurn(NextTurn));
                }
                else dispatcher.Stop();
            }
        }

        public Speed GameSpeed
        {
            get { return speed; }
            set
            {
                speed = value;
                dispatcher.Interval = new TimeSpan((long) speed);
            }
        }

        void TickHandler(object sender, EventArgs e)
        {
            NextTurn();
        }

        public void NextTurn()
        {
            Game.TheGame.NextTurn();
            Refresh();
        }
    }
}
