using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OnlineFood.Models;
using OnlineFood.Data;
using Microsoft.EntityFrameworkCore;


public class SettingsController : Controller
{
    private readonly OnlineFoodContext _context;

    public SettingsController(OnlineFoodContext context)
    {
        _context = context;
    }

    // GET: Settings
    public async Task<IActionResult> Index()
    {
        var roles = await _context.Roles.ToListAsync();
        var functions = await _context.Functions.ToListAsync();
        var viewModel = new SettingsViewModel
        {
            Roles = roles,
            Functions = functions
        };
        return View(viewModel);
    }

    // GET: Settings/CreateRole
    public IActionResult CreateRole()
    {
        return View();
    }

    // POST: Settings/CreateRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole([Bind("Name,Description")] Role role)
    {
        if (ModelState.IsValid)
        {
            _context.Add(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(role);
    }

    // GET: Settings/EditRole/5
    public async Task<IActionResult> EditRole(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return View(role);
    }

    // POST: Settings/EditRole/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(int id, [Bind("Id,Name,Description")] Role role)
    {
        if (id != role.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(role.Id))
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
        return View(role);
    }

    // GET: Settings/DeleteRole/5
    public async Task<IActionResult> DeleteRole(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    // POST: Settings/DeleteRole/5
    [HttpPost, ActionName("DeleteRole")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRoleConfirmed(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Similar CRUD operations for Function model

    private bool RoleExists(int id)
    {
        return _context.Roles.Any(e => e.Id == id);
    }
    // GET: Settings/CreateFunction
    public IActionResult CreateFunction()
    {
        return View();
    }

    // POST: Settings/CreateFunction
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFunction([Bind("Name,Description")] Function function)
    {
        if (ModelState.IsValid)
        {
            _context.Add(function);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(function);
    }

    // GET: Settings/EditFunction/5
    public async Task<IActionResult> EditFunction(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var function = await _context.Functions.FindAsync(id);
        if (function == null)
        {
            return NotFound();
        }
        return View(function);
    }

    // POST: Settings/EditFunction/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditFunction(int id, [Bind("Id,Name,Description")] Function function)
    {
        if (id != function.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(function);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionExists(function.Id))
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
        return View(function);
    }

    // GET: Settings/DeleteFunction/5
    public async Task<IActionResult> DeleteFunction(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var function = await _context.Functions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (function == null)
        {
            return NotFound();
        }

        return View(function);
    }

    // POST: Settings/DeleteFunction/5
    [HttpPost, ActionName("DeleteFunction")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFunctionConfirmed(int id)
    {
        var function = await _context.Functions.FindAsync(id);
        _context.Functions.Remove(function);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Kiểm tra sự tồn tại của Function
    private bool FunctionExists(int id)
    {
        return _context.Functions.Any(e => e.Id == id);
    }


}