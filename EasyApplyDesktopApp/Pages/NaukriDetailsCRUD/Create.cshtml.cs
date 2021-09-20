using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer;
using DataAccessLayer.Entity;

namespace EasyApplyDesktopApp.Pages.NaukriDetailsCRUD
{
    public class CreateModel : PageModel
    {
        private readonly DataAccessLayer.DataAccessContext _context;

        public CreateModel(DataAccessLayer.DataAccessContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NaukriJobDetail NaukriJobDetail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NaukriJobDetails.Add(NaukriJobDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
