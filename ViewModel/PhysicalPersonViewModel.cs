using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AIRLINE.API.ViewModel
{
    public class PhysicalPersonViewModel
    {
         [DisplayName("Nome")]
         [Required(ErrorMessage ="Campo Obrigatório")]
        public string Name { get; set; }

         [Required(ErrorMessage ="Campo Obrigatório")]
         [ValidChars(ValidChars: "-.0123456789", ErrorMessage = "Cpf inválido")]
         [ValidCpf(ErrorMessage = "Cpf inválido")]
        public string CPF { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(4, ErrorMessage = "Telefone precisa conte no minimo 4 caracteres")]
        [MaxLength(24, ErrorMessage = "Telefone precisa conte no maximo 24 caracteres")]
        [ValidChars(ValidChars:"()+- 0123456789", ErrorMessage = "Telefone inválido")]
        public string Telephone { get; set; }

         [Required(ErrorMessage ="Campo Obrigatório")]
         [MinLength(8, ErrorMessage = "'RG' inválido")]
         [MaxLength(8, ErrorMessage = "'RG' inválido")]
         [ValidChars(ValidChars:"()+- 0123456789", ErrorMessage = "RG inválido")]
        public string RG { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Range(typeof(DateTime), "1/1/1950", "31/12/2005", ErrorMessage = "Data de Nascimento inválida {1:dd/MM/yyyy} and {2:dd/MM/yyyy]}")]
        public DateTime BirthDate { get; set; } = new(1950, 1, 1);
    }
}