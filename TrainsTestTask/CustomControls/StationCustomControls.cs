using System;
using System.Collections;
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

namespace TrainsTestTask
{
    public class StationCustomControls : Control, INotifyPropertyChanged
    {
        public ObservableCollection<Shape> Shapes
        {
            get { return (ObservableCollection<Shape>)GetValue(ShapesProperty); }
            set
            {
                SetValue(ShapesProperty, value);
                OnPropertyChanged(nameof(Shapes));
            }
        }

        public static readonly DependencyProperty ShapesProperty =
            DependencyProperty.Register(nameof(Shapes), typeof(ObservableCollection<Shape>), typeof(StationCustomControls),
                new PropertyMetadata(default(ObservableCollection<Shape>)));

        #region Left

        public IEnumerable ItemsSourceLeft
        {
            get { return (IEnumerable)GetValue(ItemsSourceLeftProperty); }
            set { SetValue(ItemsSourceLeftProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceLeftProperty =
            DependencyProperty.Register(nameof(ItemsSourceLeft), typeof(IEnumerable), typeof(StationCustomControls),
                new PropertyMetadata(default(IEnumerable)));

        public object SelectedItemLeft
        {
            get { return GetValue(SelectedItemLeftProperty); }
            set { SetValue(SelectedItemLeftProperty, true); }
        }

        public static readonly DependencyProperty SelectedItemLeftProperty =
            DependencyProperty.Register(nameof(SelectedItemLeft), typeof(object), typeof(StationCustomControls),
                new PropertyMetadata(default(object)));

        #endregion Left

        #region Right
        public IEnumerable ItemsSourceRight
        {
            get { return (IEnumerable)GetValue(ItemsSourceRightProperty); }
            set { SetValue(ItemsSourceRightProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceRightProperty =
            DependencyProperty.Register(nameof(ItemsSourceRight), typeof(IEnumerable), typeof(StationCustomControls),
                new PropertyMetadata(default(IEnumerable)));

        public object SelectedItemRight
        {
            get { return GetValue(SelectedItemRightProperty); }
            set { SetValue(SelectedItemRightProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemRightProperty =
            DependencyProperty.Register(nameof(SelectedItemRight), typeof(object), typeof(StationCustomControls),
                new PropertyMetadata(default(object)));

        #endregion Right

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged is not null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
