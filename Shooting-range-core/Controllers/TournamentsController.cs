using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shooting_range_core.Data;
using Shooting_range_core.Models;
using Shooting_range_core.Models.ViewModels;

namespace Shooting_range_core.Controllers
{
    [Authorize]
    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public static ArrayList array_of_day = new ArrayList { "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعة", "السبت" };


        public TournamentsController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Tournaments
        [HttpGet]
        public IActionResult Index()
        {
            TournamentVM tvm = new TournamentVM();


            DateTime CurrentDate = DateTime.Now;
            tvm.Year = CurrentDate.Year;
            tvm.Month = CurrentDate.Month;
            tvm.DDlYear = GenerateYearsDDL(tvm.Year);
            tvm.DDlMonth = GenerateMonthsDDL(tvm.Month);
            tvm.DDLShootingField = new SelectList(_context.ShootingFields.OrderByDescending(i=>i.Id).ToList(), "Id", "ShootingName");
            tvm.ArabicMonthName = ArabicMonthName(tvm.Month) + "  " + tvm.Year;

            tvm.Tournaments = _context.Tournaments.Where(t => t.TournamentDate.Year == CurrentDate.Year && t.TournamentDate.Month == CurrentDate.Month && t.ShootingFieldId!=1008).Include(t => t.ShootingField).OrderBy(t => t.TournamentDate).ToList();
            foreach (var item in tvm.Tournaments)
            {
                int IndexOfWeek = (int)item.TournamentDate.DayOfWeek;
                item.ArabicDayName = array_of_day[IndexOfWeek].ToString();
            }

            ViewBag.IndexDayOfWeek = (int)CurrentDate.DayOfWeek;


            return View(tvm);



        }
        [HttpPost]
        public IActionResult Index(TournamentVM tvm)
        {
            tvm.ArabicMonthName = ArabicMonthName(tvm.Month) + "  " + tvm.Year;
            tvm.DDlYear = GenerateYearsDDL(tvm.Year);
            tvm.DDlMonth = GenerateMonthsDDL(tvm.Month);
            var shootingField = _context.ShootingFields.Find(tvm.ShootingFieldId);
            if (shootingField == null)
            {
                return Redirect("/Error/404");
            }
            tvm.DDLShootingField = new SelectList(_context.ShootingFields.OrderByDescending(i => i.Id).ToList(), "Id", "ShootingName",tvm.ShootingFieldId);

            if (tvm.ShootingFieldId == 1008)
            {
                tvm.Tournaments = _context.Tournaments.Where(t => t.TournamentDate.Year == tvm.Year && t.TournamentDate.Month == tvm.Month && t.ShootingFieldId != tvm.ShootingFieldId).Include(t => t.ShootingField).OrderBy(t => t.TournamentDate).ToList();

            }
            else
            {
                tvm.Tournaments = _context.Tournaments.Where(t => t.TournamentDate.Year == tvm.Year && t.TournamentDate.Month == tvm.Month && t.ShootingFieldId == tvm.ShootingFieldId).Include(t => t.ShootingField).OrderBy(t => t.TournamentDate).ToList();

            }
            foreach (var item in tvm.Tournaments)
            {
                int IndexOfWeek = (int)item.TournamentDate.DayOfWeek;
                item.ArabicDayName = array_of_day[IndexOfWeek].ToString();
            }

            return View(tvm);
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/404");
            }

