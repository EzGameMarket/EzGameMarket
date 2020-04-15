namespace IdentityService.API.Models
{
    public class Address
    {
        public string ID { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VATNumber { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }

        public string UserID { get; set; }
        public AppUser User { get; set; }
    }
}