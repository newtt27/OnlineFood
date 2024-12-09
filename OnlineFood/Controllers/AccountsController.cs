using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Controllers
{
    public class AccountsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public AccountsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            // Kiểm tra nếu chưa đăng nhập
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login");
            }
            var onlineFoodContext = _context.Accounts.Include(a => a.IdroleNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.IdroleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["Idrole"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,TenHienThi,MatKhau,Idrole")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idrole"] = new SelectList(_context.Roles, "Id", "Id", account.Idrole);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["Idrole"] = new SelectList(_context.Roles, "Id", "Id", account.Idrole);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,TenHienThi,MatKhau,Idrole")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            ViewData["Idrole"] = new SelectList(_context.Roles, "Id", "Id", account.Idrole);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.IdroleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
        // GET: Accounts/Login
        public IActionResult Login()
        {
            return View();
        }
        // POST: Accounts/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Tìm người dùng trong cơ sở dữ liệu
            var user = await _context.Accounts
                .Include(a => a.IdroleNavigation)
                .FirstOrDefaultAsync(a => a.UserName == username && a.MatKhau == password);

            if (user != null)
            {
                // Lưu thông tin vào Session
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("DisplayName", user.TenHienThi);
                HttpContext.Session.SetString("Role", user.IdroleNavigation?.TenRole ?? "User");

                // Điều hướng dựa trên quyền
                if (user.IdroleNavigation?.TenRole == "Admin")
                {
                    return RedirectToAction("Index", "Admin"); // Trang quản trị
                }
                return RedirectToAction("Index", "Home"); // Trang người dùng
            }

            // Thông báo lỗi nếu đăng nhập thất bại
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }

        // GET: Accounts/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ thông tin trong Session
            return RedirectToAction("Index", "Home");
        }

        // Phương thức xử lý thay đổi mật khẩu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (currentPassword != newPassword && newPassword == confirmPassword)
            {
                // Lấy người dùng từ cơ sở dữ liệu
                var userId = HttpContext.Session.GetInt32("UserId");
                var user = await _context.Accounts.FindAsync(userId);

                if (user != null && user.MatKhau == currentPassword)
                {
                    // Cập nhật mật khẩu mới
                    user.MatKhau = newPassword;
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    // Đăng xuất để người dùng đăng nhập lại
                    HttpContext.Session.Clear();

                    // Chuyển hướng người dùng về trang login
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Current password is incorrect or passwords do not match.");
            }
            else
            {
                ModelState.AddModelError("", "Passwords do not match.");
            }

            return View();
        }


        // GET: Accounts/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

    }
    
}
