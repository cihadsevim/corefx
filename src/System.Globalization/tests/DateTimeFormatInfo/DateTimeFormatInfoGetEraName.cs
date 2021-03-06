// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;

namespace System.Globalization.Tests
{
    public class DateTimeFormatInfoGetEraName
    {
        public static IEnumerable<object[]> GetEraName_TestData()
        {
            yield return new object[] { DateTimeFormatInfo.InvariantInfo, 1, "A.D." };
            yield return new object[] { DateTimeFormatInfo.InvariantInfo, 0, "A.D." };

            var enUSFormat = new CultureInfo("en-US").DateTimeFormat;
            yield return new object[] { enUSFormat, 1, DateTimeFormatInfoData.EnUSEraName() };
            yield return new object[] { enUSFormat, 0, DateTimeFormatInfoData.EnUSEraName() };

            var frRFormat = new CultureInfo("fr-FR").DateTimeFormat;
            yield return new object[] { frRFormat, 1, "ap. J.-C." };
            yield return new object[] { frRFormat, 0, "ap. J.-C." };
        }

        [Theory]
        [MemberData(nameof(GetEraName_TestData))]
        public void GetEraName(DateTimeFormatInfo format, int era, string expected)
        {
            Assert.Equal(expected, format.GetEraName(era));
        }

        [Fact]
        public void GetEraName_Invalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>("era", () => new DateTimeFormatInfo().GetEraName(-1)); // Era < 0

            const int EnUSMaxEra = 1;
            Assert.Throws<ArgumentOutOfRangeException>("era", () => new CultureInfo("en-US").DateTimeFormat.GetAbbreviatedEraName(EnUSMaxEra + 1)); // Era > max era for the culture
        }
    }
}
