using CommonLib.Sql;
using Dapper.Contrib.Extensions;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Api1.EfDbContext;
using System;
using Api1.Services;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {

        public PublishController()
        {
        }
        [Route("~/ef/transaction")]
        public IActionResult EntityFrameworkWithTransaction([FromServices] IAppService appService)
        {
            appService.InsertTesttable();
            return Ok("ef ok");
        }
    }
}
