using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Fintranet.Test.Domain.Entities
{
    public class CongestionRule: BaseEntity
    {
        [Required]
        [JsonPropertyName("_id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string Key { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string Value { get; set; }

    }
}
