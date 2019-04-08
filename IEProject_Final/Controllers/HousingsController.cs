using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IEProject_Final.Models;

namespace IEProject_Final.Controllers
{
    public class HousingsController : Controller
    {
        private IEProjectModels db = new IEProjectModels();

        // GET: Housings
        public ActionResult Index()
        {
            return View(db.Housings.ToList());
        }

        // GET: Housings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Housing housing = db.Housings.Find(id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            return View(housing);
        }

        // GET: Housings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Housings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Suburb,SchoolNo,CrimeNo,Buy_Price,HospitalNo")] Housing housing)
        {
            if (ModelState.IsValid)
            {
                db.Housings.Add(housing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(housing);
        }

        // GET: Housings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Housing housing = db.Housings.Find(id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            return View(housing);
        }

        // POST: Housings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Suburb,SchoolNo,CrimeNo,Buy_Price,HospitalNo")] Housing housing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(housing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(housing);
        }

        // GET: Housings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Housing housing = db.Housings.Find(id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            return View(housing);
        }

        // POST: Housings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Housing housing = db.Housings.Find(id);
            db.Housings.Remove(housing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Results(bool bool0, bool bool1, bool bool2, bool bool3)
        {
            List<double> test = new List<double>();
            List<bool> blanks = new List<bool>();
            blanks.Add(bool0);
            blanks.Add(bool1);
            blanks.Add(bool2);
            blanks.Add(bool3);
            //blanks.ToArray();
            var i = 0;
            Debug.WriteLine("test");
            Debug.WriteLine(blanks[0] + "," + blanks[1] + "," + blanks[2] + "," + blanks[3]);
            test.Add(0);
            test.Add(0.25);
            test.Add(0.15);
            test.Add(0.15);
            if (blanks[0])
             {
                test[0] = 1.5;
             }
             if(blanks[1])
             {
                test[1] = 0.5;
             }
             if(blanks[2])
             {
                test[2] = 0.5;
             }
             if(blanks[3])
            {
                test[3] = 1.5;
            }

            var test1 = GetRating(test.ToArray());
            //var test1 = (from res in db.Housings
            //select new House_Rating() { Suburb = res.Suburb, Rating = variable[0] * res.CrimeNo + variable[1] * res.HospitalNo + variable[2] * res.SuperMarketNo + variable[3] * res.SchoolNo });
            //Debug.WriteLine(test1);
            Session["Suburbs"] = test1.Select(t => t.Suburb).ToList();
            //var test2 = test1.Select(d => d.Suburb);
            //Debug.WriteLine(test2.ToString());
            return View(test1.OrderByDescending(t => t.Rating));
        }
        internal IEnumerable<House_Rating> GetRating(double[] variable)
        {
            var b = variable[0];
            var y = variable[1];
            var z = variable[2];
            var a = variable[3];
            IEnumerable<House_Rating> query = (from res in db.Housings
                                               select new
                                               {
                                                   Suburb = res.Suburb,
                                                   Rating = (100 - (res.CrimeNo * y * 0.0001)) + 0.01 * a * res.HospitalNo + (100 - (res.Buy_Price * (z*0.000001))) + b * res.SchoolNo * 0.01
                                               }).OrderByDescending(t => t.Rating).Take(10).ToList().Select(x => new House_Rating { Suburb = x.Suburb, Rating = Math.Round(x.Rating - 101,2) });
            return query;
        }
        [HttpPost]
        public ActionResult Results(String SchoolType, bool[] Blanks)
        {
            Debug.WriteLine(SchoolType);
            return RedirectToAction("UpdateResults", "Housings", new { @schooltype = SchoolType,/* @bool0 = Blanks[0], @bool1 = Blanks[1], @bool2 = Blanks[2], @bool3 = Blanks[3], @bool4 = Blanks[4], @bool5 = Blanks[5]*/ });
        }

        public ActionResult UpdateResults(String schooltype/*, bool bool0, bool bool1, bool bool2, bool bool3, bool bool4, bool bool5*/)
        {

            var temp_subs = (List<String>)Session["Suburbs"];
            List<bool> blanks = new List<bool>();
            if (int.Parse(schooltype) == 1)
            {
                ViewBag.SchoolType = "Primary";
            }
            else if (int.Parse(schooltype) == 2)
            {
                ViewBag.SchoolType = "Secondary";
            }
            else
            {
                ViewBag.SchoolType = "Special";
            }
            /*blanks.Add(bool0);
            blanks.Add(bool1);
            blanks.Add(bool2);
            //List<int> price = new List<int>();
            int price = 0;
            if(bool0)
            {
                price = 250;
            }
            if(bool1)
            {
                price = 500;
            }
            if (bool2)
            {
                price = 2000;
            }*/
            var test1 = Update(schooltype/*, price, blanks*/);
            return View(test1.OrderByDescending(t => t.SchoolNo));
        }

        internal IEnumerable<Updated_Result> Update(String schooltype/*, int price, List<bool> variable*/)
        {
            var temp_subs = (List<String>)Session["Suburbs"];
            /*var a = variable[0];
            var b = variable[1];
            var c = variable[2];*/
            /*IEnumerable<Updated_Result> query = (from res in db.Schools
                                                 where temp_subs.Contains(res.Suburb)
                                                 orderby res.PrimaryNo,res.SecondaryNo
                                                 select new
                                                 {
                                                     Suburb = res.Suburb,
                                                 }).ToList().Select(x => new Updated_Result { Suburb = x.Suburb });
            */
            IEnumerable<Updated_Result> query;
            if (schooltype.Equals("1"))
            {
                query = (from res in db.Schools
                         where temp_subs.Contains(res.Suburb)
                         orderby (res.PrimaryNo)
                         select new
                         {
                             Suburb = res.Suburb,
                             SchoolNo = res.PrimaryNo
                         }).ToList().Select(x => new Updated_Result { Suburb = x.Suburb, SchoolNo = x.SchoolNo });
            }
            else if (schooltype.Equals("2"))
            {
                query = (from res in db.Schools
                         where temp_subs.Contains(res.Suburb)
                         orderby (res.SecondaryNo)
                         select new
                         {
                             Suburb = res.Suburb,
                             SchoolNo = res.SecondaryNo
                         }).ToList().Select(x => new Updated_Result { Suburb = x.Suburb, SchoolNo = x.SchoolNo });

            }
            else
            {
                query = (from res in db.Schools
                         where temp_subs.Contains(res.Suburb)
                         orderby (res.SpecialNo)
                         select new
                         {
                             Suburb = res.Suburb,
                             SchoolNo = res.SecondaryNo
                         }).ToList().Select(x => new Updated_Result { Suburb = x.Suburb, SchoolNo = x.SchoolNo });


            }
            return query;
        }
    }
}
