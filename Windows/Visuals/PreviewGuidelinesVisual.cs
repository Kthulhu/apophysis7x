﻿using System.Drawing;
using System.Windows.Forms;
using JetBrains.Annotations;
using Rectangle = System.Drawing.Rectangle;

namespace Xyrus.Apophysis.Windows.Visuals
{
	class PreviewGuidelinesVisual : ControlVisual<PictureBox>
	{
		private bool mVisible;
		private Size mImageSize;

		public PreviewGuidelinesVisual([NotNull] PictureBox control) : base(control)
		{
		}

		public bool Visible
		{
			get { return mVisible; }
			set
			{
				mVisible = value;
				InvalidateControl();
			}
		}
		public Size ImageSize
		{
			get { return mImageSize; }
			set { mImageSize = value; }
		}

		protected override void RegisterEvents(PictureBox control)
		{
		}
		protected override void UnregisterEvents(PictureBox control)
		{
		}

		protected override void OnControlPaint(Graphics graphics)
		{
			if (!Visible) 
				return;

			if (mImageSize.Width <= 0 || mImageSize.Height <= 0)
				return;

			var s = AttachedControl;

			Point p1, p2;
			const float ratio = 0.61803399f;

			p1 = new Point(s.Width / 2, 0);
			p2 = new Point(p1.X, s.Height);
			graphics.DrawLine(Pens.White, p1, p2);

			p1 = new Point(0, s.Height / 2);
			p2 = new Point(s.Width, p1.Y);
			graphics.DrawLine(Pens.White, p1, p2);

			p1 = new Point(s.Width / 3, 0);
			p2 = new Point(p1.X, s.Height);
			graphics.DrawLine(Pens.Red, p1, p2);

			p1 = new Point(0, s.Height / 3);
			p2 = new Point(s.Width, p1.Y);
			graphics.DrawLine(Pens.Red, p1, p2);

			p1 = new Point(2 * s.Width / 3, 0);
			p2 = new Point(p1.X, s.Height);
			graphics.DrawLine(Pens.Red, p1, p2);

			p1 = new Point(0, 2 * s.Height / 3);
			p2 = new Point(s.Width, p1.Y);
			graphics.DrawLine(Pens.Red, p1, p2);

			p1 = new Point((int)(ratio * s.Width), 0);
			p2 = new Point(p1.X, s.Height);
			graphics.DrawLine(Pens.LightGreen, p1, p2);

			p1 = new Point(0, (int)(ratio * s.Height));
			p2 = new Point(s.Width, p1.Y);
			graphics.DrawLine(Pens.LightGreen, p1, p2);

			p1 = new Point(s.Width - (int)(ratio * s.Width), 0);
			p2 = new Point(p1.X, s.Height);
			graphics.DrawLine(Pens.LightGreen, p1, p2);

			p1 = new Point(0, s.Height - (int)(ratio * s.Height));
			p2 = new Point(s.Width, p1.Y);
			graphics.DrawLine(Pens.LightGreen, p1, p2);

			var fractalSize = mImageSize.FitToFrame(s.ClientSize);
			var fractalRect = new Rectangle(new Point(s.ClientSize.Width / 2 - fractalSize.Width / 2, s.ClientSize.Height / 2 - fractalSize.Height / 2), fractalSize);

			p1 = new Point(fractalRect.Left, 0);
			p2 = new Point(fractalRect.Left, s.Height);

			if (fractalRect.Left > 0)
				graphics.DrawLine(Pens.Yellow, p1, p2);

			p1 = new Point(0, fractalRect.Top);
			p2 = new Point(s.Width, fractalRect.Top);

			if (fractalRect.Top > 0)
				graphics.DrawLine(Pens.Yellow, p1, p2);

			p1 = new Point(fractalRect.Right, 0);
			p2 = new Point(fractalRect.Right, s.Height);

			if (fractalRect.Right < s.Width - 1)
				graphics.DrawLine(Pens.Yellow, p1, p2);

			p1 = new Point(0, fractalRect.Bottom);
			p2 = new Point(s.Width, fractalRect.Bottom);

			if (fractalRect.Bottom < s.Height - 1)
				graphics.DrawLine(Pens.Yellow, p1, p2);
		}
	}
}