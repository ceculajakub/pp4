namespace eCommerceMvc.Models.PayU
{
    public class OrderCreateResponse
    {
        public string statusCode { get; set; }
        public string redirectUri { get; set; }
        public string orderId { get; set; }
        public string extOrderId { get; set; }
    }
}