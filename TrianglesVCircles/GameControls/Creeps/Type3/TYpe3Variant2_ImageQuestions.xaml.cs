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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrianglesVCircles.Core.Creeps.Type3;

namespace TrianglesVCircles.GameControls.Creeps.Type3
{
    /// <summary>
    /// Interaction logic for TYpe3Variant2_ImageQuestions.xaml
    /// </summary>
    public partial class TYpe3Variant2_ImageQuestions : BaseEnemyControl
    {
        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
            "Location", typeof (Uri), typeof (TYpe3Variant2_ImageQuestions), new PropertyMetadata(default(Uri)));

        public Uri Location
        {
            get { return (Uri) GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public TYpe3Variant2_ImageQuestions()
        {
            InitializeComponent();
            Loaded += TYpe3Variant2_ImageQuestions_Loaded;
        }

        void TYpe3Variant2_ImageQuestions_Loaded(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Type3Variant2;
            Location = model.Data.ImgLocation;
        }
    }
}
