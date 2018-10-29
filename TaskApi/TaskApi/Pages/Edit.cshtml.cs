using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Pages
{
    public class EditModel : PageModel
    {
        private readonly TaskContext _db;

        public EditModel(TaskContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TaskItem Tasks { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tasks = await _db.TaskItems.FindAsync(id);

            if (Tasks == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Tasks).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"TaskItem {Tasks.Id} not found!");
            }

            return RedirectToPage("/Index");
        }
    }
}