using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entity;

namespace EasyApplyDesktopApp.Pages.NaukriDetailsCRUD
{
    public class EditModel : PageModel
    {
        private readonly DataAccessLayer.DataAccessContext _context;

        public EditModel(DataAccessLayer.DataAccessContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NaukriJobDetail NaukriJobDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NaukriJobDetail = await _context.NaukriJobDetails.FirstOrDefaultAsync(m => m.Id == id);

            if (NaukriJobDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NaukriJobDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NaukriJobDetailExists(NaukriJobDetail.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NaukriJobDetailExists(int id)
        {
            return _context.NaukriJobDetails.Any(e => e.Id == id);
        }
    }
}
