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
using TrianglesVCircles.Helpers;
using TrianglesVCircles.Problems.MusicTheory;

namespace TrianglesVCircles.GameControls.Creeps.Type3
{
    /// <summary>
    /// Interaction logic for Type3Variant1_TrebleNotes.xaml
    /// </summary>
    public partial class Type3Variant1_TrebleNotes : BaseEnemyControl
    {
        public Type3Variant1_TrebleNotes()
        {
            InitializeComponent();
            Loaded += Type3Variant1_TrebleNotes_Loaded;
        }

        void Type3Variant1_TrebleNotes_Loaded(object sender, RoutedEventArgs e)
        {
            var model = Model as Type3Variant1;
            var binding = BindingHelper.Create("Note", model);
            binding.Mode = BindingMode.OneWay;
            SetBinding(NoteProperty, binding);
        }

        public static readonly DependencyProperty NoteProperty = DependencyProperty.Register(
            "Note", typeof (TrebleNote), typeof (Type3Variant1_TrebleNotes), new PropertyMetadata(default(TrebleNote)));

        public TrebleNote Note
        {
            get { return (TrebleNote) GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }
    }
}
