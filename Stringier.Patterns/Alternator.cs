﻿namespace System.Text.Patterns {
	/// <summary>
	/// Represents an alternator pattern
	/// </summary>
	internal sealed class Alternator : Node, IEquatable<Alternator> {
		private readonly Node Left;

		private readonly Node Right;

		internal Alternator(Node Left, Node Right) {
			this.Left = Left;
			this.Right = Right;
		}

		internal Alternator(Pattern Left, Pattern Right) : this(Left.Head, Right.Head) { }

		/// <summary>
		/// Attempt to consume the <see cref="Alternator"/> from the <paramref name="Source"/>, adjusting the position in the <paramref name="Source"/> as appropriate
		/// </summary>
		/// <param name="Source">The <see cref="Source"/> to consume</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the captured string</returns>
		public override Result Consume(ref Source Source) {
			Result Result = Left.Consume(ref Source);
			if (!Result) {
				Result = Right.Consume(ref Source);
			}
			return Result;
		}

		public override Boolean Equals(String other) => Left.Equals(other) || Right.Equals(other);

		public override Boolean Equals(Object obj) {
			switch (obj) {
			case Alternator Other:
				return Equals(Other);
			case String Other:
				return Equals(Other);
			default:
				return false;
			}
		}

		public Boolean Equals(Alternator other) => Left.Equals(other.Left) && Right.Equals(other.Right);

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override Int32 GetHashCode() => Left.GetHashCode() | Right.GetHashCode();

		public override Result Neglect(ref Source Source) {
			Int32 OriginalPosition = Source.Position;
			Result Result = Right.Neglect(ref Source);
			if (Result) {
				Source.Position = OriginalPosition;
				Result = Left.Neglect(ref Source);
			}
			return Result;
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override String ToString() => $"{Left} | {Right}";
	}
}
