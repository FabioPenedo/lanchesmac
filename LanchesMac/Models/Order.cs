using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    public class Order
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;



        [Required(ErrorMessage = "Informe o sobrenome")]
        [StringLength(50)]
        [Display(Name = "Sobrenome")]
        public string Surname { get; set; } = string.Empty;



        [Required(ErrorMessage = "Informe o seu endereço")]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Address1 { get; set; } = string.Empty;



        [StringLength(100)]
        [Display(Name = "Complemento")]
        public string Address2 { get; set; } = string.Empty;



        [Required(ErrorMessage = "Informe o seu CEP")]
        [StringLength(10, MinimumLength = 8)]
        [Display(Name = "CEP")]
        public string Cep { get; set; } = string.Empty;


        
        [StringLength(10)]
        [Display(Name = "Estado")]
        public string State { get; set; } = string.Empty;



        [StringLength(50)]
        [Display(Name = "Cidade")]
        public string City { get; set; } = string.Empty;



        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Telephone { get; set; } = string.Empty;



        [Required(ErrorMessage = "Informe o email.")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; } = string.Empty;



        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do Pedido")]
        public decimal TotalOrder { get; set; }



        [ScaffoldColumn(false)]
        [Display(Name = "Itens no Pedido")]
        public int TotalItemsOrdered { get; set; }



        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime OrderDispatched { get; set; }



        [Display(Name = "Data Envio Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDeliveredTo { get; set; }



        public List<DetailOrder>? OrderItems { get; set; }
    }
}
