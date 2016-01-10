using System;
using System.Drawing;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Windows.Visuals
{
	abstract class ControlVisual<T> : ChainItem<T> where T: Control
	{
		protected ControlVisual([NotNull] T control) : base(control)
		{
		}
		protected abstract void OnControlPaint(Graphics graphics);

		public void Paint([NotNull] Graphics graphics)
		{
			if (graphics == null) throw new ArgumentNullException(nameof(graphics));
			OnControlPaint(graphics);
		}
	}

	abstract class ControlVisual : ControlVisual<Control>
	{
		protected ControlVisual([NotNull] Control control) : base(control)
		{
		}
	}
}