using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mahsongs.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mahsongs.Pages.ConnectionPages
{
    public class editModel : PageModel
    {
        private ApplicationDbContext _db;

        public editModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Connections Connection { set; get; }

        [TempData]
        public string afterAddMessage { set; get; }

        //first add a parameter that is an integer
        public void OnGet(int id)
        {
            Connection = _db.Connectionitems.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var connectionInDb = _db.Connectionitems.Find(Connection.ID);
                connectionInDb.SongName = Connection.SongName;
                connectionInDb.Artist = Connection.Artist;
                connectionInDb.Rating = Connection.Rating;

                //do this for all your properties...

                await _db.SaveChangesAsync();
                afterAddMessage = "List item updated successfully!";

                return RedirectToPage("index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}