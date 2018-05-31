﻿namespace Divergic.Logging.Xunit.UnitTests
{
    using System;
    using Divergic.Logging.Xunit;
    using FluentAssertions;
    using global::Xunit;
    using global::Xunit.Abstractions;
    using NSubstitute;

    public class TestOutputLoggerProviderTests
    {
        [Fact]
        public void CanDisposeMultipleTimesTest()
        {
            var output = Substitute.For<ITestOutputHelper>();

            using (var sut = new TestOutputLoggerProvider(output))
            {
                sut.Dispose();
                sut.Dispose();
            }
        }

        [Fact]
        public void CreateLoggerReturnsOutputLoggerTest()
        {
            var categoryName = Guid.NewGuid().ToString();

            var output = Substitute.For<ITestOutputHelper>();

            using (var sut = new TestOutputLoggerProvider(output))
            {
                var actual = sut.CreateLogger(categoryName);

                actual.Should().BeOfType<TestOutputLogger>();
            }
        }

        [Fact]
        public void ThrowsExceptionWithNullOutputTest()
        {
            Action action = () => new TestOutputLoggerProvider(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}