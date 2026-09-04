using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public  record ProductId
    {
        public Guid Value { get; }

        private ProductId(Guid value)
        {
            Value = value;
        }

        public static ProductId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("ProductId ID cannot be empty");
            }

            return new ProductId(value);
        }
    }
}
