using System;
using System.Drawing;
using JetBrains.Annotations;
using Xyrus.Apophysis.Models;
using Xyrus.Apophysis.Threading;

namespace Xyrus.Apophysis.Calculation
{
	public class ThumbnailRenderer
	{
		public Bitmap CreateBitmap([NotNull] Flame flame, float density, Size size, ThreadStateToken threadState = null)
		{
			if (flame == null) throw new ArgumentNullException(nameof(flame));
			if (density <= 0) throw new ArgumentOutOfRangeException(nameof(density));
			if (size.Width <= 0 || size.Height <= 0) throw new ArgumentOutOfRangeException(nameof(size));

			var progress = new ProgressManager(threadState);

			using (var renderer = new Renderer(flame, size, 1, 0.5f, false))
			{
				renderer.Initialize();
				renderer.Histogram.Iterate(density, progress);

				return renderer.Histogram.CreateBitmap();
			}
		}
	}
}
