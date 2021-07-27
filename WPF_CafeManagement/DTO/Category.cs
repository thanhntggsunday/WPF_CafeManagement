using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class Category
    {
        public Category()
        {

        }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Category(DataRow row)
        {
            Id = (int) row["id"];
            Name = row["name"].ToString();           
        }

        public string Name { get; set; }

        public int Id { get; set; }

    }
}