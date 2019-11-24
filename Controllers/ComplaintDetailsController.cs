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
            var complaintDetails = _complaintDetailsSystem.GetComplaintDetails(emailId: "akshayslodha@gmail.com").Result;
            return View(_mapper.Map<List<ComplaintDetailsDomain>>(complaintDetails));
        }

        // GET: ComplaintDetails/Details/5
        public ActionResult Details(Guid id)
        {
            var complaintDetails = _complaintDetailsSystem.GetComplaintDetail(id).Result;
            return View(_mapper.Map<ComplaintCompleteDetailDomain>(complaintDetails));
        }

        // GET: ComplaintDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComplaintDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComplaintDetailForCreationDomain collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var complaintDetailData = _mapper.Map<DataModels.ComplaintDetailForCreationData>(collection);
                    _complaintDetailsSystem.CreateComplaintDetail(complaintDetailData);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                
            }
            return View();
        }

        // GET: ComplaintDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComplaintDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ComplaintDetailForUpdationDomain collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var complaintDetailData = _mapper.Map<DataModels.ComplaintDetailForUpdationData>(collection);
                    _complaintDetailsSystem.UpdateComplaintDetail(id,complaintDetailData);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                
            }
            return View();
        }

        [HttpGet]
        // GET: ComplaintDetails/Delete/5
        public ActionResult Delete(Guid id)
        {

            var complaintDetails = _complaintDetailsSystem.GetComplaintDetail(id).Result;
            return View(_mapper.Map<ComplaintCompleteDetailDomain>(complaintDetails));
        }
            
        // POST: ComplaintDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, ComplaintCompleteDetailDomain complaintDetail)
        {
            try
            {
                _complaintDetailsSystem.DeleteComplaintDetail(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}