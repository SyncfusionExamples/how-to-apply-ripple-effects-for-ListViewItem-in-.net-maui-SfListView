# How to apply ripple effect on items in .NET MAUI ListView?

The [.NET MAUI ListView](https://www.syncfusion.com/maui-controls/maui-listView) enhances user interaction by adding a ripple effect using the [.NET MAUI EffectsView](https://help.syncfusion.com/maui/effects-view/getting-started) to ListView items. 

**XAML:**

Integrate the ripple effect by using SfEffectsView as the content of ItemTemplate and set TouchDownEffects to "Ripple."

```
   <Grid>
      <syncfusion:SfListView x:Name="listView"
                             ItemsSource="{Binding BookInfo}"
                             ItemSize="100">
          <syncfusion:SfListView.ItemTemplate>
              <DataTemplate>
                  <core:SfEffectsView ShouldIgnoreTouches="false"
                                      HighlightBackground="#FF0000"
                                      RippleAnimationDuration="800"
                                      TouchDownEffects="Ripple">
                      <Grid Padding="10">
                          <Grid.RowDefinitions>
                              <RowDefinition Height="0.4*" />
                              <RowDefinition Height="0.6*" />
                          </Grid.RowDefinitions>
                          <Label Text="{Binding BookName}"
                                 FontAttributes="Bold"
                                 TextColor="Teal"
                                 FontSize="21" />
                          <Label Grid.Row="1"
                                 Text="{Binding BookDescription}"
                                 TextColor="Teal"
                                 FontSize="15" />
                      </Grid>
                  </core:SfEffectsView>
              </DataTemplate>
          </syncfusion:SfListView.ItemTemplate>
      </syncfusion:SfListView>
  </Grid>
 ```


**Note** 
On Android, when a child view (SfEffectsView) handles touch events, it prevents touch propagation to the parent element (ListViewItem). This affects built-in touch interactions like selection and swiping. This can be resolved by creating a custom ListViewItem and manually managing touch interactions.

**C#**
Custom ItemsGenerator class:
 ```
 public class ItemGeneratorExt : ItemsGenerator
 {
     private SfListView listView;
     public ItemGeneratorExt(SfListView listView) : base(listView)
     {
	this.listView = listView;
     }
     
     protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
     {
	  if (type == ItemType.Record)
	     return new ListViewItemExt(this.listView);
	     
	  return base.OnCreateListViewItem(itemIndex, type, data);
     }
 }
 ```


Custom ListViewItem class:
 ```
 public class ListViewItemExt : ListViewItem, ITouchListener, ITapGestureListener, ILongPressGestureListener
 {
	private SfListView listView;
	private SfEffectsView effectsView;

	public ListViewItemExt(SfListView listView)
	{
		this.listView = listView;
	}

	protected override void OnChildAdded(Element child)
	{
		base.OnChildAdded(child);
		if (child is SfEffectsView)
		{
			effectsView = child as SfEffectsView;
			effectsView.RemoveTouchListener(effectsView);
			effectsView.RemoveGestureListener(effectsView);
			effectsView.AnimationCompleted += EffectsView_AnimationCompleted;
		}
	}
	
	private void EffectsView_AnimationCompleted(object? sender, EventArgs e)
	{
		effectsView.Reset();
	}
	
	void ITouchListener.OnTouch(Syncfusion.Maui.Core.Internals.PointerEventArgs e)
	{
		effectsView.OnTouch(e);
		base.OnTouch(e);
	}
	
	void ITapGestureListener.OnTap(TapEventArgs e)
	{
		effectsView.OnTap(e);
		base.OnTap(e);
	}

	void ILongPressGestureListener.OnLongPress(LongPressEventArgs e)
	{
		effectsView.OnLongPress(e);
		base.OnLongPress(e);
	}
}
 ```

**XAML.cs**

Assign the instance of the ItemsGeneratorExt class to the **ItemsGenerator** property of ListView.

 ```csharp
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
 ```

**Conclusion:**

I hope you enjoyed learning how to apply a ripple effect to Items in .NET MAUI ListView.

You can refer to our [.NET MAUI ListView](https://www.syncfusion.com/maui-controls/maui-listView) feature tour page to know about its other groundbreaking feature representations and [documentation](https://help.syncfusion.com/maui/listview/getting-started), and how to quickly get started with configuration specifications. 

For current customers, you can check out our components from the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion®, try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to check out our other controls.

Please let us know in the comments section if you have any queries or require clarification. You can also contact us through our [support forums](https://www.syncfusion.com/forums/), [Direct-Trac](https://support.syncfusion.com/create), or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sflistview). We are always happy to assist you!
