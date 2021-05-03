using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheSource.Data;
using TheSource.Models;

namespace TheSource.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = "Be the first one to post!";
            var post = await _context.Post.OrderByDescending(p => p.Id).ToListAsync();
            return View("Index", post);
        }

        // GET: Posts/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Posts/SearchResult
        public async Task<IActionResult> SearchResult(string SearchTerm)
        {
            ViewBag.Message = "Oh no result! Be the first one to post on this!";
            return View("Index", await _context.Post.Where(p => p.Body.Contains(SearchTerm) || p.Title.Contains(SearchTerm)).OrderByDescending(p => p.Id).ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.Post = post;
            if (post == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments.Where(p => p.PostId == id).OrderByDescending(p => p.Id).ToListAsync();
            ViewBag.Comments = comments;

            ViewBag.IsAuthor = false;
            var currentUser = "";

            if (User.Identity.IsAuthenticated) 
                currentUser = (User.FindFirst(ClaimTypes.Name).Value);

            if (post.Author.Equals(currentUser))
                ViewBag.IsAuthor = true;
            else
                ViewBag.IsAuthor = false;

            return View("Details");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment([Bind("PostId,Name,Comment")] Comments c)
        {
            if (ModelState.IsValid)
            {
                _context.Add(c);
                await _context.SaveChangesAsync();
            }
            return Redirect("Details/" + c.PostId);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            string Author = User.FindFirst(ClaimTypes.Name).Value;
            ViewBag.Author = Author;
            return View();
        }

        // POST: Posts/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Body,Author")] Post p)
        {
            if (ModelState.IsValid)
            {
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(p);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
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

        // POST: Posts/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Author")] Post p)
        {
            ViewBag.IsAuthor = false;
            var currentUser = "";

            if (User.Identity.IsAuthenticated)
                currentUser = (User.FindFirst(ClaimTypes.Name).Value);

            if (p.Author.Equals(currentUser))
                ViewBag.IsAuthor = true;
            else
            {
                ViewBag.IsAuthor = false;
                var post = await _context.Post.OrderByDescending(p => p.Id).ToListAsync();
                return View("Index", post);
            }

            if (id != p.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(p.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", p);
            }
            return View(p);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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

        // POST: Posts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string author)
        {
            ViewBag.IsAuthor = false;
            var currentUser = "";

            if (User.Identity.IsAuthenticated)
                currentUser = (User.FindFirst(ClaimTypes.Name).Value);

            if (author.Equals(currentUser))
                ViewBag.IsAuthor = true;
            else
            {
                ViewBag.IsAuthor = false;
                var pos = await _context.Post.OrderByDescending(p => p.Id).ToListAsync();
                return View("Index", pos);
            }

            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            var comments = await _context.Comments.Where(p => p.PostId == id).OrderByDescending(p => p.Id).ToListAsync();
            _context.Comments.RemoveRange(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
