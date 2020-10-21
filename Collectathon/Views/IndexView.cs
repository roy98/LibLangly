﻿using System;
using Philosoft;

namespace Collectathon.Views {
	/// <summary>
	/// Represents a limited view of an associative collection.
	/// </summary>
	/// <typeparam name="TIndex">The type of the indicies being viewed.</typeparam>
	/// <typeparam name="TElement">The type of the elements being viewed.</typeparam>
	/// <typeparam name="TCollection">The type of the collection being viewed.</typeparam>
	/// <typeparam name="TEnumerator">The type of the enumerator of this collection.</typeparam>
	public readonly struct IndexView<TIndex, TElement, TCollection, TEnumerator> : IContainable<TIndex>, IEnumerable<TIndex, IndexView<TIndex, TElement, TCollection, TEnumerator>.Enumerator> where TIndex : IEquatable<TIndex> where TCollection : IAssociator<TIndex, TElement, TCollection, TEnumerator>, IEnumerable<Association<TIndex, TElement>, TEnumerator> where TEnumerator : IEnumerator<Association<TIndex, TElement>> {
		private readonly TCollection Collection;

		internal IndexView(TCollection collection) => Collection = collection;

		/// <inheritdoc/>
		Boolean IContainable<TIndex>.Contains(TIndex element) {
			foreach (Association<TIndex, TElement> association in Collection) {
				if (association.Index?.Equals(element) ?? false) {
					return true;
				}
			}
			return false;
		}

		/// <inheritdoc/>
		public Enumerator GetEnumerator() => new Enumerator(Collection);

		public readonly struct Enumerator : IEnumerator<TIndex> {
			private readonly IEnumerator<Association<TIndex, TElement>> enumerator;

			public Enumerator(TCollection collection) => enumerator = collection.GetEnumerator();

			/// <inheritdoc/>
			public TIndex Current => enumerator.Current.Index;

			/// <inheritdoc/>
			public IEnumerator<TIndex> GetEnumerator() => this;

			/// <inheritdoc/>
			public Boolean MoveNext() => enumerator.MoveNext();

			/// <inheritdoc/>
			public void Reset() => enumerator.Reset();
		}

	}
}
