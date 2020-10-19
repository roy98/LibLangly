﻿using System;

namespace Philosoft {
	/// <summary>
	/// Indicates the collection is countable.
	/// </summary>
	public interface ILengthy : ICountable {
		/// <summary>
		/// Gets the number of elements contained in the collection
		/// </summary>
		/// <value>The number of elements contained in the collection.</value>
		nint Length { get; }

		/// <inheritdoc/>
		nint ICountable.Count => Length;
	}
}
