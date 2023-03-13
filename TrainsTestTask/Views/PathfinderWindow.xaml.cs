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
using System.Windows.Shapes;
using TrainsTestTask.ViewModels;

namespace TrainsTestTask.Views
{
    /// <summary>
    /// Логика взаимодействия для PathfinderWindow.xaml
    /// </summary>
    public partial class PathfinderWindow : Window
    {
        private Station station;

        public PathfinderWindow(Station station, Pathfinder pathfinder)
        {
            InitializeComponent();

            var viewModel = new PathfinderViewModel(station, pathfinder);
        }
    }
}
