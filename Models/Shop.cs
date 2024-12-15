using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
namespace BerarDenisaLab7.Models
{
    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } 
        public string ShopName { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string ShopDetails => $"{ShopName}, {Adress}";

        [OneToMany]
        public List<ShopList> ShopLists { get; set; } = new();
    }
}
