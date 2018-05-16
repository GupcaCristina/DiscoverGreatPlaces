using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Places.BLL.Services.Interfaces;
using Places.DAL.EF;
using Places.DTO;
using Places.Web.Models.ViewModels;

namespace Places.Web.Controllers
{
    public class WorkSchedulesController : Controller
    {
        private IWorkScheduleServices _workScheduleservices;

        public WorkSchedulesController(IWorkScheduleServices workScheduleservices)
        {
            _workScheduleservices = workScheduleservices;
        }

        // GET: WorkSchedules
        public IActionResult Index()
        {
            var workSchedules = _workScheduleservices.GetByPlace(2);
            var workSchedulesModelViews= Mapper.Map<List<WorkScheduleViewModel>>(workSchedules);
            return View(workSchedulesModelViews);
        }

        // GET: WorkSchedules/Create
        public IActionResult Create(long id=2)
        {
            var days = _workScheduleservices.GetDaysOfWeek();
            ViewBag.days = Mapper.Map<List<LookupViewModel>>(days);
            ViewBag.PlaceId = id;

            return View();
        }

        // POST: WorkSchedules/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( WorkScheduleViewModel workSchedule)
        {
           
            if (ModelState.IsValid)
            {
                var newWorkSchedule = Mapper.Map<WorkScheduleDTO>(workSchedule);
                _workScheduleservices.SaveWorkSchedule(newWorkSchedule);
              
                return RedirectToAction(nameof(Index));
            }
            return View(workSchedule);
        }

        //// GET: WorkSchedules/Edit/5
        public IActionResult Edit(int id)
        {

            var workSchedule = Mapper.Map<WorkScheduleViewModel>(_workScheduleservices.GetById(id));

            var days = _workScheduleservices.GetDaysOfWeek();
            ViewBag.days = Mapper.Map<List<LookupViewModel>>(days);
            if (workSchedule == null)
            {
                return NotFound();
            }
            return View(workSchedule);
        }

        //// POST: WorkSchedules/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("StartWork,EndWork,HasLunchBreak,StartBreak,EndBreak,PlaceId,Day,Id")] EditWorkScheduleViewModel workScheduleModelView)
        {
            if (id != workScheduleModelView.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var workSchedule = Mapper.Map<WorkScheduleDTO>(workScheduleModelView);
                    _workScheduleservices.UpdateWorkSchedule(workSchedule);
                 
                }
                catch (DbUpdateConcurrencyException)
                {                   
                        throw;                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workScheduleModelView);
        }

        //// GET: WorkSchedules/Delete/5
        public IActionResult Delete(int id)
        {
          
            var workSchedule =  _workScheduleservices.GetById(id);
            var workScheduleVieModel = Mapper.Map<WorkScheduleViewModel>(workSchedule);


            if (workSchedule == null)
            {
                return NotFound();
            }

            return View(workScheduleVieModel);
        }

        //// POST: WorkSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
       
            _workScheduleservices.DeleteWorkSchedule(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
