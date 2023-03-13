using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainsTestTask.PathfinderRestrictions;
using TrainsTestTask.Views;

namespace TrainsTestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Station station;
        private Window window;
        private Pathfinder pathfinder;

        public MainWindow()
        {
            InitializeComponent();

            var parser = new StationParser();
            station = parser.Parse("");

            var restrictions = new IPathfinderRestriction[]
            {
                new AllowOnlyDeadEndReverseRestriction(),
                new ForbidSharpCornerRestriction()
            };

            pathfinder = new Pathfinder(restrictions);

            DataContext = new ApplicationViewModel(station);
        }

        private void GetInsideDebug(object sender, RoutedEventArgs e)
        {
            // TODO remove
            ;
        }

        private void OpenPathfinderWindow(object sender, RoutedEventArgs e)
        {
            if (window is null || !window.IsLoaded)
                window = new PathfinderWindow(station, pathfinder);

            window.Show();
        }
    }
}
