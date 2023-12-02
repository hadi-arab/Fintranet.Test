using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Fintranet.Test.Domain.Enums;
using System.Text.Json.Serialization;

namespace Fintranet.Test.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        [JsonPropertyName("_id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(VehicleType))]
        public VehicleType VehicleType { get; set; }

        [Required]
        public string Name { get; set; }

    }

    
}
