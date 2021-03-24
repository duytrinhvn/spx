﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPX.Data;
using SPX.Models;
using SPX.ViewModels;

namespace SPX.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BucketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<TeamSelected> TeamSelections { get; set; }

        public BucketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buckets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buckets.ToListAsync());
        }

        // GET: Buckets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bucket == null)
            {
                return NotFound();
            }

            return View(bucket);
        }

        // GET: Buckets/Create
        public IActionResult Create()
        {
            TeamSelections = new List<TeamSelected>();
            var teams = _context.Teams.ToList();
            teams.ForEach((team) => {
                TeamSelections.Add(new TeamSelected { Id = Guid.NewGuid(), Team = team, Selected = false });
            });
            return View(new BucketsCreateViewModel
            {
                TeamSelections = TeamSelections
            });
        }

        // POST: Buckets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] Bucket bucket)
        {
            if (ModelState.IsValid)
            {
                bucket.Id = Guid.NewGuid();

                var allTeams = _context.Teams.ToList();

                // Find selected teams
                var teamSelectedIds = (from teamSelected in TeamSelections
                                       where teamSelected.Selected == true
                                       select teamSelected.Team.Id).ToList();
                var teams = new List<Team>();
                teamSelectedIds.ForEach((id) =>
                {
                    var team = (from t in allTeams
                                where t.Id == id
                                select t).FirstOrDefault();
                    teams.Add(team);
                });


                // Create a bucketTeamConnection array object
                var bucketTeamConnections = new List<BucketTeamConnection>();

                // Loop through teams to add connections
                teams.ForEach((team) => {
                    bucketTeamConnections.Add(new BucketTeamConnection
                    {
                        Id = Guid.NewGuid(),
                        BucketFK = bucket.Id,
                        TeamFK = team.Id
                    });
                });

                await _context.Buckets.AddAsync(bucket);
                await _context.BucketTeamConnections.AddRangeAsync(bucketTeamConnections);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bucket);
        }

        // GET: Buckets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets.FindAsync(id);
            if (bucket == null)
            {
                return NotFound();
            }
            return View(bucket);
        }

        // POST: Buckets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price")] Bucket bucket)
        {
            if (id != bucket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bucket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BucketExists(bucket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bucket);
        }

        // GET: Buckets/Delete/5
        public async Task<IActionResult> Delete([Bind("Id")] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bucket == null)
            {
                return NotFound();
            }

            // Find all teams in this bucket
            var bucketTeamConnections = await _context.BucketTeamConnections.ToListAsync();
            var teamFKs = (from connection in bucketTeamConnections
                         where connection.BucketFK == id
                         select connection.TeamFK).ToList();
            var allTeams = _context.Teams.ToList();
            var teams = new List<Team>();
            teamFKs.ForEach((id) =>
            {
                var team = (from t in allTeams
                            where t.Id == id
                            select t).FirstOrDefault();
                teams.Add(team);
            });

            return View(new BucketsDeleteViewModel
            {
                Bucket = bucket,
                Teams = teams
            });
        }

        // POST: Buckets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bucket = await _context.Buckets.FindAsync(id);

            // delete all connections relate to this bucket
            var bucketTeamConnections = await _context.BucketTeamConnections.ToListAsync();
            bucketTeamConnections.ForEach((connection) => { 
                if(connection.BucketFK == id)
                {
                    _context.Remove(connection);
                }
            });

            _context.Buckets.Remove(bucket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BucketExists(Guid id)
        {
            return _context.Buckets.Any(e => e.Id == id);
        }
    }
}
