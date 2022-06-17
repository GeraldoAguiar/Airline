namespace AIRLINE.API.Models;

public class LegalPerson
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public DateTime PostedAt { get; set; } = DateTime.Now;
    public string Telephone { get; set; }

}
