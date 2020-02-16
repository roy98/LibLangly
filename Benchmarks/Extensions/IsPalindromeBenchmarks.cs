﻿using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Stringier;

namespace Benchmarks.Extensions {
	[SimpleJob(RuntimeMoniker.NetCoreApp30)]
	[SimpleJob(RuntimeMoniker.CoreRt30)]
	[SimpleJob(RuntimeMoniker.Mono)]
	public class IsPalindromeBenchmarks {
		[Params("", "hello", "detartrated", "Malayalam", "Was it a car or a cat I saw?", "No 'X' in Nixon", "Able was I ere I saw Elba", "A man, a plan, a canal, Panama!", "Café Éfac")]
		public String String { get; set; }

		[Benchmark]
		public Boolean IsPalindrome_GraphemeWise() => String.IsPalindrome();

		[Benchmark]
		public Boolean IsPalindrome_CharacterWise() => String.AsSpan().IsPalindrome();
	}
}
