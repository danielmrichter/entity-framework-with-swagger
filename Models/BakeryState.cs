using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnet_bakery
{
    public class BakeryState {
        public List<BreadInventory> breadInventory { get; set; }
        public List<Baker> bakers { get; set; }
    }
}
