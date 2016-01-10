using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using JetBrains.Annotations;
using Xyrus.Apophysis.Calculation;
using Xyrus.Apophysis.Models;

namespace Xyrus.Apophysis.Windows.Visuals
{
	class IteratorPreviewVisual : CanvasVisual<Canvas>
	{
		private IEnumerable<IteratorVisual> mCollection;
		private float mRange;
		private float mDensity;
		private bool mApplyPostTransform;

		public IteratorPreviewVisual([NotNull] Control control, [NotNull] Canvas canvas, [NotNull] IEnumerable<IteratorVisual> collection) : base(control, canvas)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));

			mCollection = collection;
			mRange = 1;
			mDensity = 1;
			mApplyPostTransform = true;
		}
		protected override void DisposeOverride(bool disposing)
		{
			mCollection = null;
		}

		public float Range
		{
			get { return mRange; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException();
				mRange = value;
			}
		}
		public float Density
		{
			get { return mDensity; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException();
				mDensity = value;
			}
		}
		public bool ApplyPostTransform
		{
			get { return mApplyPostTransform; }
			set { mApplyPostTransform = value; }
		}

		private Vector2 IterateSample(Iterator model, Vector2 point, IEnumerable<Variation> variations, IterationData data)
		{
			point = model.PreAffine.Transform(point);

			data.PreX = point.X;
			data.PreY = point.Y;
			data.PreZ = 0;
			data.PostX = 0;
			data.PostY = 0;
			data.PostZ = 0;

			foreach (var variation in variations)
			{
				variation.Calculate(data);
			}

			point.X = data.PostX;
			point.Y = data.PostY;

			if (mApplyPostTransform)
				point = model.PostAffine.Transform(point);

			return point;
		}
		private void DrawModel(Graphics graphics, Iterator model)
		{
			var variations = model.Variations.GetOrderedForExecution().Where(x => System.Math.Abs(x.Weight) > float.Epsilon).ToArray();
			foreach (var variation in variations)
			{
				variation.Prepare();
			}

			var n = Range;
			var d1 = Density * 5;

			var p1 = new Vector2(-n, -n);
			var p2 = new Vector2(n, n);

			var step = 1 / d1;
			var data = new IterationData();

			using (var brush = new SolidBrush(Color.FromArgb(0x80, model.GetColor())))
			//using (var pen = new Pen(brush))
			{
				//Vector2 lastPoint = null;

				var plot = new Action<float, float>((x, y) =>
				{
					var vout = IterateSample(model, new Vector2(x, y), variations, data);
					var pos = Canvas.WorldToCanvas(vout);

					// ReSharper disable once AccessToModifiedClosure
					//var pA = (lastPoint ?? pos).ToPoint();
					var pB = pos.ToPoint();

					// ReSharper disable once AccessToDisposedClosure
					graphics.FillEllipse(brush, pB.X - 1, pB.Y - 1, 3, 3);
					//graphics.DrawLine(pen, pA, pB);

					//lastPoint = pos;
				});

				for (float y = p1.Y; y <= p2.Y; y += step)
				{
					//lastPoint = null;

					for (float x = p1.X; x <= p2.X; x += step)
					{
						plot(x, y);
					}
				}
			}
		}

		protected override void OnControlPaint(Graphics graphics)
		{
			foreach (var iterator in mCollection)
			{
				if (iterator.IsSelected)
				{
					DrawModel(graphics, iterator.Model);
				}
			}
		}
	}
}