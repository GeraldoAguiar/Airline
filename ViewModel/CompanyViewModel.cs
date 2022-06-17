using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AIRLINE.API.ViewModel;

public class CompanyViewModel
{
    [DisplayName("Nome")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [MinLength(2, ErrorMessage = "'Nome' precisa conter um mínimo de 2 caracteres.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Campo Obrigatório")]
    public string UF { get; set; }
    
    [Required(ErrorMessage = "Campo Obrigatório")]
    public string CNPJ { get; set; }
}