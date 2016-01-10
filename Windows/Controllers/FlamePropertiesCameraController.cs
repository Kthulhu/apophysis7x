﻿using System;
using System.Numerics;
using JetBrains.Annotations;
using Xyrus.Apophysis.Windows.Forms;

namespace Xyrus.Apophysis.Windows.Controllers
{
	class FlamePropertiesCameraController : DataInputController<FlameProperties>
	{
		private FlamePropertiesController mParent;

		public FlamePropertiesCameraController(FlameProperties view, [NotNull] FlamePropertiesController parent) 
			: base(view, parent.Initializer)
		{
			if (parent == null) throw new ArgumentNullException(nameof(parent));
			mParent = parent;
		}
		protected override void DisposeOverride(bool disposing)
		{
			mParent = null;
		}

		protected override void AttachView()
		{
			Register(View.ZoomDragPanel,
				xx => mParent.Flame.Zoom = xx,
				() => mParent.Flame.Zoom);
			Register(View.XPositionDragPanel,
				xx => mParent.Flame.Origin = new Vector2(xx, mParent.Flame.Origin.Y),
				() => mParent.Flame.Origin.X);
			Register(View.YPositionDragPanel,
				xx => mParent.Flame.Origin = new Vector2(mParent.Flame.Origin.X, xx),
				() => mParent.Flame.Origin.Y);
			Register(View.RotationDragPanel,
				xx => mParent.Flame.Angle = -xx * Float.Pi / 180.0f,
				() => mParent.Flame.Angle * -180 / Float.Pi);
			Register(View.ScaleDragPanel,
				xx => mParent.Flame.PixelsPerUnit = xx * mParent.Flame.CanvasSize.Width / 100,
				() => mParent.Flame.PixelsPerUnit * 100.0f / mParent.Flame.CanvasSize.Width);


			Register(View.ZoomScrollBar,
				xx => mParent.Flame.Zoom = xx / 1000,
				() => mParent.Flame.Zoom * 1000);
			Register(View.XPositionScrollBar,
				xx => mParent.Flame.Origin = new Vector2(xx/1000, mParent.Flame.Origin.Y),
				() => mParent.Flame.Origin.X * 1000);
			Register(View.YPositionScrollBar,
				xx => mParent.Flame.Origin = new Vector2(mParent.Flame.Origin.X, xx/1000),
				() => mParent.Flame.Origin.Y * 1000);
			Register(View.RotationScrollBar,
				xx => mParent.Flame.Angle = -xx * Float.Pi / 180.0f,
				() => mParent.Flame.Angle * -180 / Float.Pi);
		}
		protected override void DetachView()
		{
			Cleanup();
		}

		protected override void OnValueCommittedOverride(object control)
		{
			mParent.CommitValue();
		}
		protected override void OnValueChangedOverride(object control)
		{
			mParent.PreviewController.DelayedUpdatePreview();
		}
	}
}