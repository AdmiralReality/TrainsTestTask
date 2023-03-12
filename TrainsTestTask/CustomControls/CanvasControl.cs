using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace TrainsTestTask.CustomControls
{
    public class CanvasControl : Control
    {
        public static readonly DependencyProperty ShapesProperty
			= DependencyProperty.Register(nameof(Shapes), typeof(List<Shape>), typeof(CanvasControl),
				new PropertyMetadata(() => new List<Shape>())); // TODO huh?


		private List<Shape> shapes;
		public List<Shape> Shapes
		{
			get { return (List<Shape>)GetValue(ShapesProperty); }
			set { SetValue(ShapesProperty, value); }
		}


	}
}
