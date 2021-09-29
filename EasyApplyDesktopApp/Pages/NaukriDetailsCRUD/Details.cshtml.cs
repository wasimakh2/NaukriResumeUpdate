using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entity;

namespace EasyApplyDesktopApp.Pages.NaukriDetailsCRUD
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccessLayer.DataAccessContext _context;

        public DetailsModel(DataAccessLayer.DataAccessContext context)
        {
            _context = context;
        }

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
    }
}
