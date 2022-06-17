
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AIRLINE.API.ViewModel
{
    public class LegalPersonViewModels
    {
   
    [DisplayName("Nome")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [MinLength(2, ErrorMessage = "'Nome' precisa conter um mínimo de 2 caracteres.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage ="Campo Obrigatório")]
    [ValidChars(ValidChars: "-.0123456789", ErrorMessage = "Cnpj inválido")]
    [ValidCnpj(ErrorMessage = "Cnpj inválido")]
    public string CNPJ { get; set; }

    [DisplayName("Telefone")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [MinLength(4, ErrorMessage = "Telefone precisa conte no minimo 4 caracteres")]
    [MaxLength(24, ErrorMessage = "Telefone precisa conte no maximo 24 caracteres")]
    [ValidChars(ValidChars:"()+- 0123456789", ErrorMessage = "Telefone inválido")]
    public string Telephone { get; set; }

    }
}