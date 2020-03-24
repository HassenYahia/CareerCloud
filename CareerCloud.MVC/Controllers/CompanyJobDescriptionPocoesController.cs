using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CareerCloud.MVC.Controllers
{
    public class CompanyJobDescriptionPocoesController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobDescriptionPocoesController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobDescriptionPocoes
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobDescriptions.Include(c => c.CompanyJob);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJobDescriptionPocoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPocoes/Create
        public IActionResult Create()
        {
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: CompanyJobDescriptionPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobDescriptionPoco.Id = Guid.NewGuid();
                _context.Add(companyJobDescriptionPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPocoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions.FindAsync(id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescriptionPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (id != companyJobDescriptionPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobDescriptionPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobDescriptionPocoExists(companyJobDescriptionPoco.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPocoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescriptionPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions.FindAsync(id);
            _context.CompanyJobDescriptions.Remove(companyJobDescriptionPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobDescriptionPocoExists(Guid id)
        {
            return _context.CompanyJobDescriptions.Any(e => e.Id == id);
        }
    }
}
