using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [MaxLength(12,ErrorMessage ="Account Number Length is 11")]
        [Required(ErrorMessage ="Account Number is Required")]
        [Column(TypeName ="nvarchar(11)")]
        [Display(Name ="Account Number")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Beneficiary Name is Required")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Beneficiary Name")]
        public string BeneficiaryName { get; set; }


        [Display(Name = "Bank Name")]
        [Required(ErrorMessage = "Bank Name is Required")]
        [Column(TypeName ="nvarchar(100)")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Swift Code is Required")]
        [Display(Name = "Swift Code")]
        [Column(TypeName = "nvarchar(100)")]
        public string SwiftCode { get; set; }

        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Amount is Required")]
        public int Amount { get; set; }
    }
}
