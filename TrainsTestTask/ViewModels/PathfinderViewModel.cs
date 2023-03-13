using Accessibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrainsTestTask.Helpers;

namespace TrainsTestTask.ViewModels
{
    class PathfinderViewModel : INotifyPropertyChanged
    {
        Station station;
        Pathfinder pathfinder;
        Track currentTrack;

        public Dictionary<(Point, Point), Line> LineBetweenPointsDict { get; set; } = new();

        private ObservableCollection<Shape> lines;
        public ObservableCollection<Shape> Lines
        {
            get { return lines; }
            private set
            {
                lines = value;
                OnPropertyChanged(nameof(Lines));
            }
        }

        private ObservableCollection<Point> itemsSourceLeft;
        public ObservableCollection<Point> ItemsSourceLeft // Parks, Костыль - TemplateBinding не привязывает SelectedItem
        {
            get { return itemsSourceLeft; }
            private set
            {
                itemsSourceLeft = value;
                OnPropertyChanged(nameof(ItemsSourceLeft));
            }
        }

        private Point selectedItemLeft;
        public Point SelectedItemLeft // Selected Park, Костыль - TemplateBinding не привязывает SelectedItem
        {
            get { return selectedItemLeft; }
            set
            {
                selectedItemLeft = (Point)value;
                OnPropertyChanged(nameof(SelectedItemLeft));
                TryPathfind();
            }
        }

        private ObservableCollection<Point> itemsSourceRight;
        public ObservableCollection<Point> ItemsSourceRight
        {
            get { return itemsSourceRight; }
            set
            {
                itemsSourceRight = value;
                OnPropertyChanged(nameof(ItemsSourceRight));
            }
        }

        private Point selectedItemRight;
        public Point SelectedItemRight
        {
            get { return selectedItemRight; }
            set
            {
                selectedItemRight = (Point)value;
                OnPropertyChanged(nameof(SelectedItemRight));
                TryPathfind();
            }
        }

        public PathfinderViewModel(Station station, Pathfinder pathfinder)
        {
            this.station = station;
            this.pathfinder = pathfinder;

            (var lines, LineBetweenPointsDict) = ViewModelLinesHelper.BuildLinesForRendering(station);
            Lines = new ObservableCollection<Shape>(lines);
            ItemsSourceLeft = new ObservableCollection<Point>(station.Points);
            ItemsSourceRight = new ObservableCollection<Point>(station.Points);
        }

        public void TryPathfind()
        {
            // keeping joints selection on top of the list

            // remove joints selection
            while (Lines.Last() is Ellipse ellipse)
                Lines.RemoveAt(Lines.Count - 1);

            // remove old path and add new one
            if (SelectedItemLeft is not null && SelectedItemRight is not null)
            {
                var lowerBound = currentTrack is null ? Lines.Count : Lines.Count - currentTrack.Points.Count;

                for (var i = Lines.Count - 1; i > lowerBound; i--)
                    Lines.RemoveAt(i);

                currentTrack = pathfinder.Find(SelectedItemLeft, SelectedItemRight);

                if (currentTrack is not null)
                {

                    var newLines = new List<Line>();
                    for (var i = 0; i < currentTrack.Points.Count - 1; i++)
                        newLines.Add(ViewModelLinesHelper.GetLine(currentTrack.Points[i], currentTrack.Points[i + 1], Brushes.Red));

                    newLines.ForEach(x => Lines.Add(x));
                }
            }

            // add new points selection (works even if there is no path currently)
            foreach (var (point, brush) in new List<(Point, Brush)> { (SelectedItemLeft, Brushes.Blue), (SelectedItemRight, Brushes.Green) })
            {
                if (point is null)
                    continue;

                var height = 10;
                var width = 10;

                var ellipse = new Ellipse() { Height = height, Width = width, Stroke = brush, Fill = brush };
                Canvas.SetLeft(ellipse, point.X - width / 2);
                Canvas.SetTop(ellipse, point.Y - height / 2);
                Lines.Add(ellipse);
            }
                
        }

        #region Interface implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged is not null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion Interface implementation
    }
}
