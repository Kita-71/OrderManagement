﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        private OnlineShoppingEntities db = new OnlineShoppingEntities();
        // GET: Admin
        
        public ActionResult AdminIndex()
        {
            return View();
        }
        public ActionResult OrderIndex()
        {
            return View();
        }
        public ActionResult OrderCheck()
        {
            return View();
        }
        public ActionResult OrderInfo(int id)
        {
            Order t_order = db.Order.FirstOrDefault(s => s.ID == id );
            TempData["orderid"] = id;
            TempData.Keep("orderid");
            return View(t_order);
        }
        public ActionResult UpdateInfo()
        {
            int temp = (int)TempData["orderid"];
            TempData.Keep("orderid");
            Order t_order = db.Order.FirstOrDefault(s => s.ID == temp);
            return View(t_order);
        }
        [HttpPost]
        public ActionResult UpdateInfo(Order update)
        {
            //调用更新操作
            db.Entry(update).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("OrderCheck", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult OrderCheck(String OrderId)
        {
          
            if (String.IsNullOrEmpty(OrderId))
            {
                ViewBag.notice = "Id不能为空";
                return View();
            }
            int id = Convert.ToInt32(OrderId);
            Order orderchecked  = db.Order.FirstOrDefault(s => s.ID == id);
            if (orderchecked == null)
            {
                ViewBag.notice = "没有该订单";
                return View();
            }
            else
            {
                return RedirectToAction("OrderInfo", "Admin",new { id = id});
            }
        }
        public ActionResult AllOrders()
        {
            return View(db.Order.ToList());
        }

        public ActionResult OrderStatu1()
        {
            return View(db.Order.ToList());
        }
        public ActionResult OrderStatu2()
        {
            return View(db.Order.ToList());
        }
        public ActionResult OrderStatu3()
        {
            return View(db.Order.ToList());
        }
       
        public ActionResult DeliverInfo(int id)
        {
            Order t_order = db.Order.FirstOrDefault(s => s.ID == id);
            return View(t_order);
        }

        [HttpPost]
        public ActionResult DeliverInfo(Order update)
        {
            update.State = "待收货";
            update.DeliverTIME = DateTime.Now.ToLocalTime();
            db.Entry(update).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("OrderStatu1", "Admin");
        }

        public ActionResult OrderSum()
        {
            float sum=0;
            Commodity commodity1= db.Commodity.FirstOrDefault(s => s.ID == 1);
            Commodity commodity2 = db.Commodity.FirstOrDefault(s => s.ID == 2);
            Commodity commodity3 = db.Commodity.FirstOrDefault(s => s.ID == 3);
            sum += commodity1.Sales.Count;
            sum += commodity2.Sales.Count;
            sum += commodity3.Sales.Count;
            ViewBag.a = commodity1.Sales.Count / sum*100;
            ViewBag.b = commodity2.Sales.Count / sum * 100;
            ViewBag.c = commodity3.Sales.Count / sum * 100;
            return View(db.Commodity.ToList());
        }
    }
}