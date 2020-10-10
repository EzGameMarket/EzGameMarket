namespace IdentityService.API.Models
{
    public class CreditCard
    {
        public string ID { get; set; }

        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string Exp_Year { get; set; }
        public string Exp_Month { get; set; }

        public string UserID { get; set; }
        public AppUser User { get; set; }
    }
}