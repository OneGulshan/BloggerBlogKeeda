using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggerBlogKeeda.Data;
using BloggerBlogKeeda.Models;
using Microsoft.AspNetCore.Authorization;
using BloggerBlogKeeda.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggerBlogKeeda.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly DataContext _context;
        private UserManager<AppUser> _userManager;

        public PostController(DataContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Post
        public async Task<IActionResult> Index(int? pageNumber)
        {
            if (_context.Post != null)
            {
                var post = _context.Post.Include(_ => _.User);
                int pageSize = 3;
                return View(await PaginatedList<Post>.CreateAsync(post.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            return View();
        }

        // GET: Post/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var post = await _context.Post.Include(_ => _.User).Include(_=>_.PostTags).ThenInclude(_=>_.Tags).Include(_ => _.PostCategories).ThenInclude(_ => _.Category).FirstOrDefaultAsync(m => m.Id == id);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            if (_context.Category != null && _context.Tags != null)
            {
                var CategoryList = _context.Category.ToList();
                var TagList = _context.Tags.ToList();
                PostViewModel vm = new()
                {
                    Categories = CategoryList.Select(_ => new SelectListItem()
                    {
                        Text = _.Title,
                        Value = _.Id.ToString()
                    }).ToList(),
                    Tags = TagList.Select(_ => new SelectListItem()
                    {
                        Text = _.Title,
                        Value = _.Id.ToString()
                    }).ToList()
                };
                return View(vm);
            }
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel vm)
        {
            var selectedCategory = vm.Categories?.Where(_ => _.Selected).Select(_ => _.Value).Select(int.Parse).ToList();
            var selectedTags = vm.Tags?.Where(_ => _.Selected).Select(_ => _.Value).Select(int.Parse).ToList();
            
            if (selectedCategory != null && selectedTags != null)
            {

                if (ModelState.IsValid)
                {
                    var post = new Post
                    {
                        Title = vm.Title,
                        Description = vm.Description,
                        PublishedDate = vm.PublishedDate,
                        AppUserId = _userManager.GetUserId(HttpContext.User)
                    };
                    foreach (var item in selectedCategory)
                    {
                        if (post.PostCategories != null)
                        {

                            post.PostCategories.Add(new PostCategory()
                            {
                                Post = post,
                                CategoryId = item
                            });
                        }
                    }
                    foreach (var item in selectedTags)
                    {
                        if (post.PostTags != null)
                        {

                            post.PostTags.Add(new PostTags()
                            {
                                Post = post,
                                TagsId = item
                            });
                        }
                    }
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PublishedDate,AppUserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'DataContext.Post'  is null.");
            }
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return (_context.Post?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
