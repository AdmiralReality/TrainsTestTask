using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrainsTestTask.Commands;
using TrainsTestTask.Helpers;

namespace TrainsTestTask.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        Station station;
        Dictionary<(Point, Point), Line> lineBetweenPointsDict = new();

        // general-purpose lines of station for rendering.
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

        // park selection property.
        private List<Park> itemsSourceLeft;
        public List<Park> ItemsSourceLeft // Parks, Костыль - TemplateBinding не привязывает SelectedItem
        {
            get { return itemsSourceLeft; }
            private set
            {
                itemsSourceLeft = value;
                OnPropertyChanged(nameof(ItemsSourceLeft));
            }
        }

        private Park selectedItemLeft;
        public Park SelectedItemLeft // Selected Park, Костыль - TemplateBinding не привязывает SelectedItem
        {
            get { return selectedItemLeft; }
            set
            {
                selectedItemLeft = (Park)value;
                OnPropertyChanged(nameof(SelectedItemLeft));
                TryBuildPolygon();
            }
        }

        private List<Brush> itemsSourceRight;
        public List<Brush> ItemsSourceRight
        {
            get { return itemsSourceRight; }
            set
            {
                itemsSourceRight = (List<Brush>)value;
                OnPropertyChanged(nameof(ItemsSourceRight));
            }
        }

        private Brush selectedItemRight;
        public Brush SelectedItemRight
        {
            get { return selectedItemRight; }
            set
            {
                selectedItemRight = (Brush)value;
                OnPropertyChanged(nameof(SelectedItemRight));
                TryBuildPolygon();
                TryChangePolygonColor();
            }
        }

        public ApplicationViewModel(Station station)
        {
            this.station = station;
            ItemsSourceLeft = this.station.Parks;
            (var lines, lineBetweenPointsDict) = ViewModelLinesHelper.BuildLinesForRendering(this.station);

            Lines = new ObservableCollection<Shape>(lines);

            ItemsSourceRight = new List<Brush>()
            {
                Brushes.Black, Brushes.Red, Brushes.Green, Brushes.Blue
            };
        }

        private void TryBuildPolygon()
        {
            if (SelectedItemLeft is null || SelectedItemRight is null)
                return;

            var park = SelectedItemLeft;
            var points = park.GetPolygonPoints();

            var renderPolygon = new System.Windows.Shapes.Polygon();
            renderPolygon.Stroke = SelectedItemRight ?? ItemsSourceRight.First();
            renderPolygon.Fill = SelectedItemRight ?? ItemsSourceRight.First();
            renderPolygon.Points = new PointCollection(points.Select(point => new System.Windows.Point(point.X, point.Y)));
            renderPolygon.Opacity = 0.2;

            while (Lines.Any() && Lines.FirstOrDefault() is System.Windows.Shapes.Polygon)
                Lines.RemoveAt(0);

            Lines.Insert(0, renderPolygon);
        }

        private void TryChangePolygonColor()
        {
            if (!Lines.Any() || SelectedItemLeft is null || SelectedItemRight is null)
                return;

            var polygon = Lines.First() as Polygon;
            polygon.Stroke = SelectedItemRight;
            polygon.Fill = SelectedItemRight;
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
