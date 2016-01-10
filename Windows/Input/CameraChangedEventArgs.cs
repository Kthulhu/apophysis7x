using System;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Windows.Input
{
	[PublicAPI]
	public class CameraChangedEventArgs : EventArgs
	{
		[NotNull]
		public CameraInputOperation Operation { get; private set; }
		public CameraData Data { get; private set; }

		public CameraChangedEventArgs([NotNull] CameraInputOperation operation, [NotNull] CameraData data)
		{
			if (operation == null) throw new ArgumentNullException(nameof(operation));
			if (data == null) throw new ArgumentNullException(nameof(data));
			Operation = operation;
			Data = data;
		}
	}
}