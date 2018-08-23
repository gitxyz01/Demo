using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Define
{
    public class Define
    {
        public enum OrderStatus
        {
            [Description("Đang Chờ")]
            ChoXacNhan = 0,

            [Description("Đã Giao Hàng")]
            DaGiaoHang = 1,

            [Description("Hủy")]
            Huy = 2
        }
        public enum MenuType
        {
            [Description("Menu Footer")]
            Footer = 1,

            [Description("Top Banner")]
            TopBanner = 2,

            [Description("Logo")]
            Logo = 3,

            [Description("Bottom Banner")]
            BottomBanner = 4,

        }
        public enum CheckName
        {
            Create = 1,

            Update =2,
        }

    }
}
