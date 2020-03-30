// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;

namespace OpenVisitor.Shared
{
    public class Visitor
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public string Host { get; set; } = null!;

        [Required]
        public DateTime SignedInAt { get; set; }

        public DateTime? SignedOutAt { get; set; }
    }
}