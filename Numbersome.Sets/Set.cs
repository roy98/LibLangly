﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Traits;

namespace Numbersome {
	/// <summary>
	/// Represents a set of <typeparamref name="TElement"/>.
	/// </summary>
	/// <typeparam name="TElement">The type of elements in the set.</typeparam>
	/// <remarks>
	/// <para>A set is a collection in which any element can only exist once. All other collections are technically multi-sets, if you need such. Furthermore, sets don't care about data being ordered.</para>
	/// <para>Unlike <see cref="System.Collections.Generic.ISet{T}"/> this adheres to Set Theory and Set Algebra, so its behavior should not be surprising, and existing techniques and solutions can easily be translated.</para>
	/// </remarks>
	public class Set<TElement> : IContains<TElement> {
		/// <summary>
		/// The <see cref="Predicate{T}"/> used to determine inclusion into the set.
		/// </summary>
		[DisallowNull, NotNull]
		protected readonly Predicate<TElement> Predicate;

		/// <summary>
		/// Initializes a new <see cref="Set{TElement}"/>.
		/// </summary>
		/// <param name="predicate">The <see cref="Predicate{T}"/> used to determine inclusion into the set.</param>
		public Set([DisallowNull] Predicate<TElement> predicate) => Predicate = predicate;

		/// <summary>
		/// Initializes a new <see cref="Set{TElement}"/>.
		/// </summary>
		/// <param name="elements">The elements of the set.</param>
		public Set([DisallowNull] params TElement[] elements) => Predicate = (element) => elements.Contains(element);

		/// <summary>
		/// Initializes a new <see cref="Set{TElement}"/>.
		/// </summary>
		/// <param name="collection">The elements of the set.</param>
		public Set([DisallowNull] IContains<TElement> collection) => Predicate = (element) => collection.Contains(element);

		/// <summary>
		/// Singleton instance for the empty set (Ø) of <typeparamref name="TElement"/>.
		/// </summary>
		[DisallowNull, NotNull]
		public static Set<TElement> Empty { get; } = new Set<TElement>((element) => element is null);

		/// <summary>
		/// Singleton instance for the universe set (U) of <typeparamref name="TElement"/>.
		/// </summary>
		[DisallowNull, NotNull]
		public static Set<TElement> Universe { get; } = new Set<TElement>((element) => element is not null);

		/// <summary>
		/// Returns the difference of <paramref name="right"/> from <paramref name="left"/>.
		/// </summary>
		/// <param name="left">The lefthand set.</param>
		/// <param name="right">The righthand set.</param>
		/// <returns>The difference of <paramref name="right"/> from <paramref name="left"/>.</returns>
		[return: NotNull]
		public static Set<TElement> operator -([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) => new(Difference(left, right));

		/// <summary>
		/// Returns the compliment of the <paramref name="set"/>.
		/// </summary>
		/// <param name="set">This set.</param>
		/// <returns>The compliment of the <paramref name="set"/>.</returns>
		[return: NotNull]
		public static Set<TElement> operator !([AllowNull] Set<TElement> set) => new(Compliment(set));

		/// <summary>
		/// Returns the intersection of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">The lefthand set.</param>
		/// <param name="right">The righthand set.</param>
		/// <returns>The intersection of <paramref name="left"/> and <paramref name="right"/>.</returns>
		[return: NotNull]
		public static Set<TElement> operator &([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) => new(Intersection(left, right));

		/// <summary>
		/// Returns the disjunction of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">The lefthand set.</param>
		/// <param name="right">The righthand set.</param>
		/// <returns>The disjunction of <paramref name="left"/> and <paramref name="right"/>.</returns>
		[return: NotNull]
		public static Set<TElement> operator ^([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) => new(Disjunction(left, right));

		/// <summary>
		/// Returns the union of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">The lefthand set.</param>
		/// <param name="right">The righthand set.</param>
		/// <returns>The union of <paramref name="left"/> and <paramref name="right"/>.</returns>
		[return: NotNull]
		public static Set<TElement> operator |([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) => new(Union(left, right));

		/// <summary>
		/// Constructs a <see cref="Set{TElement}"/> from the range <paramref name="lower"/>..<paramref name="upper"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of the elements in the set.</typeparam>
		/// <param name="lower">The lower bound.</param>
		/// <param name="upper">The upper bound.</param>
		/// <returns>A <see cref="Set{TElement}"/> for the given range.</returns>
		public static Set<TElement> Range<TElement>([DisallowNull] TElement lower, [DisallowNull] TElement upper) where TElement : IComparable<TElement> => new((element) => lower.CompareTo(element) <= 0 && element.CompareTo(upper) <= 0);

		/// <inheritdoc/>
		public Boolean Contains([AllowNull] TElement element) => Predicate(element);

		[return: NotNull]
		protected static Predicate<TElement> Compliment([AllowNull] Set<TElement> set) => set is not null ? (element) => !set.Predicate(element) : (element) => element is null;

		[return: NotNull]
		protected static Predicate<TElement> Difference([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) {
			if (left is null) {
				return (element) => element is null;
			} else if (right is null) {
				return left.Predicate;
			} else {
				return (element) => left.Predicate(element) && !right.Predicate(element);
			}
		}

		[return: NotNull]
		protected static Predicate<TElement> Disjunction([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) {
			if (left is null && right is null) {
				return (element) => element is null;
			} else if (left is null) {
				return right.Predicate;
			} else if (right is null) {
				return left.Predicate;
			} else {
				return (element) => left.Predicate(element) ^ right.Predicate(element);
			}
		}

		[return: NotNull]
		protected static Predicate<TElement> Intersection([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) {
			if (left is null && right is null) {
				return (element) => element is null;
			} else if (left is null) {
				return right.Predicate;
			} else if (right is null) {
				return left.Predicate;
			} else {
				return (element) => left.Predicate(element) && right.Predicate(element);
			}
		}

		[return: NotNull]
		protected static Predicate<TElement> Union([AllowNull] Set<TElement> left, [AllowNull] Set<TElement> right) {
			if (left is null && right is null) {
				return (element) => element is null;
			} else if (left is null) {
				return right.Predicate;
			} else if (right is null) {
				return left.Predicate;
			} else {
				return (element) => left.Predicate(element) || right.Predicate(element);
			}
		}
	}
}
