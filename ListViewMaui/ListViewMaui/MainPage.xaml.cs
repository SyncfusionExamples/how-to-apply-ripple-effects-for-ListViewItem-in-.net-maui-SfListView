namespace ListViewMaui
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
#if ANDROID
			this.listView.ItemGenerator = new ItemGeneratorExt(this.listView);
#endif
		}

	}

}
