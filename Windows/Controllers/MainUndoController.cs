using System;
using JetBrains.Annotations;
using Xyrus.Apophysis.Windows.Forms;

namespace Xyrus.Apophysis.Windows.Controllers
{
	class MainUndoController : Controller<Main>
	{
		private MainController mParent;

		public MainUndoController([NotNull] Main view, [NotNull] MainController parent) : base(view)
		{
			if (parent == null) throw new ArgumentNullException(nameof(parent));
			
			mParent = parent;
		}
		protected override void DisposeOverride(bool disposing)
		{
			mParent = null;
		}

		protected override void AttachView()
		{
			mParent.UndoController.StackChanged += OnUndoStackChanged;
			mParent.UndoController.ChangeCommitted += OnChangeCommitted;
		}
		protected override void DetachView()
		{
			mParent.UndoController.StackChanged -= OnUndoStackChanged;
			mParent.UndoController.ChangeCommitted -= OnChangeCommitted;
		}

		private void OnChangeCommitted(object sender, EventArgs e)
		{
			mParent.SetDirty();
		}
		private void OnUndoStackChanged(object sender, EventArgs e)
		{
			mParent.UpdateToolbar();
			mParent.UpdateMenu();
		}
	}
}