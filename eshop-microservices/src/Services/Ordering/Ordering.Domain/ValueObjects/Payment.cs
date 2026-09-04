namespace Ordering.Domain.ValueObjects
{
    public class Payment
    {
        public string? CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;

        private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardName);
            ArgumentException.ThrowIfNullOrEmpty(cardNumber);
            ArgumentException.ThrowIfNullOrEmpty(expiration);
            ArgumentException.ThrowIfNullOrEmpty(cvv);
            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}