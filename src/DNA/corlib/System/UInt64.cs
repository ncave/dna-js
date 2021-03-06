// Copyright (c) 2012 DotNetAnywhere
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#if !LOCALTEST

using System.Globalization;
namespace System {
	public struct UInt64 : IFormattable, IComparable, IComparable<ulong>, IEquatable<ulong> {

		public const ulong MinValue = 0;
		public const ulong MaxValue = 0xffffffffffffffffL;

#pragma warning disable 0169, 0649
		internal ulong m_value;
#pragma warning restore 0169, 0649

		public override bool Equals(object obj) {
			return (obj is ulong) && ((ulong)obj).m_value == this.m_value;
		}

		public override int GetHashCode() {
			return (int)(this.m_value & 0xffffffff) ^ (int)(this.m_value >> 32);
		}

		#region Parse methods

		public static ulong Parse(string s) {
			return Parse(s, NumberStyles.Integer, null);
		}

		public static ulong Parse(string s, NumberStyles style) {
			return Parse(s, style, null);
		}

		public static ulong Parse(string s, IFormatProvider formatProvider) {
			return Parse(s, NumberStyles.Integer, formatProvider);
		}

		public static ulong Parse(string s, NumberStyles style, IFormatProvider formatProvider) {
			if (s == null) {
				throw new ArgumentNullException();
			}
			//TODO: use style and provider
			int error = 0;
			int radix = (style & NumberStyles.AllowHexSpecifier) != 0 ? 16 : 10;
			ulong result = s.InternalToUInt64(out error, radix);
			if (error != 0) {
				throw String.GetFormatException(error);
			}
			return result;
		}

		public static bool TryParse(string s, out ulong result) {
			return TryParse(s, NumberStyles.Integer, null, out result);
		}

		private static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result) {
			if (s == null) {
				result = 0;
				return false;
			}
			//TODO: use style and provider
			int error = 0;
			int radix = (style & NumberStyles.AllowHexSpecifier) != 0 ? 16 : 10;
			result = s.InternalToUInt64(out error, radix);
			return error == 0;
		}

		#endregion

		#region ToString methods

		public override string ToString() {
			// return NumberFormatter.FormatGeneral(new NumberFormatter.NumberStore(this.m_value));
			return String.InternalFromUInt64(this.m_value);
		}

		public string ToString(IFormatProvider formatProvider) {
			return NumberFormatter.FormatGeneral(new NumberFormatter.NumberStore(this.m_value), formatProvider);
		}

		public string ToString(string format) {
			return this.ToString(format, null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			NumberFormatInfo nfi = NumberFormatInfo.GetInstance(formatProvider);
			return NumberFormatter.NumberToString(format, this.m_value, nfi);
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			if (!(obj is ulong)) {
				throw new ArgumentException();
			}
			return this.CompareTo((ulong)obj);
		}

		#endregion

		#region IComparable<ulong> Members

		public int CompareTo(ulong x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<ulong> Members

		public bool Equals(ulong x) {
			return this.m_value == x;
		}

		#endregion

	}
}

#endif