            var tournament = await _context.Tournaments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return Redirect("/Error/404");
            }

            return View(tournament);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            // latest record inserted
            var latestId = _context.Tournaments.Max(p => p.Id);
            var LatestTournamnet = _context.Tournaments.Find(latestId);
            ViewBag.ShootingFields = new SelectList(_context.ShootingFields.ToList(), "Id", "ShootingName", LatestTournamnet.ShootingFieldId);
            return View(LatestTournamnet);

        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                TimeSpan FromTime = TimeSpan.Parse(tournament.FromTime);
                TimeSpan Totime = TimeSpan.Parse(tournament.ToTime);

                // == 1 --> from > to , -1 --> from <to ,  0 --> from == to
                if (TimeSpan.Compare(FromTime, Totime) != 1)
                {
                    ViewBag.ShootingFields = new SelectList(_context.ShootingFields.ToList(), "Id", "ShootingName");
                    _context.Add(tournament);
                    await _context.SaveChangesAsync();
                    TempData["message"] = NotificationSystem.AddMessage("تم اضافة الدورة بنجاح ", NotificationType.Success.Value);
                }
                else
                {
                    TempData["message"] = NotificationSystem.AddMessage("وقت البدء يجب ان يكون اصغر من وقت الانتهاء ", NotificationType.Danger.Value);
                    return View(tournament);

                }


                return RedirectToAction(nameof(Index));

            }
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/404");
            }

            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return Redirect("/Error/404");
            }

            ViewBag.ShootingFields = new SelectList(_context.ShootingFields.ToList(), "Id", "ShootingName", tournament.ShootingFieldId);

            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return Redirect("/Error/404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournament);
                    ViewBag.ShootingFields = new SelectList(_context.ShootingFields.ToList(), "Id", "ShootingName", tournament.ShootingFieldId);
                    await _context.SaveChangesAsync();
                    TempData["message"] = NotificationSystem.AddMessage("تم تعديل الدورة بنجاح ", NotificationType.Success.Value);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(tournament.Id))
                    {
                        return Redirect("/Error/404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/404");
            }

            var tournament = await _context.Tournaments.Include(t => t.ShootingField).FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return Redirect("/Error/404");
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }

        public IActionResult Calendar(DateTime start_date, int ShootingFieldId)
        {
            if (start_date.Year == 1)
            {
                start_date = DateTime.Now.Date;
            }

            var shootingField = _context.ShootingFields.Find(ShootingFieldId);
            if (shootingField == null)
            {
                return Redirect("/Error/404");
            }
            // start day in week
            DateTime start_date_week = getWeekStartEndDate(start_date).Date;
            // end day in week
            DateTime end_date_week = getWeekStartEndDate(start_date, true).Date;
            ViewBag.ShootingFieldId = ShootingFieldId;

            ICollection<Tournament> tournaments = _context.Tournaments.Where(t => t.TournamentDate.Date >= start_date_week && t.TournamentDate.Date <= end_date_week && t.ShootingFieldId == ShootingFieldId).Include(s => s.ShootingField).ToList().ToList();

            // if time <10 add 0 befor this time
            foreach (var item in tournaments)
            {
                TimeSpan ts = TimeSpan.Parse(item.FromTime);
                if (ts.Hours < 10)
                {
                    item.FromTime = "0" + ts.Hours + ":00";
                }

            }



            return View(tournaments);
        }

        public IActionResult CalendarMonthly(DateTime start_date)
        {
            if (start_date.Year == 1)
            {
                start_date = DateTime.Now;
            }

            return View(_context.Tournaments.Where(t => t.TournamentDate.Year == start_date.Year && t.TournamentDate.Month == start_date.Month).Include(s => s.ShootingField).ToList());

        }

        public static DateTime getWeekStartEndDate(DateTime week_start_date, bool return_week_end_date = false)
        {
            if (return_week_end_date)
            {
                return week_start_date.AddDays(6);
            }
            return week_start_date;
        }

        public static string ArabicMonthName(int month_number)
        {
            IDictionary<int, string> monthsList = new Dictionary<int, string>() {
                {1,"كانون الثاني" },
                {2,"شباط" },
                {3,"آذار" },
                {4,"نيسان" },
                {5,"أيار" },
                {6,"حزيران" },
                {7,"تموز" },
                {8,"آب" },
                {9,"أيلول" },
                {10,"تشرين الأول" },
                {11,"تشرين الثاني" },
                {12,"كانون الأول" },
            };
            return monthsList[month_number];
        }

        public static SelectList GenerateYearsDDL(int selected_year = -1)
        {
            var items = new List<SelectListItem>();
            for (int i = 2018; i <= 2050; i++)
            {

                SelectListItem tmp = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                };
                if (selected_year != -1 && Int32.Parse(tmp.Value) == selected_year)
                {
                    tmp.Selected = true;
                }
                items.Add(tmp);



            }
            return new SelectList(items, "Value", "Text", selected_year);
        }
        //return a selectedlist of months
        public static SelectList GenerateMonthsDDL(int selected_month = -1)
        {
            var items = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                SelectListItem tmp = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = ArabicMonthName(i)
                };
                if (selected_month != -1 && Int32.Parse(tmp.Value) == selected_month)
                {
                    tmp.Selected = true;
                }
                items.Add(tmp);
            }
            return new SelectList(items, "Value", "Text", selected_month);
        }

        //public void ExportToExcel(DateTime start_date)
        //{
        //    if (start_date.Year == 1)
        //    {
        //        start_date = DateTime.Now;
        //    }

        //    var TournamnetList = _context.Tournaments.Where(t => t.TournamentDate.Year == start_date.Year && t.TournamentDate.Month == start_date.Month).Include(s => s.ShootingField).ToList();
        //    var excel = new ExcelMapper();
        //    excel.Ignore<Tournament>(t=>t.Id);
        //    var UniqueFileName = $@"{Guid.NewGuid()}.txt";

        //    excel.Save(UniqueFileName+".xlsx", TournamnetList, "Products");


        //}
    }
}
