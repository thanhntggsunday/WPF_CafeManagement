using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class Table
    {
        public Table()
        {

        }
        public Table(int id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public Table(DataRow row)
        {
            Id = (int) row["id"];
            Name = row["name"].ToString();
            Status = row["status"].ToString();
        }

        public string Status { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}