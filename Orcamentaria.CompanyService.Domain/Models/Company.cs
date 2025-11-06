namespace Orcamentaria.CompanyService.Domain.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string CorporateName { get; set; }
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public string Address { get; set; }
        public string TelePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdatedBy { get; set; }
    }
}
