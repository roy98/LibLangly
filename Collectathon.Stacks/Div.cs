using System;
using System.Diagnostics.CodeAnalysis;

namespace Collectathon.Stacks {
	public static partial class StackExtensions {
		#region Int32
		/// <summary>
		/// Divides the top two elements together, and pushes the result back onto the stack.
		/// </summary>
		/// <param name="stack">This <see cref="Stack{TElement}"/>.</param>
		/// <returns>A <see	cref="Stack{TElement}"/> instance if successful; otherwise, <see langword="null"/>.</returns>
		[return: MaybeNull]
		public static Stack<Int32> Div([AllowNull] this Stack<Int32> stack) => stack.Div(out Int32 result).Write(result);

		/// <summary>
		/// Divides the top two elements together, and returns the <paramref name="result"/>.
		/// </summary>
		/// <param name="stack">This <see cref="Stack{TElement}"/>.</param>
		/// <param name="result">The result of the division.</param>
		/// <returns>A <see	cref="Stack{TElement}"/> instance if successful; otherwise, <see langword="null"/>.</returns>
		[return: MaybeNull]
		public static Stack<Int32> Div([AllowNull] this Stack<Int32> stack, out Int32 result) {
			stack.Read(out Int32 right);
			stack.Read(out Int32 left);
			result = left / right;
			return stack;
		}
		#endregion

		#region Int64
		/// <summary>
		/// Divides the top two elements together, and pushes the result back onto the stack.
		/// </summary>
		/// <param name="stack">This <see cref="Stack{TElement}"/>.</param>
		/// <returns>A <see	cref="Stack{TElement}"/> instance if successful; otherwise, <see langword="null"/>.</returns>
		[return: MaybeNull]
		public static Stack<Int64> Div([AllowNull] this Stack<Int64> stack) => stack.Div(out Int64 result).Write(result);

		/// <summary>
		/// Divides the top two elements together, and returns the <paramref name="result"/>.
		/// </summary>
		/// <param name="stack">This <see cref="Stack{TElement}"/>.</param>
		/// <param name="result">The result of the division.</param>
		/// <returns>A <see	cref="Stack{TElement}"/> instance if successful; otherwise, <see langword="null"/>.</returns>
		[return: MaybeNull]
		public static Stack<Int64> Div([AllowNull] this Stack<Int64> stack, out Int64 result) {
			stack.Read(out Int64 right);
			stack.Read(out Int64 left);
			result = left / right;
			return stack;
		}
		#endregion
	}
}
