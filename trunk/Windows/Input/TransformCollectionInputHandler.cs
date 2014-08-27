using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xyrus.Apophysis.Windows.Visuals;
using Xyrus.Apophysis.Windows.Math;
using Xyrus.Apophysis.Windows.Models;

namespace Xyrus.Apophysis.Windows.Input
{
	[PublicAPI]
	public class TransformCollectionInputHandler : InputHandler, IEnumerable<TransformInputHandler>
	{
		private TransformUpdatedEventHandler mTransformUpdated;

		private EventHandler mBeginEdit;
		private EventHandler mEndEdit;

		private TransformCollectionVisual mVisualCollection;
		private List<TransformInputHandler> mHandlers;
		private Canvas mCanvas;

		public TransformCollectionInputHandler([NotNull] Control control, [NotNull] TransformCollectionVisual visualCollection, [NotNull] Canvas canvas) : base(control)
		{
			if (visualCollection == null) throw new ArgumentNullException("visualCollection");
			if (canvas == null) throw new ArgumentNullException("canvas");

			mVisualCollection = visualCollection;
			mCanvas = canvas;
			mVisualCollection.ContentChanged += OnCollectionChanged;

			mHandlers = new List<TransformInputHandler>();
		}
		protected override void DisposeOverride(bool disposing)
		{
			if (mVisualCollection != null)
			{
				mVisualCollection.ContentChanged += OnCollectionChanged;

				//disposed somewhere else
				mVisualCollection = null;
			}

			mCanvas = null;
		}

		public TransformCollection Collection
		{
			get { return mVisualCollection.Collection; }
		}
		public TransformInputHandler this[int index]
		{
			get
			{
				if (mHandlers == null)
					throw new IndexOutOfRangeException();

				if (index < 0 || index >= mHandlers.Count)
					throw new IndexOutOfRangeException();

				return mHandlers[index];
			}
		}

		private void OnCollectionChanged(object sender, EventArgs eventArgs)
		{
			if (mHandlers == null || mVisualCollection == null)
				return;

			foreach (var handler in mHandlers)
			{
				handler.Dispose();
			}

			mHandlers.Clear();

			foreach (var visual in mVisualCollection.Reverse())
			{
				mHandlers.Add(new TransformInputHandler(AttachedControl, visual, mCanvas));
			}

			InvalidateControl();
		}

		protected override bool OnAttachedControlMouseMove(Vector2 cursor, MouseButtons button)
		{
			if (mHandlers == null)
				return false;

			if (mHandlers.Any(x => x.IsDragging))
			{
				foreach (var handler in mHandlers.Where(x => x.IsDragging))
				{
					if (handler.HandleMouseMove(cursor, button))
					{
						var operation = handler.GetCurrentOperation();
						if (operation != null)
						{
							RaiseTransformUpdated(operation);
						}
						
						return true;
					}
				}

				return true;
			}

			foreach (var handler in mHandlers)
			{
				handler.InvalidateHitTest();
			}

			foreach (var handler in mHandlers)
			{
				if (handler.HandleMouseMove(cursor, button))
					return true;
			}

			return false;
		}
		protected override bool OnAttachedControlMouseWheel(double delta, MouseButtons button)
		{
			if (mHandlers == null)
				return false;

			foreach (var handler in mHandlers)
			{
				if (handler.HandleMouseWheel(delta, button))
					return true;
			}

			return false;
		}

		protected override bool OnAttachedControlMouseDown(Vector2 cursor)
		{
			if (mHandlers == null)
				return false;

			foreach (var visual in mVisualCollection)
			{
				visual.IsSelected = false;
			}

			foreach (var handler in mHandlers)
			{
				if (handler.HandleMouseDown(cursor))
				{
					RaiseBeginEdit();
					InvalidateControl();
					return true;
				}
			}

			return false;
		}
		protected override bool OnAttachedControlMouseUp()
		{
			if (mHandlers == null)
				return false;

			RaiseEndEdit();

			foreach (var handler in mHandlers)
			{
				if (handler.HandleMouseUp())
				{
					InvalidateControl();
					return true;
				}
			}

			return false;
		}

		protected override bool OnAttachedControlMouseDoubleClick()
		{
			if (mHandlers == null)
				return false;

			foreach (var handler in mHandlers)
			{
				if (handler.HandleMouseDoubleClick())
					return true;
			}

			return false;
		}

		protected void RaiseTransformUpdated([NotNull] TransformInputOperation operation)
		{
			if (mTransformUpdated != null)
				mTransformUpdated(this, new TransformUpdatedEventArgs(operation));
		}

		protected void RaiseBeginEdit()
		{
			if (mBeginEdit != null)
				mBeginEdit(this, new EventArgs());
		}
		protected void RaiseEndEdit()
		{
			if (mEndEdit != null)
				mEndEdit(this, new EventArgs());
		}

		public event TransformUpdatedEventHandler TransformUpdated
		{
			add { mTransformUpdated += value; }
			remove { mTransformUpdated -= value; }
		}

		public event EventHandler BeginEdit
		{
			add { mBeginEdit += value; }
			remove { mBeginEdit -= value; }
		}
		public event EventHandler EndEdit
		{
			add { mEndEdit += value; }
			remove { mEndEdit -= value; }
		}

		public IEnumerator<TransformInputHandler> GetEnumerator()
		{
			if (mHandlers == null)
				return new List<TransformInputHandler>().GetEnumerator();

			return mHandlers.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}