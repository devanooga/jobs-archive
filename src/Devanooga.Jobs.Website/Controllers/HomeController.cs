﻿namespace Devanooga.Jobs.Website.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
