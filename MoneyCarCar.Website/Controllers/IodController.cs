using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyCarCar.Website.Controllers
{
    public class IodController : Controller
    {
        public ActionResult Iod()
        {
            return View();
        }


        //
        // POST: /Iod/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Iod/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Iod/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Iod/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Iod/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
