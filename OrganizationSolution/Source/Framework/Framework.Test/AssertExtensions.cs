namespace Framework.Test
{
    using FluentAssertions;
    using Framework.Business;
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="AssertExtensions" />.
    /// </summary>
    public static class AssertExtensions
    {
        /// <summary>
        /// The EquivalentWithMissingMembersIgnoreId.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="actualValues">The actualValues<see cref="IEnumerable{T1}"/>.</param>
        /// <param name="expectedValues">The expectedValues<see cref="IEnumerable{T2}"/>.</param>
        public static void EquivalentWithMissingMembersIgnoreId<T1, T2>(IEnumerable<T1> actualValues, IEnumerable<T2> expectedValues)
            where T1 : IModelWithId
            where T2 : IModelWithId
        {
            foreach (var actualValue in actualValues)
            {
                actualValue.Id = 0;
            }

            foreach (var expectedValue in expectedValues)
            {
                expectedValue.Id = 0;
            }

            actualValues.Should().BeEquivalentTo(
                expectedValues,
                options => options.ExcludingMissingMembers().Excluding(x => x.Id));
        }

        /// <summary>
        /// The EquivalentWithMissingMembers.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="actualValues">The actualValues<see cref="IEnumerable{T1}"/>.</param>
        /// <param name="expectedValues">The expectedValues<see cref="IEnumerable{T2}"/>.</param>
        public static void EquivalentWithMissingMembers<T1, T2>(IEnumerable<T1> actualValues, IEnumerable<T2> expectedValues)
        {
            actualValues.Should().BeEquivalentTo(
                expectedValues,
                options => options.ExcludingMissingMembers());
        }

        /// <summary>
        /// The Equivalent.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="actualValues">The actualValues<see cref="IEnumerable{T1}"/>.</param>
        /// <param name="expectedValues">The expectedValues<see cref="IEnumerable{T2}"/>.</param>
        public static void Equivalent<T1, T2>(IEnumerable<T1> actualValues, IEnumerable<T2> expectedValues)
        {
            actualValues.Should().BeEquivalentTo(expectedValues);
        }

        /// <summary>
        /// The EquivalentWithMissingMembers.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="actualValue">The actualValue<see cref="T1"/>.</param>
        /// <param name="expectedValue">The expectedValue<see cref="T2"/>.</param>
        public static void EquivalentWithMissingMembers<T1, T2>(T1 actualValue, T2 expectedValue)
        {
            actualValue.Should().BeEquivalentTo(
                expectedValue,
                options => options.ExcludingMissingMembers());
        }

        /// <summary>
        /// The EquivalentWithMissingMembersIgnoreId.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="actualValue">The actualValue<see cref="T1"/>.</param>
        /// <param name="expectedValue">The expectedValue<see cref="T2"/>.</param>
        public static void EquivalentWithMissingMembersIgnoreId<T1, T2>(T1 actualValue, T2 expectedValue)
            where T1 : IModelWithId
            where T2 : IModelWithId
        {
            actualValue.Id = 0;
            expectedValue.Id = 0;

            actualValue.Should().BeEquivalentTo(
                expectedValue,
                options => options.ExcludingMissingMembers().Excluding(x => x.Id));
        }

        /// <summary>
        /// The Equivalent.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="actualValue">The actualValue<see cref="T1"/>.</param>
        /// <param name="expectedValue">The expectedValue<see cref="T2"/>.</param>
        public static void Equivalent<T1, T2>(T1 actualValue, T2 expectedValue)
        {
            actualValue.Should().BeEquivalentTo(expectedValue);
        }

        /// <summary>
        /// The ContainsErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="TErrorCode[]"/>.</param>
        public static void ContainsErrorCode<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, TErrorCode errorCode, params TErrorCode[] errorCodes)
            where TErrorCode : struct, Enum
        {
            ContainsErrorCode(errorRecords, errorCodes.Prepend(errorCode));
        }

        /// <summary>
        /// The ContainsErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="IEnumerable{TErrorCode}"/>.</param>
        public static void ContainsErrorCode<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, IEnumerable<TErrorCode> errorCodes)
            where TErrorCode : struct, Enum
        {
            var result = ErrorCode(errorRecords, errorCodes);
            Assert.True(result, "Result does not contain the error code.");
        }

        /// <summary>
        /// The NotContainsErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="TErrorCode[]"/>.</param>
        public static void NotContainsErrorCode<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, TErrorCode errorCode, params TErrorCode[] errorCodes)
            where TErrorCode : struct, Enum
        {
            NotContainsErrorCode(errorRecords, errorCodes.Prepend(errorCode));
        }

        /// <summary>
        /// The NotContainsErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="IEnumerable{TErrorCode}"/>.</param>
        public static void NotContainsErrorCode<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, IEnumerable<TErrorCode> errorCodes)
            where TErrorCode : struct, Enum
        {
            var result = ErrorCode(errorRecords, errorCodes);
            Assert.False(result, "Result contains the error code.");
        }

        /// <summary>
        /// The ContainsOrdinalPosition.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="ordinalPositions">The ordinalPositions<see cref="int[]"/>.</param>
        public static void ContainsOrdinalPosition<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, int ordinalPosition, params int[] ordinalPositions)
            where TErrorCode : struct, Enum
        {
            ContainsOrdinalPosition(errorRecords, ordinalPositions.Prepend(ordinalPosition));
        }

        /// <summary>
        /// The ContainsOrdinalPosition.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="ordinalPositions">The ordinalPositions<see cref="IEnumerable{int}"/>.</param>
        public static void ContainsOrdinalPosition<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, IEnumerable<int> ordinalPositions)
            where TErrorCode : struct, Enum
        {
            var result = OrdinalPosition(errorRecords, ordinalPositions);
            Assert.True(result, "Result does not contain the ordinal position.");
        }

        /// <summary>
        /// The NotContainsOrdinalPosition.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="ordinalPositions">The ordinalPositions<see cref="int[]"/>.</param>
        public static void NotContainsOrdinalPosition<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, int ordinalPosition, params int[] ordinalPositions)
            where TErrorCode : struct, Enum
        {
            NotContainsOrdinalPosition(errorRecords, ordinalPositions.Prepend(ordinalPosition));
        }

        /// <summary>
        /// The NotContainsOrdinalPosition.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="ordinalPositions">The ordinalPositions<see cref="IEnumerable{int}"/>.</param>
        public static void NotContainsOrdinalPosition<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, IEnumerable<int> ordinalPositions)
            where TErrorCode : struct, Enum
        {
            var result = OrdinalPosition(errorRecords, ordinalPositions);
            Assert.False(result, "Result does contain the ordinal position.");
        }

        /// <summary>
        /// The ErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="IEnumerable{TErrorCode}"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool ErrorCode<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, IEnumerable<TErrorCode> errorCodes)
            where TErrorCode : struct, Enum
        {
            var result = errorRecords.SelectMany(x => x.Errors.Select(y => y.ErrorCode)).Where(z => errorCodes.Contains(z)).Distinct();
            return result.Count() == errorCodes.Distinct().Count();
        }

        /// <summary>
        /// The OrdinalPosition.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="ordinalPositions">The ordinalPositions<see cref="IEnumerable{int}"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool OrdinalPosition<TErrorCode>(ErrorRecords<TErrorCode> errorRecords, IEnumerable<int> ordinalPositions)
            where TErrorCode : struct, Enum
        {
            var result = errorRecords.Select(x => x.OrdinalPosition).Where(y => ordinalPositions.Contains(y)).Distinct();
            return result.Count() == ordinalPositions.Distinct().Count();
        }
    }
}
