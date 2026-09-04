using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; private set; }

        private OrderId(Guid value) { Value = value; }
        public static OrderId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("OrderId ID cannot be empty");
            }

            return new OrderId(value);
        }
    }
}
