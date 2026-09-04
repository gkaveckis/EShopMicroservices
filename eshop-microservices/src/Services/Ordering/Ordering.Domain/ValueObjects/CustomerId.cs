using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; init; }

        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new ArgumentException("Customer ID cannot be empty");
            }

            return new CustomerId(value);            
        }
    }
}
