using System.Data;

namespace WPF_CafeManagement.DTO
{
    public class Account
    {
        public Account()
        {

        }
        public Account(string userName, string displayName, int type, string password = null)
        {
            UserName = userName;
            DisplayName = displayName;
            Type = type;
            Password = password;
        }

        public Account(DataRow row)
        {
            UserName = row["userName"].ToString();
            DisplayName = row["displayName"].ToString();
            Type = (int) row["type"];
            Password = row["password"].ToString();
        }

        public int Type { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }
    }
}