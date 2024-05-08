using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
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
}
