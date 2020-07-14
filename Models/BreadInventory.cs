using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_bakery
{
    public enum BreadType {
        Sourdough,
        Focaccia,
        Rye,
        White
    }
    public class BreadInventory
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }
        public int inventory { get; set; }

        // implicitly convert the enum to a string on json serialization (woo!)
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BreadType breadType { get; set; }

        // [JsonIgnore]
        [ForeignKey("Bakers")]
        public int bakedByid { get; set; }
        // [ForeignKey("bakedByid")]
        // will create bakedByid column
        public Baker bakedBy { get; set; }

        public void bake(int count = 1) {
            this.inventory += count;
        }

        public void sell(int count = 1) {
            this.inventory -= count;
        }

        // [NotMapped]
        // public string bakedByStr { get { return (this.bakedBy == null ? "n/a" : this.bakedBy.name); } }
    }
}
