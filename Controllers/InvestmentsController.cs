using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SPX.Data;
using SPX.Models;
using SPX.ViewModels;

namespace SPX.Controllers
{
    [Authorize(Roles = "Investor")]
    public class InvestmentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public InvestmentsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        // GET: InvestController
        public async Task<ActionResult> Index()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId);

            var interests = context.Interests.ToList();

            // Find Interest objects of the current user
            var currentUserInterests = (from i in interests
                                        where i.ApplicationUser == user
                                        select i);

            // Find interested teams of current user
            var allTeams = context.Teams.ToList();
            var currentUserInterestTeamIds = (from i in interests
                                            where i.ApplicationUser == user
                                            select i.TeamFK).ToList();
            var teams = new List<Team>();
            currentUserInterestTeamIds.ForEach((id) => {
                var team = (from t in allTeams
                            where t.Id == id
                            select t).FirstOrDefault();
                teams.Add(team);
            });

            // Find buckets that has interested teams of the current user
            var bucketTeamConnections = context.BucketTeamConnections.ToList();
            var buckets = context.Buckets.ToList();

            /*            var matchedBucket = from c in bucketTeamConnections
                                            where currentUserInterestTeams.Contains(c.Team)
                                            group c by c.Bucket into newGroup
                                            select newGroup.Key;*/
            var matchedBuckets = new List<Bucket>();
            teams.ForEach((team) => {
                var bucketFK = (from c in bucketTeamConnections
                              where c.TeamFK == team.Id
                              select c.BucketFK).FirstOrDefault();
                var bucket = (from b in buckets
                              where b.Id == bucketFK
                              select b).FirstOrDefault();
                if (!matchedBuckets.Contains(bucket) && bucket != null)
                {
                    matchedBuckets.Add(bucket);
                }
            });

            return View(new InvestmentsListViewModel
            {
                Interests = currentUserInterests,
                Buckets = matchedBuckets
            });
        }

        public async Task<RedirectToActionResult> AddInterest(string teamId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null && teamId != null)
            {
                var team = await context.Teams.FindAsync(Guid.Parse(teamId));
                var user = await userManager.FindByIdAsync(userId);
                var interest = new Interest { Id = Guid.NewGuid(), Team = team, ApplicationUser = user };

                context.Interests.Add(interest);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("ControlInterests");
        }

        public async Task<RedirectToActionResult> DeleteInterest(string teamId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null && teamId != null)
            {
                var team = await context.Teams.FindAsync(Guid.Parse(teamId));
                var user = await userManager.FindByIdAsync(userId);

                var interests = context.Interests.ToList();
                var selectedInterest = (from i in interests
                                        where i.ApplicationUser == user && i.Team == team
                                        select i).FirstOrDefault();
                context.Interests.Remove(selectedInterest);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("ControlInterests");
        }

        public async Task<ActionResult> ControlInterests()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId);

            var teams = context.Teams.ToList();
            var leagues = context.Leagues.ToList();

            var interests = context.Interests.ToList();
            var currentUserInterestedTeamsIds = from i in interests
                                                where i.ApplicationUser == user
                                                select i.Team.Id;

            return View(new ControlInterestsViewModel
            {
                Teams = teams,
                Leagues = leagues,
                InterestedTeamsIds = currentUserInterestedTeamsIds
            });
        }
    }
}
