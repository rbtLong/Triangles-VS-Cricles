using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace TrianglesVCircles.Problems.MusicTheory
{
    /// <summary>
    /// Interaction logic for TrebleNotes.xaml
    /// </summary>
    public partial class TrebleNotes : UserControl
    {
        public TrebleNotes()
        {
            InitializeComponent();
            setNote(Note);
        }

        public static readonly DependencyProperty NoteProperty = DependencyProperty.Register(
            "Note", typeof (TrebleNote), typeof (TrebleNotes), new PropertyMetadata(default(TrebleNote),
                trbleChanged));

        private static void trbleChanged(DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs arg)
        {
            var src = dependencyObject as TrebleNotes;
            src.setNote(src.Note);
        }

        private void setNote(TrebleNote note)
        {
            hideAllNotes();
            switch (note)
            {
                case TrebleNote.c:
                    c.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.d:
                    d.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.e:
                    e.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.f:
                    f.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.g:
                    g.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.a:
                    a.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.b:
                    b.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.c1:
                    c1.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.d1:
                    d1.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.e1:
                    e1.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.f1:
                    f1.Visibility = Visibility.Visible;
                    break;
                case TrebleNote.g1:
                    g1.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void hideAllNotes()
        {
            c.Visibility =
                d.Visibility =
                    e.Visibility =
                        f.Visibility =
                            g.Visibility =
                                a.Visibility =
                                    b.Visibility =
                                        c1.Visibility =
                                            d1.Visibility =
                                                e1.Visibility =
                                                    f1.Visibility =
                                                        g1.Visibility = Visibility.Collapsed;
        }

        public TrebleNote Note
        {
            get { return (TrebleNote) GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }
    }

    public enum TrebleNote
    {
        c,
        d,
        e,
        f,
        g,
        a,
        b,
        c1,
        d1,
        e1,
        f1,
        g1,
    }
}
