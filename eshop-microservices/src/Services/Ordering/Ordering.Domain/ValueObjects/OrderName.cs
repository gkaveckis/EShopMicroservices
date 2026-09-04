using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; private set; } = default!;

        private OrderName(string value)
        {
            Value = value;
        }

        private static OrderName Of(string value) {
            ArgumentException.ThrowIfNullOrEmpty(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new OrderName(value);
        }
    }    
}
