# how-to-apply-ripple-effects-for-ListViewItem-in-.net-maui-SfListView

This repository contains a sample demonstrating how to apply ripple effect for ListViewItem in .NET MAUI ListView (SfListView).

## Sample

```xaml
<syncfusion:SfListView
            x:Name="listView"
            ItemSize="100"
            ItemsSource="{Binding BookInfo}">
    <syncfusion:SfListView.ItemTemplate>
        <DataTemplate>
            <core:SfEffectsView
                HighlightBackground="#FF0000"
                LongPressEffects="Scale,Selection"
                RippleAnimationDuration="800"
                ShouldIgnoreTouches="false"
                TouchDownEffects="Highlight,Ripple">
                <Grid Padding="10">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="0.4*" />
                        <RowDefinition Height="0.6*" />
                    </Grid.RowDefinitions>
                    <Label
                        FontAttributes="Bold"
                        FontSize="21"
                        Text="{Binding BookName}"
                        TextColor="Teal" />
                    <Label
                        Grid.Row="1"
                        FontSize="15"
                        Text="{Binding BookDescription}"
                        TextColor="Teal" />
                </Grid>
            </core:SfEffectsView>
        </DataTemplate>
    </syncfusion:SfListView.ItemTemplate>
</syncfusion:SfListView>
```

```c#
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

public class ItemGeneratorExt : ItemsGenerator
{
    public SfListView listView;

    public ItemGeneratorExt(SfListView listView) : base(listView)
    {
        this.listView = listView;
    }

    protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
    {
        if (type == ItemType.Record)
        {
            return new ListViewItemExt(this.listView);
        }

        return base.OnCreateListViewItem(itemIndex, type, data);
    }
}
```

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

