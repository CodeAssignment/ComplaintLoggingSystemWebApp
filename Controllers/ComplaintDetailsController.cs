using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintLoggingSystem.Models;
using ComplaintLoggingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintLoggingSystem.Controllers
{
    public class ComplaintDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IComplaintDetailsSystem _complaintDetailsSystem;

        public ComplaintDetailsController(IComplaintDetailsSystem complaintDetailsSystem, IMapper mapper)
        {
            _complaintDetailsSystem = complaintDetailsSystem;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

        }
        // GET: ComplaintDetails
        public ActionResult Index()
        {
            var complaintDetails = _complaintDetailsSystem.GetComplaintDetails(emailId:"akshayslodha@gmail.com").Result;
            return View(_mapper.Map<List<ComplaintDetailsDomain>>(complaintDetails));
        }

        // GET: ComplaintDetails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComplaintDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComplaintDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComplaintDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComplaintDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComplaintDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComplaintDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}