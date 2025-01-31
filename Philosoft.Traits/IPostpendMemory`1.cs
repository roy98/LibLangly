﻿using System.Diagnostics.CodeAnalysis;

namespace System.Traits {
	/// <summary>
	/// Indicates the type can have other elements postpended onto it, with additional array and memory operations.
	/// </summary>
	/// <typeparam name="TElement">The type of the elements.</typeparam>
	public interface IPostpendMemory<TElement> : IPostpend<TElement> {
		/// <summary>
		/// Postpends the elements onto this object, as a batch.
		/// </summary>
		/// <param name="elements">The elements to postpend.</param>
		void Postpend([AllowNull] params TElement?[] elements);

		/// <summary>
		/// Postpends the elements onto this object, as a batch.
		/// </summary>
		/// <param name="elements">The elements to postpend.</param>
		void Postpend(ArraySegment<TElement?> elements);

		/// <summary>
		/// Postpends the elements onto this object, as a batch.
		/// </summary>
		/// <param name="elements">The elements to postpend.</param>
		void Postpend(Memory<TElement?> elements);

		/// <summary>
		/// Postpends the elements onto this object, as a batch.
		/// </summary>
		/// <param name="elements">The elements to postpend.</param>
		void Postpend(ReadOnlyMemory<TElement?> elements);
	}
}
