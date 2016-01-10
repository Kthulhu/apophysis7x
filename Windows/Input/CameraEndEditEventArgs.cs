using System;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Windows.Input
{
	[PublicAPI]
	public class CameraEndEditEventArgs : EventArgs
	{
		[NotNull]
		public CameraData Data { get; private set; }
		public bool EditMade { get; private set; }

		public CameraEndEditEventArgs([NotNull] CameraData data, bool editMade)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));
			Data = data;
			EditMade = editMade;
		}
	}
}