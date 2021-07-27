using System;
using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class Menu
    {
        public Menu(string foodName, int count, float price, float totalPrice = 0)
        {
            FoodName = foodName;
            Count = count;
            Price = price;
            TotalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            FoodName = row["Name"].ToString();
            Count = (int) row["count"];
            Price = (float) Convert.ToDouble(row["price"].ToString());
            TotalPrice = (float) Convert.ToDouble(row["totalPrice"].ToString());
        }

        public float TotalPrice { get; set; }

        public float Price { get; set; }

        public int Count { get; set; }

        public string FoodName { get; set; }
    }
}