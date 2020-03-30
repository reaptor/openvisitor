// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;

namespace OpenVisitor.Client.Shared
{
    public static class Iso8601DateTimeExtensions
    {
        public static string ToIso8601String(this DateTime that) => that.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ");
        public static DateTime Iso8601StringToDate(this string that) => DateTime.Parse(that, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
}