using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SachOnline.Buss
{
    public class NhaXuatBanBus
    {
        public static IEnumerable<NhaXuatBan> DanhSach() // HÀM TRẢ NHIỀU SẢN PHẨM
        {
            DataBase mydb = new DataBase();
            var dm = mydb.NhaXuatBans.Select(p => p);
            return (dm);
        }
    }
}