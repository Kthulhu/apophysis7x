using System;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Calculation
{
	[PublicAPI]
	public class BitmapReadyEventArgs : EventArgs
	{
		public BitmapReadyEventArgs(float nextIssue)
		{
			NextIssue = nextIssue;
		}

		public float NextIssue { get; private set; }
	}
}