namespace AIRLINE.API.Models.Providers
{
    public class PhysicalPerson
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public string Telephone { get; set; }
        public string RG { get; set; }
        public DateTime BirthDate { get; set; }
    }
}