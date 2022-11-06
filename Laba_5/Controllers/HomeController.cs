using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Laba_5.Models;


namespace Laba_5.Controllers
{
 
 public class GLobal_Variables
    {
        public static User last_user;// Костыль, который позволит обратится к последнему пользователю с которым велась работа
        public static Order unfinished_order;//Костыль для заполнения и создания заказа
    }

 public partial class HomeController : Controller
    {
        public ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            GLobal_Variables.last_user = user;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Create_Order()
        {
        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create_Order(User user,Order order)
        {
            if (order != null)
            {

                GLobal_Variables.unfinished_order = order;
                return RedirectToAction("Product_Choose");
            }
            else
                return RedirectToAction("Index");
        }

       public async Task<IActionResult> Order_List()
        {
            return View(await db.Orders.ToListAsync());
        }

        public async Task<IActionResult> Order_Details(int? id)
        {
            if (id != null)
            {
                Order order = await db.Orders.FirstOrDefaultAsync(p => p.Id == id);
                if (order != null)
                    return View(order);
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
