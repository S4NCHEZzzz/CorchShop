using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Laba_5.Models;
using Laba_5.Controllers;

namespace Laba_5.Controllers
{
    public partial class HomeController : Controller
    {

        public async Task<IActionResult> Product_List()
        {
            return View(await db.Products.ToListAsync());
        }


        public IActionResult Create_Product()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create_Product(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Product_List");
        }




        public async Task<IActionResult> Product_Details(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }





        public async Task<IActionResult> Product_Edit(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Product_Edit(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Product_List");
        }




        [HttpGet]
        [ActionName("Product_Delete")]
        public async Task<IActionResult> Product_ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Product_Delete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Product_List");
                }
            }
            return NotFound();
        }

        public IActionResult Product_Choose(Product product, Kostil kostil)
        {
            foreach (Product item in db.Products)
            {
                kostil.product.Add(item);
            }

            return View(kostil);
        }
        [HttpPost]
        public async Task<IActionResult> Product_Choose(Product product,User user, Order order, Kostil kostil)
        { 
            GLobal_Variables.unfinished_order.Product = kostil.name_kostil;
            order = GLobal_Variables.unfinished_order;
            db.Orders.Add(order);
            user = GLobal_Variables.last_user;
           // order.Id = user.Id;       
            user.Order = order;
            order.User = user;
             await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }

}