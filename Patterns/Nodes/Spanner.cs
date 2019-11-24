﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Stringier.Patterns.Nodes {
	/// <summary>
	/// Represents a <see cref="Nodes.Node"/> who's content can span. That is, as long as it is present once, can repeat multiple times past that point.
	/// </summary>
	[SuppressMessage("Minor Bug", "S1206:\"Equals(Object)\" and \"GetHashCode()\" should be overridden in pairs", Justification = "It already is overriden, Sonar just doesn't understand this pattern.")]
	internal sealed class Spanner : Node, IEquatable<Spanner> {
		/// <summary>
		/// The <see cref="Nodes.Node"/> to be parsed.
		/// </summary>
		private readonly Node Node;

		/// <summary>
		/// Initialize a new <see cref="Spanner"/> from the given <paramref name="node"/>.
		/// </summary>
		/// <param name="node">The <see cref="Nodes.Node"/> to be parsed.</param>
		internal Spanner(Node node) => Node = node;

		/// <summary>
		/// Checks the first character in the <paramref name="source"/> against the header of this node.
		/// </summary>
		/// <remarks>
		/// This is primarily used to check whether a pattern may exist at the current position.
		/// </remarks>
		/// <param name="source">The <see cref="Source"/> to check against.</param>
		/// <returns><c>true</c> if this <see cref="Pattern"/> may be present, <c>false</c> if definately not.</
		internal override Boolean CheckHeader(ref Source source) => Node.CheckHeader(ref source);

		/// <summary>
		/// Call the Consume parser of this <see cref="Node"/> on the <paramref name="source"/> with the <paramref name="result"/>.
		/// </summary>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="result">A <see cref="Result"/> containing whether a match occured and the captured <see cref="String"/>.
		internal override void Consume(ref Source source, ref Result result) {
			//Store the source position and result length, because backtracking has to be done on the entire span unit
			Int32 OriginalPosition = source.Position;
			Int32 OriginalLength = result.Length;
			//We need to confirm the pattern exists at least once
			Node.Consume(ref source, ref result);
			if (!result) {
				//Backtrack
				source.Position = OriginalPosition;
				result.Length = OriginalLength;
				return;
			}
			//Now continue to consume as much as possible
			while (result) {
				//Update the positions so we can backtrack this unit
				OriginalPosition = source.Position;
				OriginalLength = result.Length;
				//Try consuming
				Node.Consume(ref source, ref result);
				if (!result) {
					//Before we break out, backtrack
					source.Position = OriginalPosition;
					result.Length = OriginalLength;
				}
			}
			result.Error.Clear(); //As long as the first pattern matched, this consume is successful; we just stop on the eventual fail
		}

		/// <summary>
		/// Call the Neglect parser of this <see cref="Node"/> on the <paramref name="source"/> with the <paramref name="result"/>.
		/// </summary>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="result">A <see cref="Result"/> containing whether a match occured and the captured <see cref="String"/>.
		internal override void Neglect(ref Source source, ref Result result) {
			//We need to confirm the pattern exists at least once
			Node.Neglect(ref source, ref result);
			if (!result) {
				return;
			}
			//Now continue to consume as much as possible
			while (result) {
				Node.Neglect(ref source, ref result);
			}
			result.Error.Clear(); //As long as the first pattern matched, this consume is successful; we just stop on the eventual fail
		}

		/// <summary>
		/// Determines whether this instance and a specified object have the same value.
		/// </summary>
		/// <param name="node">The <see cref="Node"/> to compare with the current <see cref="Node"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Node"/> is equal to the current <see cref="Node"/>; otherwise, <c>false</c>.</
		public override Boolean Equals(Node? node) {
			switch (node) {
			case Spanner other:
				return Equals(other);
			default:
				return false;
			}
		}

		/// <summary>
		/// Determines whether this instance and a specified object have the same value.
		/// </summary>
		/// <param name="other">The <see cref="Concatenator"/> to compare with the current <see cref="Node"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Node"/> is equal to the current <see cref="Node"/>; otherwise, <c>false</c>.</returns
		public Boolean Equals(Spanner other) => Node.Equals(other.Node);

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override Int32 GetHashCode() => Node.GetHashCode();

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Node"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current <see cref="Node"/>.</returns>
		public override String ToString() => $"+╣{Node}║";
	}
}
