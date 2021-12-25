using Newtonsoft.Json;

namespace Accounting.WebAPI.Entities
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Messege { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
