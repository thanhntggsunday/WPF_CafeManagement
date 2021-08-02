using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MDI;

namespace WPF_CafeManagement.Common
{
    public static class AppStaticDataManagement
    {
        public static List<MdiChild> TableWindowsList { get; set; } = new List<MdiChild>();
        public static List<MdiChild> AdminWindowsList { get; set; } = new List<MdiChild>();
    }
}
