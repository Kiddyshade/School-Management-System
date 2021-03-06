using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem2.Models;

namespace SchoolManagementSystem2.Controllers
{
    public class EnrollmentsController : Controller
    {
        private SchoolManagement_DBEntities db = new SchoolManagement_DBEntities();

        // GET: Enrollments
        public async Task<ActionResult> Index()
        {
            var enrollment = db.Enrollment.Include(e => e.Course).Include(e => e.Student).Include(e => e.Lecturers);
            return View(await enrollment.ToListAsync());
        }

        public PartialViewResult _enrollmentPartial(int? CourseID)
        {
            var enrollment = db.Enrollment.Where(q=>q.CourseID == CourseID)  //Filteration process
                .Include(e => e.Course)
                .Include(e => e.Student);
            return PartialView(enrollment.ToList());
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "LastName");
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID,LecturerId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollment.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "LastName", enrollment.StudentID);
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name", enrollment.LecturerId);
            return View(enrollment);
        }

        [HttpPost]

        public async Task <JsonResult> AddStudent([Bind(Include = "CourseID,StudentID")] Enrollment enrollment)
        {
            var isEnrolled = db.Enrollment.Any(q => q.StudentID == enrollment.StudentID && q.CourseID == enrollment.CourseID);
            try
            {
                if (ModelState.IsValid && !isEnrolled)
                {
                    db.Enrollment.Add(enrollment);
                    await db.SaveChangesAsync();
                    return Json(new { IsSuccess = true, Message = "Student added Successfully" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new {IsSuccess = false, Message = "Student not added Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new {IsSuccess = false, Message = "Error: Contact Administrator" }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "LastName", enrollment.StudentID);
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name", enrollment.LecturerId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID,LecturerId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "LastName", enrollment.StudentID);
            ViewBag.LecturerId = new SelectList(db.Lecturers, "Id", "First_Name", enrollment.LecturerId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollment.FindAsync(id);
            db.Enrollment.Remove(enrollment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetStudents(string term)
        {
            var student = db.Student.Select(q => new
            {
                Name = q.FirstName + " " + q.LastName,
                Id=q.StudentID
            }).Where(q=> q.Name.Contains(term));
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
