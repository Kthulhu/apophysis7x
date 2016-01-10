using System;
using System.Linq;
using JetBrains.Annotations;
using Xyrus.Apophysis.Windows;

namespace Xyrus.Apophysis.Calculation
{
	[PublicAPI]
	public class ProgressiveIterationManager : IterationManagerBase, IThreaded, IProgressive
	{
		private static readonly float[] mDensitySeries = { 1, 5 };
		private readonly ThreadedIterationManager mInnerIterationManager;

		private readonly object mLock = new object();

		private float mNextDensity, mLogDensity;
		private int mSeriesIndex;

		public ProgressiveIterationManager()
		{
			mInnerIterationManager = new ThreadedIterationManager();
			mInnerIterationManager.Started += OnStarted;
			mInnerIterationManager.Progress += OnProgress;
			mInnerIterationManager.Finished += OnFinished;
		}

		public event BitmapReadyEventHandler BitmapReady;
		public TimeSpan? TimeUntilNextBitmap { get; private set; }

		private void OnStarted(object sender, StartedEventArgs args)
		{
			UpdateState(mInnerIterationManager);
			RaiseStarted();

			TimeLock.RunAfter(0.1f, () =>
			{
				if (BitmapReady != null)
				{
					var progressAtNextDensity = mNextDensity * IterationProgress/CurrentDensity;
					BitmapReady(this, new BitmapReadyEventArgs(progressAtNextDensity));
				}
			});
		}
		private void OnProgress(object sender, ProgressEventArgs args)
		{
			UpdateState(mInnerIterationManager);

			var currentDensity = mInnerIterationManager.CurrentDensity;
			if (currentDensity >= mNextDensity)
			{
				lock (mLock)
				{
					Suspend();

					mSeriesIndex ++;
					if (mSeriesIndex >= mDensitySeries.Length)
					{
						mLogDensity ++;
						mSeriesIndex = 0;
					}

					mNextDensity = mDensitySeries[mSeriesIndex] * Float.Power(10, mLogDensity);

					if (BitmapReady != null)
					{
						var progressAtNextDensity = mNextDensity * IterationProgress / CurrentDensity;
						BitmapReady(this, new BitmapReadyEventArgs(progressAtNextDensity));
					}

					Resume();
				}
			}

			var iterationsOfNextBitmap = mNextDensity*TotalIterations/TargetDensity;
			TimeUntilNextBitmap = IterationsPerSecond <= 0 ? (TimeSpan?)null : TimeSpan.FromSeconds((iterationsOfNextBitmap - IterationCount) /IterationsPerSecond);

			RaiseProgress();
		}
		private void OnFinished(object sender, FinishedEventArgs args)
		{
			UpdateState(mInnerIterationManager);
			RaiseFinished(args.Cancelled);
		}

		public void StartIterate(Histogram histogram)
		{
			if (histogram == null) throw new ArgumentNullException(nameof(histogram));
			StartIterate(histogram, 10e6f);
		}
		public void Iterate(Histogram histogram)
		{
			if (histogram == null) throw new ArgumentNullException(nameof(histogram));
			Iterate(histogram, 10e6f);
		}

		public override void StartIterate(Histogram histogram, float maxDensity)
		{
			TimeUntilNextBitmap = null;
			mSeriesIndex = 0;
			mLogDensity = 0;
			mNextDensity = mDensitySeries.First();
			mInnerIterationManager.StartIterate(histogram, maxDensity);
		}
		public override void Iterate(Histogram histogram, float maxDensity)
		{
			TimeUntilNextBitmap = null;
			mSeriesIndex = 0;
			mLogDensity = 0;
			mNextDensity = mDensitySeries.First();
			mInnerIterationManager.Iterate(histogram, maxDensity);
		}

		public override bool IsCancelling
		{
			get { return mInnerIterationManager.IsCancelling; }
		}
		public override bool IsSuspended
		{
			get { return mInnerIterationManager.IsSuspended; }
		}

		public override void Cancel()
		{
			mInnerIterationManager.Cancel();
		}
		public override void Resume()
		{
			mInnerIterationManager.Resume();
		}
		public override void Suspend()
		{
			mInnerIterationManager.Suspend();
		}
		public override void Wait()
		{
			mInnerIterationManager.Wait();
		}

		public void SetThreadCount(int? threadCount)
		{
			mInnerIterationManager.SetThreadCount(threadCount);
		}
	}
}