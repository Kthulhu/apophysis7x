﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Xyrus.Apophysis.Windows.Visuals;
using Xyrus.Apophysis.Windows.Input;
using Xyrus.Apophysis.Windows.Math;
using Xyrus.Apophysis.Windows.Models;
using Rectangle = System.Drawing.Rectangle;

namespace Xyrus.Apophysis.Windows.Controls
{
	public partial class EditorCanvas : UserControl
	{
		private ControlVisualChain mVisual;
		private InputHandlerChain mInteraction;

		private GridVisual mGridPainter;
		private GridRulerVisual mRulerPainter;
		private TransformCollectionVisual mTransformPainter;

		private GridInputStrategy mGridInteraction;
		private TransformCollectionInputHandler mTransformInteraction;

		private EditorGridContextMenu mGridContextMenu;
		private EditorCommands mCommands;

		private EventHandler mBeginEdit;
		private EventHandler mEndEdit;

		private bool mIsDisposed;

		public EditorCanvas()
		{
			InitializeComponent();
			SuspendLayout();

			var grid = new Grid(new Vector2(Width, Height));

			mVisual = new ControlVisualChain(this);
			mInteraction = new InputHandlerChain(this);
			
			mVisual.Add(mGridPainter = new GridVisual(this, grid));
			mVisual.Add(mTransformPainter = new TransformCollectionVisual(this, grid), 100);
			mVisual.Add(mRulerPainter = new GridRulerVisual(this, grid), int.MaxValue);

			mInteraction.Add(mGridInteraction = new GridInputStrategy(this, grid), int.MaxValue);
			mInteraction.Add(mTransformInteraction = new TransformCollectionInputHandler(this, mTransformPainter, grid), 100);

			mTransformInteraction.BeginEdit += OnBeginEdit;
			mTransformInteraction.EndEdit += OnEndEdit;

			GridLineColor = Color.FromArgb(0xff, 0x66, 0x66, 0x66);
			BackdropColor = Color.Transparent;
			GridZeroLineColor = Color.FromArgb(0xff, 0xbb, 0xbb, 0xbb);

			RulerGridLineColor = Color.Gray;
			RulerBackgroundColor = Color.FromArgb(0xff, 0x33, 0x33, 0x33);
			RulerBackdropColor = Color.Transparent;

			ShowRuler = true;

			mCommands = new EditorCommands(this);
			mGridContextMenu = new EditorGridContextMenu(this);

			Settings.UnbindContextMenu();
			Settings.BindContextMenu(mGridContextMenu);

			MouseClick += OnCanvasMouseClick;
			ResumeLayout(false);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && !mIsDisposed)
			{
				MouseClick -= OnCanvasMouseClick;

				if (components != null)
				{
					components.Dispose();
				}

				if (mTransformInteraction != null)
				{
					mTransformInteraction.BeginEdit -= OnBeginEdit;
					mTransformInteraction.EndEdit -= OnEndEdit;
				}

				if (mInteraction != null)
				{
					mInteraction.Dispose();
					mInteraction = null;
				}

				if (mVisual != null)
				{
					mVisual.Dispose();
					mVisual = null;
				}

				if (mGridContextMenu != null)
				{
					mGridContextMenu.Dispose();
					mGridContextMenu = null;
				}

				if (mCommands != null)
				{
					mCommands.Dispose();
					mCommands = null;
				}

				mIsDisposed = true;
			}

			mGridPainter = null;
			mRulerPainter = null;
			mTransformPainter = null;
			mGridInteraction = null;
			mTransformInteraction = null;

			base.Dispose(disposing);
		}

		public TransformCollection Transforms
		{
			get { return mTransformPainter.Collection; }
			set { mTransformPainter.Collection = value; }
		}

		public Color GridZeroLineColor
		{
			get { return mGridPainter.GridZeroLineColor; }
			set { mGridPainter.GridZeroLineColor = value; }
		}
		public Color GridLineColor
		{
			get { return mGridPainter.GridLineColor; }
			set { mGridPainter.GridLineColor = value; }
		}
		public Color BackdropColor
		{
			get { return mGridPainter.BackdropColor; }
			set { mGridPainter.BackdropColor = value; }
		}

		public Color RulerGridLineColor
		{
			get { return mRulerPainter.GridLineColor; }
			set { mRulerPainter.GridLineColor = value; }
		}
		public Color RulerBackdropColor
		{
			get { return mRulerPainter.BackdropColor; }
			set { mRulerPainter.BackdropColor = value; }
		}
		public Color RulerBackgroundColor
		{
			get { return mRulerPainter.BackgroundColor; }
			set { mRulerPainter.BackgroundColor = value; }
		}

		public Color ReferenceColor
		{
			get { return mTransformPainter.ReferenceColor; }
			set { mTransformPainter.ReferenceColor = value; }
		}

		[Browsable(false)]
		public EditorCommands Commands
		{
			get { return mCommands; }
		}

		[NotNull]
		public EditorSettings Settings
		{
			get { return mTransformInteraction.Settings; }
			set
			{
				mTransformInteraction.Settings.UnbindContextMenu();
				mTransformInteraction.Settings = value;
				mTransformInteraction.Settings.BindContextMenu(mGridContextMenu);
			}
		}

		public Vector2 CursorPosition
		{
			get { return mGridInteraction.Canvas.CanvasToWorld(mInteraction.CursorPosition); }
		}

		public bool ShowRuler
		{
			get { return mRulerPainter.ShowLabels || mRulerPainter.ShowVertical || mRulerPainter.ShowHorizontal; }
			set
			{
				mRulerPainter.ShowLabels = value;
				mRulerPainter.ShowHorizontal = value;
				mRulerPainter.ShowVertical = value;

				Point lt, rb;

				if (value)
				{
					lt = new Point(mRulerPainter.RulerSize + 10, mRulerPainter.RulerSize + 10);
					rb = new Point(10, 10);
				}
				else
				{
					lt = new Point(10, 10);
					rb = new Point(10, 10);
				}

				mTransformPainter.HintTextRectangle = new Rectangle(lt.X, lt.Y, rb.X - lt.X, rb.Y - lt.Y);
				Refresh();
			}
		}
		public bool HighlightOrigin
		{
			get { return mGridPainter.HighlightOrigin; }
			set { mGridPainter.HighlightOrigin = value; }
		}

		public void ZoomOptimally()
		{
			if (mTransformInteraction == null)
				return;

			mTransformInteraction.ZoomOptimally();
		}

		private void OnBeginEdit(object sender, EventArgs args)
		{
			RaiseBeginEdit();
		}
		protected void RaiseBeginEdit()
		{
			if (mBeginEdit != null)
				mBeginEdit(this, new EventArgs());
		}

		private void OnEndEdit(object sender, EventArgs args)
		{
			RaiseEndEdit();
		}
		protected void RaiseEndEdit()
		{
			if (mEndEdit != null)
				mEndEdit(this, new EventArgs());
		}

		public event EventHandler BeginEdit
		{
			add { mBeginEdit += value; }
			remove { mBeginEdit -= value; }
		}
		public event EventHandler EndEdit
		{
			add { mEndEdit += value; }
			remove { mEndEdit -= value; }
		}

		private void OnCanvasMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			var selected = mTransformInteraction == null ? null : mTransformInteraction.GetSelectedTransform();
			if (selected != null)
			{
				//todo
			}

			if (selected == null && mGridContextMenu != null)
			{
				mGridContextMenu.Show(this, new Point(e.X, e.Y), ToolStripDropDownDirection.Default);
			}
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			mInteraction.TriggerKeyPress(keyData);

			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
