namespace Rebar.ReqestsModels
{
    public class CreateOrderReqest
    {
        public List<CreateOrderShake>Shakes { get; set; }
        public string ClientName { get; set; }
    }
}
