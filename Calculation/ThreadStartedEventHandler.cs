using JetBrains.Annotations;

namespace Xyrus.Apophysis.Calculation
{
	[PublicAPI]
	public delegate void ThreadStartedEventHandler(object sender, ThreadStartedEventArgs args);
}