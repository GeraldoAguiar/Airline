using AIRLINE.API.Models.Providers;

namespace AIRLINE.API.Models.Company;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UF { get; set; }
    public string CNPJ { get; set; }
    
    public IList<LegalPerson> Legal { get; set; }
    public IList<PhysicalPerson> Physical { get; set; }
}