﻿using System.Diagnostics.CodeAnalysis;
using System.Traits;
using System.Traits.Concepts;

namespace System {
	public static partial class TraitExtensions {
		/// <summary>
		/// Determines whether this collection contains any of the specified <paramref name="elements"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="elements">The elements to attempt to find.</param>
		/// <returns><see langword="true"/> if any of the <paramref name="elements"/> are contained in this collection; otherwise <see langword="false"/>.</returns>
		public static Boolean ContainsAny<TElement>([DisallowNull] this IContains<TElement> collection, [AllowNull] params TElement[] elements) {
			if (elements is not null) {
				foreach (TElement element in elements) {
					if (collection.Contains(element)) {
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Determines whether this collection contains any of the specified <paramref name="elements"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
		/// <typeparam name="TEnumerator">The type of the enumerator for the collection.</typeparam>
		/// <param name="collection">This collection.</param>
		/// <param name="elements">The elements to attempt to find.</param>
		/// <returns><see langword="true"/> if any of the <paramref name="elements"/> are contained in this collection; otherwise <see langword="false"/>.</returns>
		public static Boolean ContainsAny<TElement, TEnumerator>([DisallowNull] this ISequence<TElement, TEnumerator> collection, [AllowNull] params TElement[] elements) where TEnumerator : IEnumerator<TElement> {
			if (elements is not null) {
				foreach (TElement element in elements) {
					if (collection.Contains(element)) {
						return true;
					}
				}
			}
			return false;
		}
	}
}
