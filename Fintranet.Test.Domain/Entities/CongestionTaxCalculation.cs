using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fintranet.Test.Domain.Entities
{
    public class CongestionTaxCalculation: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CalculatedTax { get; set; }
        public int CarType { get; set; }
        public string RequestParams { get; set; }
    }
}
