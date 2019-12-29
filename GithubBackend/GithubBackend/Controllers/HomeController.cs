using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GithubBackend.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace GithubBackend.Controllers
{
    [Route("api/[controller]")]

    public class HomeController : Controller
    {
        private readonly string _key;

        public HomeController()
        {
            _key = "repo";
        }
        [HttpGet]
        public async Task<ActionResult> GetRepos()
        {
            try
            {
                var listRepo = HttpContext.Session.GetString(_key);
                var listRepoObject = JsonConvert.DeserializeObject<List<Repo>>(listRepo);
                return Ok(listRepoObject);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "an unknown error occurred" });
            }

        }
        [HttpPost]
        public async Task<ActionResult> addRepo([FromBody] List<Repo> repo)
        {
            try
            {
                if (HttpContext.Session.GetString(_key) == null)
                {
                    HttpContext.Session.SetString(_key, JsonConvert.SerializeObject(repo));

                }
                else
                {
                    var listRepo = HttpContext.Session.GetString(_key);
                    var listRepoObject = JsonConvert.DeserializeObject<List<Repo>>(listRepo);
                    listRepoObject.Add(repo.FirstOrDefault());
                    HttpContext.Session.Remove(_key);
                    HttpContext.Session.SetString(_key, JsonConvert.SerializeObject(listRepoObject));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "an unknown error occurred" });
            }
            return Ok();
        }



    }
}
