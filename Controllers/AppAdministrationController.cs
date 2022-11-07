using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportManagement.Configuration;
using TransportManagement.Helper;
using TransportManagement.Models;

namespace TransportManagement.Controllers
{
    public class AppAdministrationController : Controller
    {
        private readonly IAppConfiguration _configuration;
        private readonly ServiceContext serviceContext;
        public AppAdministrationController(IAppConfiguration configuration)
        {
            this._configuration = configuration;
            serviceContext = new ServiceContext(configuration);
        }

        // GET: AppAdministrationController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser(UserModel model)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("FirstName", model.FirstName);
            data.Add("LastName", model.LastName);
            data.Add("Email", model.Email);
            data.Add("Permissions", model.Permissions);
            data.Add("CreatedDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("ETag", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("CreatedBy", model.CreatedBy);
            data.Add("IsActive", "1");

            serviceContext.CreateUser(data);

            UserModel user = serviceContext.GetUserByCondition("Email", model.Email);
            return View("UserView", user);
        }

        public ActionResult EditUserForm(string userId)
        {
            UserModel user = serviceContext.GetUserByCondition("ID", userId);
            return View("EditUserForm", user);
        }

        public ActionResult EditUser(UserModel model)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("FirstName", model.FirstName);
            data.Add("LastName", model.LastName);
            data.Add("Email", model.Email);
            data.Add("Permissions", model.Permissions);
            data.Add("ETag", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("CreatedBy", model.CreatedBy);
            data.Add("LastUpdatedBy", User.Claims.Where(t => t.Type == "preferred_username").Select(t => t.Value).FirstOrDefault());

            serviceContext.UpdateUserByCondition("ID", model.ID, data);

            UserModel user = serviceContext.GetUserByCondition("ID", model.ID);
            return View("UserView", user);
        }

        public ActionResult ViewUser(string UserId)
        {
            UserModel user = serviceContext.GetUserByCondition("ID",UserId);
            return View("UserView",user);
        }

        public ActionResult DeleteUser(string UserId)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("IsActive", "0");

            serviceContext.UpdateUserByCondition("ID", UserId, data);

            return RedirectToAction(actionName: "GetUsers", controllerName: "AppAdministration");
        }

        public ActionResult GetUsers()
        {
            List<UserModel> users = serviceContext.GetUsers();

            return View("UsersListView", users);
        }

        public ActionResult CreateUserForm()
        {
            return View(new UserModel() { });
        }

        // GET: AppAdministrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppAdministrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppAdministrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppAdministrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppAdministrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppAdministrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppAdministrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
