using Accessibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private List<Point> itemsSourceLeft;
        public List<Point> ItemsSourceLeft // Parks, Костыль - TemplateBinding не привязывает SelectedItem
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

        private List<Point> itemsSourceRight;
        public List<Point> ItemsSourceRight
        {
            get { return itemsSourceRight; }
            set
            {
                itemsSourceRight = (List<Point>)value;
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
        }

        public void TryPathfind()
        {
            if (SelectedItemLeft is null || SelectedItemRight is null)
                return;

            // TODO remove previous track selection.
            // TODO add selected points highlight.

            var lowerBound = Lines.Count - currentTrack.Points.Count;
            for (var i = Lines.Count; i >= lowerBound; i--)
                Lines.RemoveAt(i);

            currentTrack = pathfinder.Find(SelectedItemLeft, SelectedItemRight);

            var newLines = new List<Line>();
            for (var i = 0; i < currentTrack.Points.Count - 1; i++)
                newLines.Add(ViewModelLinesHelper.GetLine(currentTrack.Points[i], currentTrack.Points[i + 1], Brushes.Red));

            newLines.ForEach(x => Lines.Add(x));
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
