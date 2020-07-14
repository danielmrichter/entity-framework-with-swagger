using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnet_bakery
{
    public class Baker {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [JsonIgnore]
        public ICollection<BreadInventory> breads { get; set; }
        
        // reminder: this is not a database field, so you cant filter
        // or do linq queries against it. This is purely for helping
        // out with JSON serializing
        [NotMapped]
        public int breadCount { 
            get {
                return (this.breads != null ? this.breads.Count : 0);
            }
        }
    }
}
