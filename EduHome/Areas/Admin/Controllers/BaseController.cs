﻿using EduHome.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
   [Authorize(Roles = Constants.AdminRole)]

    public class BaseController : Controller
    {

    }
}
