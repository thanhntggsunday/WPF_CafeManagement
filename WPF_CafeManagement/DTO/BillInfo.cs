using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billId, int foodId, int count)
        {
            Id = id;
            BillId = billId;
            FoodId = foodId;
            Count = count;
        }

        public BillInfo(DataRow row)
        {
            Id = (int) row["id"];
            BillId = (int) row["idbill"];
            FoodId = (int) row["idfood"];
            Count = (int) row["count"];
        }

        public int Count { get; set; }

        public int FoodId { get; set; }

        public int BillId { get; set; }

        public int Id { get; set; }
    }
}