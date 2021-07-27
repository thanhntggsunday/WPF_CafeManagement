using System;
using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class Food
    {
        public Food()
        {

        }
        public Food(int id, string name, int categoryId, float price)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
        }

        public Food(DataRow row)
        {
            Id = (int) row["id"];
            Name = row["name"].ToString();
            CategoryId = (int) row["idcategory"];
            Price = (float) Convert.ToDouble(row["price"].ToString());
        }

        public float Price { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}