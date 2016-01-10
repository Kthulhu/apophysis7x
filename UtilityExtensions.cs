﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace Xyrus.Apophysis
{
	[PublicAPI]
	public static class UtilityExtensions
	{
		public static Size FitToFrame(this Size size, Size canvasSize)
		{
			if (size.Width <= 0 || size.Height <= 0)
				return size;

			if (canvasSize.Width <= 0 || canvasSize.Height <= 0)
				return canvasSize;

			var outputSize = (float)size.Width / size.Height > (float)canvasSize.Width / canvasSize.Height
				? new Size(canvasSize.Width, (int)(canvasSize.Width * (float)size.Height / size.Width))
				: new Size((int)(canvasSize.Height * (float)size.Width / size.Height), canvasSize.Height);

			return outputSize;
		}

		public static Color Invert(this Color color)
		{
			return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
		}
		public static Color ColorFromAhsb(int a, float h, float s, float b)
		{
			if (0 > a || 255 < a)
			{
				throw new ArgumentOutOfRangeException(nameof(a));
			}

			if (0f > h || 360f < h)
			{
				throw new ArgumentOutOfRangeException(nameof(h));
			}

			if (0f > s || 1f < s)
			{
				throw new ArgumentOutOfRangeException(nameof(s));
			}

			if (0f > b || 1f < b)
			{
				throw new ArgumentOutOfRangeException(nameof(b));
			}

			if (System.Math.Abs(s) < float.Epsilon)
			{
				return Color.FromArgb(a, Convert.ToInt32(b*255), Convert.ToInt32(b*255), Convert.ToInt32(b*255));
			}

			float fMax, fMid, fMin;

			if (0.5 < b)
			{
				fMax = b - (b*s) + s;
				fMin = b + (b*s) - s;
			}
			else
			{
				fMax = b + (b*s);
				fMin = b - (b*s);
			}

			var sextant = (int) System.Math.Floor(h/60f);
			if (300f <= h)
			{
				h -= 360f;
			}

			h /= 60f;
			h -= 2f * (float)System.Math.Floor(((sextant + 1f) % 6f) / 2f);

			if (0 == sextant%2)
			{
				fMid = h*(fMax - fMin) + fMin;
			}
			else
			{
				fMid = fMin - h*(fMax - fMin);
			}

			var max = Convert.ToInt32(fMax*255);
			var mid = Convert.ToInt32(fMid*255);
			var min = Convert.ToInt32(fMin*255);

			switch (sextant)
			{
				case 1:
					return Color.FromArgb(a, mid, max, min);
				case 2:
					return Color.FromArgb(a, min, max, mid);
				case 3:
					return Color.FromArgb(a, min, mid, max);
				case 4:
					return Color.FromArgb(a, mid, min, max);
				case 5:
					return Color.FromArgb(a, max, min, mid);
				default:
					return Color.FromArgb(a, max, mid, min);
			}
		}

		public static bool CheckDirectory(ref string path, bool emptyAllowed = false, bool requiresWriteAccess = false)
		{
			if (!emptyAllowed && (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(path.Trim())))
			{
				return false;
			}

			var expandedPath = Environment.ExpandEnvironmentVariables(path);
			if (!Directory.Exists(expandedPath))
			{
				return false;
			}

			if (requiresWriteAccess)
			{
				var testFilePath = Path.Combine(expandedPath, @"~" + DateTime.Now.Ticks.ToString("X8"));
				try
				{
					File.WriteAllText(testFilePath, string.Empty);
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					try
					{
						if (File.Exists(testFilePath))
							File.Delete(testFilePath);
					}
					// ReSharper disable once EmptyGeneralCatchClause
					catch (Exception) { }
				}
			}

			path = expandedPath;
			return true;
		}
		public static bool CheckFile(ref string path, bool emptyAllowed = false, bool requiresExist = true)
		{
			if (!emptyAllowed && (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(path.Trim())))
			{
				return false;
			}

			var expandedPath = Environment.ExpandEnvironmentVariables(path);
			if (requiresExist && !File.Exists(expandedPath))
			{
				return false;
			}

			if (!requiresExist)
			{
				var testFilePath = Path.Combine(expandedPath, @"~" + DateTime.Now.Ticks.ToString("X8"));
				try
				{
					File.WriteAllText(testFilePath, string.Empty);
				}
				catch (Exception)
				{
					return false;
				}
				finally
				{
					try
					{
						if (File.Exists(testFilePath))
							File.Delete(testFilePath);
					}
					// ReSharper disable once EmptyGeneralCatchClause
					catch (Exception) { }
				}
			}

			path = expandedPath;
			return true;
		}

		public static string CleanseFileName(this string text)
		{
			var chars = Path.GetInvalidFileNameChars();
			return chars.Aggregate(text, (cc, c) => cc.Replace(c, '_'));
		}

		public static void RegisterInContainer([NotNull] this Type @interface, [NotNull] Type @as, bool singleton = false)
		{
			if (@interface == null) throw new ArgumentNullException(nameof(@interface));
			if (@as == null) throw new ArgumentNullException(nameof(@as));

			ApophysisApplication.Container.RegisterType(@interface, @as);
		}
	}
}