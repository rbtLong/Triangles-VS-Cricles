using System.Windows.Controls;

namespace TrianglesVCircles.GameControls
{
	/// <summary>
	/// Interaction logic for BackgroundControl.xaml
	/// </summary>
	public partial class BackgroundControl : UserControl
	{
	    public static BackgroundControl _instance;

	    public static BackgroundControl Instance
	    {
            get { return _instance; }
	    }

		public BackgroundControl()
		{
			InitializeComponent();
		    _instance = this;
		}

	    public void LoadLevel0()
	    {
			
	    }

	    public void LoadMainMenu()
	    {

	    }
	}
}