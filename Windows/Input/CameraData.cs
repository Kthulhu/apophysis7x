using System;
using System.Numerics;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Windows.Input
{
	[PublicAPI]
	public class CameraData
	{
		private Vector2 mOrigin;

		public Vector2 Origin
		{
			get { return mOrigin; }
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				mOrigin = value;
			}
		}

		public float Angle { get; set; }
		public float Zoom { get; set; }
		public float Scale { get; set; }
	}
}