using System;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Messaging
{
	[PublicAPI]
	public class MessageEventArgs : EventArgs
	{
		public string Message { get; private set; }

		public MessageEventArgs(string message)
		{
			Message = message;
		}
	}
}