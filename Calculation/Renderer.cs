using System;
using System.Drawing;
using JetBrains.Annotations;
using Xyrus.Apophysis.Models;
using Xyrus.Apophysis.Strings;

namespace Xyrus.Apophysis.Calculation
{
	[PublicAPI]
	public class Renderer : IDisposable
	{
		private const int mFuse = 20;
		private const float mNotifyThreshold = 1.0f;

		private readonly RenderMessengerBase mNullMessenger = new RenderMessengerBase();
		private RenderMessengerBase mMessenger;

		private NativeTimer mProgressTicker;
		private FlameData mData;

		~Renderer()
		{
			Dispose(false);
		}
		public Renderer([NotNull] Flame flame, Size size, int oversample = 1, float filterRadius = 0, bool withTransparency = true)
		{
			if (flame == null) throw new ArgumentNullException(nameof(flame));
			if (oversample <= 0) throw new ArgumentOutOfRangeException(nameof(oversample));
			if (filterRadius < 0) throw new ArgumentOutOfRangeException(nameof(filterRadius));
			if (size.Width <= 0 || size.Height <= 0) throw new ArgumentOutOfRangeException(nameof(size));

			Oversample = oversample;
			FilterRadius = filterRadius;
			WithTransparency = withTransparency;
			Size = size;

			mMessenger = mNullMessenger;
			mProgressTicker = new NativeTimer();

			Data = new FlameData(this, flame.Copy());
		}

		public void Initialize()
		{
			Data.Initialize();

			var size = Data.HistogramSize;
			var memSize = Histogram.GetMemorySize(size);

			Messenger.SendMessage(string.Format(@"{0} : {1}", DateTime.Now.ToString(@"T"), string.Format(Messages.RenderAllocatingMessage, memSize)));
			Histogram = new Histogram(this, size)
			{
				Brightness = Data.Flame.Brightness,
				Gamma = Data.Flame.Gamma,
				GammaThreshold = Data.Flame.GammaThreshold,
				Vibrancy = Data.Flame.Vibrancy,
				Background = Data.Flame.Background,
				Transparent = WithTransparency
			};
		}
		public void AdjustPixelsPerUnit(Size size)
		{
			Data.AdjustPixelsPerUnit(size);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (mData != null)
				{
					mData.Dispose();
					mData = null;
				}

				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				if (Histogram != null)
				{
					Histogram.Dispose();
					
					// ReSharper disable once AssignNullToNotNullAttribute
					Histogram = null;
				}
			}

			mProgressTicker = null;
			mMessenger = null;
		}

		public Size Size { get; private set; }
		public float FilterRadius { get; private set; }
		public int Oversample { get; private set; }
		public bool WithTransparency { get; private set; }

		[NotNull]
		public RenderMessengerBase Messenger
		{
			get { return mMessenger; }
			set
			{
				// ReSharper disable once ConstantNullCoalescingCondition
				mMessenger = value ?? mMessenger;
			}
		}

		[NotNull]
		public FlameData Data
		{
			get { return mData; }
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				mData = value;
			}
		}

		[NotNull]
		public Histogram Histogram
		{
			get; 
			private set;
		}
	}
}