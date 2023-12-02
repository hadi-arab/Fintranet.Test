using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Fintranet.Test.Domain.Entities
{
    public class CongestionFee: BaseEntity
    {
        [Required]
        [JsonPropertyName("_id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string StartTime { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string EndTime { get; set; }

        [Required]
        public int Fee { get; set; }

        [Required]
        [Range(2013, 4000)]
        public int Year { get; set; }
    }
}
