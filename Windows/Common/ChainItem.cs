using System.Windows.Forms;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Windows
{
	abstract class ChainItem<T> : ControlEventInterceptor<T> where T: Control
	{
		protected ChainItem([NotNull] T control) : base(control)
		{
		}
	}

	abstract class ChainItem : ChainItem<Control>
	{
		protected ChainItem([NotNull] Control control)
			: base(control)
		{
		}
	}
}