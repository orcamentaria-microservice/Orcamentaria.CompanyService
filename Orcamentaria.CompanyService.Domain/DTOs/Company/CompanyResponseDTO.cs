namespace Orcamentaria.CompanyService.Domain.DTOs.Company
{
    public class CompanyResponseDTO
    {
        public long Id { get; set; }
        public string CorporateName { get; set; }
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public string Address { get; set; }
        public string TelePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
