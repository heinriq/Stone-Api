namespace StoneChallange.Models {
    public class Transaction{
        public int TransactionId { get; set; }
        public double MerchantCnpj { get; set; }
        public int CheckoutCode { get; set; }
        public string CipheredCardNumber { get; set; }
    }
}