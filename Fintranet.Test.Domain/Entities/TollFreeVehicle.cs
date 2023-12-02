using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Fintranet.Test.Domain.Entities
{
    public class TollFreeVehicle : BaseEntity
    {
        [Required]
        [JsonPropertyName("_id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        //public Vehicle Vehicle { get; set; }
    }
}
