using PagedList;
using SachOnline.Buss;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        public void add(String id, string sdt)
        {
            DataBase mydb = new DataBase();
            var check = mydb.GioHangs.FirstOrDefault(s => s.MaSP == id && s.SDT == sdt);

            if (check == null)
            {
                GioHang gh = new GioHang();
                gh.MaSP = id;
                gh.SDT = sdt;
                gh.SoLuong = 1;
                mydb.GioHangs.Add(gh);
                mydb.SaveChanges();
            }
            else
            {
                int soluong = (int)check.SoLuong;
                check.SoLuong = soluong + 1;
                mydb.SaveChanges();

            }

        }
        public ActionResult xoaspfromcart(String id, int page = 1, int pagesize = 10)
        {
            DataBase mydb = new DataBase();
            string sdt = Session["SDT"].ToString();
            var check = mydb.GioHangs.FirstOrDefault(s => s.MaSP == id && s.SDT == sdt);
            mydb.GioHangs.Remove(check);
            mydb.SaveChanges();
            var sp = mydb.GioHangs.Select(p => p);
            sp = sp.OrderBy(p => p.SanPham.TenSP);
            sp = sp.Where(s => s.SDT == sdt);
            return View(sp.ToPagedList(page, pagesize));

        }
        // GET: ShoppingCart
        public ActionResult AddToCart(string id, int page = 1, int pagesize = 10)
        {
            DataBase mydb = new DataBase();
            string sdt = Session["SDT"].ToString();
            add(id, sdt);
            var sp = mydb.GioHangs.Select(p => p);
            sp = sp.OrderBy(p => p.SanPham.TenSP);
            sp = sp.Where(s => s.SDT == sdt);
            return View(sp.ToPagedList(page, pagesize));
        }
        public ActionResult Minus(string id, int page = 1, int pagesize = 10)
        {
            DataBase mydb = new DataBase();
            string sdt = Session["SDT"].ToString();
            tru(id, sdt);
            var sp = mydb.GioHangs.Select(p => p);
            sp = sp.OrderBy(p => p.SanPham.TenSP);
            sp = sp.Where(s => s.SDT == sdt);
            return View(sp.ToPagedList(page, pagesize));
        }

        public ActionResult XemGioHang(int page = 1, int pagesize = 10)
        {
            DataBase mydb = new DataBase();
            string sdt = Session["SDT"].ToString();
            var sp = mydb.GioHangs.Select(p => p);
            sp = sp.OrderBy(p => p.SanPham.TenSP);
            sp = sp.Where(s => s.SDT == sdt);
            return View(sp.ToPagedList(page, pagesize));
        }
        public void tru(String id, string sdt)
        {
            DataBase mydb = new DataBase();
            var check = mydb.GioHangs.FirstOrDefault(s => s.MaSP == id && s.SDT == sdt);

            int soluong = (int)check.SoLuong;
            check.SoLuong = soluong - 1;



            if (check.SoLuong == 0)
            {
                mydb.GioHangs.Remove(check);
            }
            mydb.SaveChanges();

        }
    }
}