// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;

namespace OpenVisitor
{
    public static class ISO8601DateTimeExtensions
    {
        public static string ToISO8601String(this DateTime that) => that.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ");
        public static DateTime ISO8601StringToDate(this string that) => DateTime.Parse(that, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
}