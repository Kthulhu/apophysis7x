﻿using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using JetBrains.Annotations;
using Rectangle = Xyrus.Apophysis.Math.Rectangle;

namespace Xyrus.Apophysis.Windows.Visuals
{
	abstract class CanvasVisual<T> : ControlVisual where T: Canvas
	{
		private T mCanvas;

		private Color mGridZeroLineColor;
		private Color mGridLineColor;
		private Color mBackdropColor;

		protected CanvasVisual([NotNull] Control control, [NotNull] T canvas) : base(control)
		{
			if (canvas == null) throw new ArgumentNullException(nameof(canvas));
			mCanvas = canvas;
		}
		protected override void DisposeOverride(bool disposing)
		{
			mCanvas = null;
		}

		public T Canvas
		{
			get { return mCanvas; }
		}
		public Color GridLineColor
		{
			get { return mGridLineColor; }
			set
			{
				mGridLineColor = value;
				InvalidateControl();
			}
		}
		public Color GridZeroLineColor
		{
			get { return mGridZeroLineColor; }
			set
			{
				mGridZeroLineColor = value;
				InvalidateControl();
			}
		}
		public Color BackdropColor
		{
			get { return mBackdropColor; }
			set
			{
				mBackdropColor = value;
				InvalidateControl();
			}
		}

		protected Rectangle GetWorldBounds(Vector2 snapScale)
		{
			var u = Canvas.CanvasToWorld(new Vector2());
			var v = Canvas.CanvasToWorld(Canvas.Size);

			var c0 = Canvas.Snap(u, snapScale, CanvasSnapBehavior.Floor, CanvasSnapBehavior.Ceil);
			var c1 = Canvas.Snap(v, snapScale, CanvasSnapBehavior.Ceil, CanvasSnapBehavior.Floor);

			return new Rectangle(c0, c1 - c0);
		}
		protected Rectangle GetCanvasBounds(Vector2 snapScale)
		{
			var wb = GetWorldBounds(snapScale);

			var c0 = Canvas.WorldToCanvas(wb.TopLeft);
			var c1 = Canvas.WorldToCanvas(wb.BottomRight);

			return new Rectangle(c0, c1 - c0);
		}

		protected sealed override void RegisterEvents(Control control)
		{
			control.Resize += OnCanvasResize;
		}
		protected sealed override void UnregisterEvents(Control control)
		{
			control.Resize -= OnCanvasResize;
		}

		private void OnCanvasResize(object sender, EventArgs e)
		{
			var control = sender as Control;
			if (control == null)
				return;

			if (control.Width <= 0 || control.Height <= 0)
				return;

			var oldSize = mCanvas.Size;

			mCanvas.Resize(new Vector2(control.Width, control.Height));
			OnCanvasResize(oldSize, mCanvas.Size);
			InvalidateControl();
		}
		protected virtual void OnCanvasResize(Vector2 oldSize, Vector2 newSize)
		{
		}
	}
}