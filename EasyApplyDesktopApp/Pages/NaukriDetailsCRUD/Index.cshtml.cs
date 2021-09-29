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
    public class IndexModel : PageModel
    {
        private readonly DataAccessLayer.DataAccessContext _context;

        public IndexModel(DataAccessLayer.DataAccessContext context)
        {
            _context = context;
        }

        public IList<NaukriJobDetail> NaukriJobDetail { get;set; }

        public async Task OnGetAsync()
        {
            NaukriJobDetail = await _context.NaukriJobDetails.ToListAsync();
        }
    }
}
