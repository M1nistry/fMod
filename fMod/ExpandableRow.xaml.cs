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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fMod
{
    /// <summary>
    /// Interaction logic for ExpandableRow.xaml
    /// </summary>
    public partial class ExpandableRow : UserControl
    {
        public ExpandableRow()
        {
            InitializeComponent();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ActualHeight <= 57)
            {
                var growAnimation = new DoubleAnimation
                {
                    From = 55,
                    To = 200,
                    FillBehavior = FillBehavior.HoldEnd,
                    BeginTime = TimeSpan.FromSeconds(0.1),
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                var growForm = new Storyboard
                {
                    Name = "ExpandForm"
                };
                growForm.Children.Add(growAnimation);
                Storyboard.SetTarget(growAnimation, this);
                Storyboard.SetTargetProperty(growAnimation, new PropertyPath(HeightProperty));
                growForm.Begin(this, true);
            }
            else
            {
                var shrinkAnimation = new DoubleAnimation
                {
                    From = 200,
                    To = 55,
                    FillBehavior = FillBehavior.Stop,
                    BeginTime = TimeSpan.FromSeconds(0.1),
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                var growForm = new Storyboard
                {
                    Name = "ShrinkForm"
                };
                growForm.Children.Add(shrinkAnimation);
                Storyboard.SetTarget(shrinkAnimation, this);
                Storyboard.SetTargetProperty(shrinkAnimation, new PropertyPath(HeightProperty));
                growForm.Begin(this, true);
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            SetResourceReference(BackgroundProperty, SystemColors.ControlLightBrushKey);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            SetResourceReference(BackgroundProperty, SystemColors.ControlBrushKey);
        }
    }
}
