using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApi.Models;

namespace TaskApi.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TaskContext _db;

        public CreateModel(TaskContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TaskItem Tasks { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var lastTask =  _db.TaskItems.OrderByDescending(t => t.Id).FirstOrDefault();
            if(lastTask != null)
            {
                Tasks.Id = lastTask.Id + 1;
            }
            else
            {
                Tasks.Id = 1;
            }

            _db.TaskItems.Add(Tasks);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}