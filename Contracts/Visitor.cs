using System;

namespace OpenVisitor.Contracts
{
    public class Visitor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Host { get; set; } = null!;
        public DateTime SignedInAt { get; set; } = DateTime.Now;
        public DateTime? SignedOutAt { get; set; }
    }
}