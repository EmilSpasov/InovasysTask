using Inovasys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inovasys.Data.Models;
using Inovasys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Inovasys.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Contragent> _userManager;
        private readonly IVatInformationService _vatInformationService;

        public HomeController(ILogger<HomeController> logger, UserManager<Contragent> userManager, IVatInformationService vatInformationService)
        {
            _logger = logger;
            _userManager = userManager;
            _vatInformationService = vatInformationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var currentUser = await this._userManager.GetUserAsync(this.User);

            var viewModel = new ProfileViewModel
            {
                Name = currentUser.Name,
                Email = currentUser.Email,
                Address = currentUser.Address,
                DdsNumber = currentUser.DdsNumber,
            };

            return this.View(viewModel);
        }

        public IActionResult VatValidation()
        {
            return this.View();
        }

        public IActionResult VatValidationResult(string requestNumber)
        {
            var result = _vatInformationService.GetInformation(requestNumber);

            if (result == null)
            {
                return this.RedirectToAction(nameof(Error));
            }
           
            return this.View(result);
        }

        public IActionResult GetData(string vatNumber)
        {
            var result = _vatInformationService.GetInformation(vatNumber);

            return this.Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
