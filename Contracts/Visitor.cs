using System;
using System.ComponentModel.DataAnnotations;

namespace OpenVisitor.Contracts
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