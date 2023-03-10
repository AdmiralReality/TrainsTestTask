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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainsTestTask.PathfinderRestrictions;

namespace TrainsTestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Station testStation;
        private Track testTrack;

        private void LoadPremadeData(object sender, RoutedEventArgs e)
        {
            #region simpleJunction
            //var p01 = new Point() { X = 20, Y = 20 };
            //var p02 = new Point() { X = 60, Y = 20 };
            //var p03 = new Point() { X = 80, Y = 20 };
            //var p04 = new Point() { X = 120, Y = 20 };
            //var p05 = new Point() { X = 160, Y = 20 };
            //var p06 = new Point() { X = 200, Y = 20 };
            //var p07 = new Point() { X = 240, Y = 20 };
            //var p08 = new Point() { X = 260, Y = 20 };

            //var p11 = new Point() { X = 20, Y = 80 };
            //var p12 = new Point() { X = 60, Y = 80 };
            //var p13 = new Point() { X = 100, Y = 80 };
            //var p14 = new Point() { X = 120, Y = 80 };
            //var p15 = new Point() { X = 160, Y = 80 };
            //var p16 = new Point() { X = 180, Y = 80 };
            //var p17 = new Point() { X = 220, Y = 80 };
            //var p18 = new Point() { X = 260, Y = 80 };

            //p01.AddDestination(p02);
            //p02.AddDestination(p03);
            //p03.AddDestination(p04, p13);
            //p04.AddDestination(p05);
            //p05.AddDestination(p06);
            //p06.AddDestination(p07);
            //p07.AddDestination(p08);

            //p18.AddDestination(p17);
            //p17.AddDestination(p16);
            //p16.AddDestination(p06, p15);
            //p15.AddDestination(p14, p16);
            //p14.AddDestination(p13, p15);
            //p13.AddDestination(p12, p14);
            //p12.AddDestination(p11);

            //point = p01;
            //point.Draw(CustomCanvas);
            #endregion simpleJunction

            var parser = new StationParser();
            testStation = parser.Parse("/file/path");
            testStation.Draw(CustomCanvas);
            testTrack = testStation.SavedTracks[0];
        }

        private void HighlightTrack(object sender, RoutedEventArgs e)
        {
            if (testTrack is null)
                return;

            if (testTrack.IsHighlighted)
                testTrack.RemoveHighlight();
            else
                testTrack.Highlight();
        }

        private void SearchAndHighlight(object sender, RoutedEventArgs e)
        {
            var pathfinder = new Pathfinder(new IPathfinderRestriction[]
            {
                new ForbidSharpCornerRestriction(),
                new AllowOnlyDeadEndReverseRestriction()
            });

            //var track = pathfinder.Find(testStation.Points[6], testStation.Points[25]);
            var track = pathfinder.Find(testStation.Points[6], testStation.Points[21]); // dead-end track

            if (track is not null)
            {
                testTrack = track;
                testTrack.Highlight();
            }
        }

        private void HighlightPremadePark(object sender, RoutedEventArgs e)
        {
            testStation.Parks.First().DrawPolygon(CustomCanvas);
        }
    }
}
