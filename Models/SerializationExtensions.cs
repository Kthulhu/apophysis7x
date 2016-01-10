using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;

namespace Xyrus.Apophysis.Models
{
	static class SerializationExtensions
	{
		[NotNull]
		public static string Serialize(this Size value)
		{
			return string.Format(@"{0} {1}", 
				value.Width.ToString(CultureInfo.InvariantCulture),
				value.Height.ToString(CultureInfo.InvariantCulture));
		}

		[NotNull]
		public static string Serialize(this Vector2 value)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			return string.Format(@"{0} {1}",
				value.X.ToString(CultureInfo.InvariantCulture),
				value.Y.ToString(CultureInfo.InvariantCulture));
		}

		[NotNull]
		public static string Serialize(this Color value)
		{
			var r = value.R / 255.0;
			var g = value.G / 255.0;
			var b = value.B / 255.0;

			return string.Format(@"{0} {1} {2}",
				r.ToString(CultureInfo.InvariantCulture),
				g.ToString(CultureInfo.InvariantCulture),
				b.ToString(CultureInfo.InvariantCulture));
		}

		[NotNull]
		public static string Serialize(this int value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		[NotNull]
		public static string Serialize(this float value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		[NotNull]
		public static string Serialize([CanBeNull] this float[] value)
		{
			value = value ?? new float[0];

			return string.Join(@" ", value.Select(Serialize).ToArray());
		}
	}
}