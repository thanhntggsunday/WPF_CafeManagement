using System;
using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class Bill
    {
        private DateTime? _dateCheckIn;
        private DateTime? _dateCheckOut;

        public Bill() { }

        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckOut, int status, int discount = 0)
        {
            Id = id;
            DateCheckIn = dateCheckin;
            DateCheckOut = dateCheckOut;
            Status = status;
            Discount = discount;
        }

        public Bill(DataRow row)
        {
            Id = (int) row["id"];
            DateCheckIn = (DateTime?) row["dateCheckin"];

            object dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
                DateCheckOut = (DateTime?) dateCheckOutTemp;

            Status = (int) row["status"];

            if (row["discount"].ToString() != "")
                Discount = (int) row["discount"];

            if (row["totalPrice"] != null)
            {
                var total = row["totalPrice"].ToString();
                float fltTotalPrice = 0;
                float.TryParse(total, out fltTotalPrice);

                TotalPrice = fltTotalPrice;
            }
            
        }

        public static Bill CreateBillTotalPrice(DataRow row)
        {
            Bill bill = new Bill();

            bill.Id = (int)row["id"];
            if (row["totalPrice"] != null)
            {
                var total = row["totalPrice"].ToString();
                float fltTotalPrice = 0;
                float.TryParse(total, out fltTotalPrice);

                bill.TotalPrice = fltTotalPrice;
            }

            return bill;
        }

        public int Discount { get; set; }

        public int Status { get; set; }

        public DateTime? DateCheckOut
        {
            get { return _dateCheckOut; }
            set { _dateCheckOut = value; }
        }

        public DateTime? DateCheckIn
        {
            get { return _dateCheckIn; }
            set { _dateCheckIn = value; }
        }

        public int Id { get; set; }
        public float TotalPrice { get; set; }
    }
}