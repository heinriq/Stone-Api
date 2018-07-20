using System;

namespace StoneApi.Models {
    public class Transaction{
        public int TransactionId { get; set; }
        public double MerchantCnpj { get; set; }
        public int CheckoutCode { get; set; }
        public string CipheredCardNumber { get; set; }
        public int AmountInCent { get; set; }
        public int Installments { get; set; }
        public string AcquirerName { get; set; }
        public string PaymentMethod { get; set; }
        public string CardBrandName { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AcquirerAuthorizationDateTime { get; set; }
    }
}