using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RadiantTulip.View
{
    public class DraggableThumbSlider : Slider
    {
        public double FinalValue
        {
            get { return (double)GetValue(FinalValueProperty); }
            set { SetValue(FinalValueProperty, value); }
        }

        public static readonly DependencyProperty FinalValueProperty =
            DependencyProperty.Register("FinalValue", typeof(double), typeof(DraggableThumbSlider), new PropertyMetadata(0.0));

        protected override void OnThumbDragCompleted(System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            base.OnThumbDragCompleted(e);
            FinalValue = Value;
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            FinalValue = Value;
        }
    }
}
