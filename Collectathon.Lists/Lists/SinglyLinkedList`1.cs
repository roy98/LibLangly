﻿using System;
using System.Diagnostics.CodeAnalysis;
using Langly;

namespace Collectathon.Lists {
	/// <summary>
	/// Represents a singly-linked list.
	/// </summary>
	/// <typeparam name="TElement">The type of the elements in the list.</typeparam>
	public sealed partial class SinglyLinkedList<TElement> : StandardList<TElement, SinglyLinkedList<TElement>.Node, SinglyLinkedList<TElement>> {
		/// <summary>
		/// Initializes a new <see cref="SinglyLinkedList{TElement}"/> with the given <paramref name="elements"/>.
		/// </summary>
		/// <param name="elements">The initial elements of the list.</param>
		public SinglyLinkedList([DisallowNull] TElement[] elements) : base(elements, Filters.None) { }

		/// <summary>
		/// Initializes a new <see cref="SinglyLinkedList{TElement}"/>.
		/// </summary>
		public SinglyLinkedList() : base(Filters.None) { }

		/// <summary>
		/// Initializes a new <see cref="SinglyLinkedList{TElement}"/> with the given <paramref name="elements"/>.
		/// </summary>
		/// <param name="elements">The initial elements of the list.</param>
		/// <param name="filter">The type of filter to use.</param>
		public SinglyLinkedList([DisallowNull] TElement[] elements, Filters filter) : base(elements, filter) { }

		/// <summary>
		/// Initializes a new <see cref="SinglyLinkedList{TElement}"/>.
		/// </summary>
		/// <param name="filter">The type of filter to use.</param>
		public SinglyLinkedList(Filters filter) : base(filter) { }

		/// <summary>
		/// Converts the <paramref name="elements"/> to a <see cref="SinglyLinkedList{TElement}"/>.
		/// </summary>
		/// <param name="elements">The elements to convert to a list.</param>
		[return: MaybeNull, NotNullIfNotNull("elements")]

		public static implicit operator SinglyLinkedList<TElement>([AllowNull] TElement[] elements) => elements is not null ? new(elements) : null;

		/// <inheritdoc/>
		[MemberNotNull(nameof(Head), nameof(Tail))]
		public override void Insert(nint index, [AllowNull] TElement element) {
			if (index == 0) {
				Prepend(element);
			} else if (index == Count) {
				Postpend(element);
			} else if (Count > 0) {
				Node P = null!;
				Node? N = Head;
				nint i = 0;
				while (N is not null) {
					P = N;
					N = N.Next;
					i++;
					if (i == index) break;
				}
				P.Next = new Node(element, next: N);
				Count++;
			} else {
				Head = new Node(element, next: null);
				Tail = Head;
				Count++;
			}
		}

		/// <inheritdoc/>
		public override void Remove([AllowNull] TElement element) => throw new NotImplementedException();

		/// <inheritdoc/>
		public override void RemoveFirst([AllowNull] TElement element) => throw new NotImplementedException();

		/// <inheritdoc/>
		public override void RemoveLast([AllowNull] TElement element) => throw new NotImplementedException();

		/// <inheritdoc/>
		protected override Node NewUnlinkedNode([AllowNull] TElement element) => new Node(element, next: null);
	}
}
