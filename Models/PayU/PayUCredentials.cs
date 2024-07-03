namespace eCommerceMvc.Models.PayU;

public class PayUCredentials
{
    public string ClientId { get; }
    public string ClientSecret { get; }
    public bool IsSandbox { get; }

    public PayUCredentials(string clientId, string clientSecret, bool sandbox)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        IsSandbox = sandbox;
    }

    public static PayUCredentials Sandbox(string clientId, string clientSecret)
    {
        return new PayUCredentials(clientId, clientSecret, true);
    }

    public string BaseUrl
    {
        get
        {
            if (IsSandbox)
            {
                return "https://secure.snd.payu.com";
            }
            else
            {
                return "https://secure.payu.com";
            }
        }
    }
}