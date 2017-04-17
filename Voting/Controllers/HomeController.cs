using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voting.Models;

namespace Voting.Controllers
{
    public class HomeController : Controller
    {
        VotingContext db = new VotingContext();

        public ActionResult Index()
        {
            List<MatchResults> matchesList = new List<MatchResults>();
            foreach (var item in db.MatchResults)
            {
                matchesList.Add(item);
            }

            ViewBag.MatchesList = matchesList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(VotingResults[] votingResults)
        {
            List<MatchResults> matchesList = new List<MatchResults>();

            foreach (var item in db.MatchResults)
            {
                VotingResults vr = votingResults.Where(x => x.Id == item.Id).First();
                if (vr.Value == 1)
                {
                    item.HomeWon++;

                }
                if (vr.Value == 2)
                {
                    item.GuestWon++;
                }
                if (vr.Value == 3)
                {
                    item.Draw++;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Stats");
        }

        public ActionResult Stats()
        {
            List<MatchResults> matchesList = new List<MatchResults>();
            foreach (var item in db.MatchResults)
            {
                matchesList.Add(item);
            }

            ViewBag.MatchesList = matchesList;
            return View();
        }
    }
}