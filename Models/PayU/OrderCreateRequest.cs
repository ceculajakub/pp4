namespace eCommerceMvc.Models.PayU
{
    public class OrderCreateRequest
    {
        public string notifyUrl { get; set; }
        public string customerIp { get; set; }
        public string merchantPosId { get; set; }
        public string description { get; set; }
        public string currencyCode { get; set; }
        public int totalAmount { get; set; }
        public string extOrderId { get; set; }
        public Buyer buyer { get; set; }
        public List<Product> products { get; set; }
        public string continueUrl { get; set; }
    }

    public class Buyer
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string language { get; set; }
    }

    public class Product
    {
        public string name { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
    }
}