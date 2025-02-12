﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProMSC.Areas.Clientes.Models;
using ProMSC.Data;

namespace ProMSC.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes/Cliente
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Cliente.ToListAsync());
        //}

        public async Task<IActionResult> Index(string BuscarNombre, string sortOrder, string currentFilter, int? page)
        {
            ViewData["RazonSocialSortParm"] = String.IsNullOrEmpty(sortOrder) ? "RazonSocial_desc" : "";
            ViewData["EstadoSortParm"] = sortOrder == "Estado_asc" ? "Estado_desc" : "Estado_asc";

            if (BuscarNombre != null)
            {
                page = 1;
            }
            else
            {
                BuscarNombre = currentFilter;
            }

            ViewData["CurrentFilter"] = BuscarNombre;
            ViewData["CurrentSort"] = sortOrder;

            var Cliente = from cr in _context.Cliente select cr;

            if (!String.IsNullOrEmpty(BuscarNombre))
            {
                Cliente = Cliente.Where(c => c.razonsocial.Contains(BuscarNombre));
            }

            switch (sortOrder)
            {
                case "RazonSocial_desc":
                    Cliente = Cliente.OrderByDescending(s => s.razonsocial);
                    break;
                case "Estado_desc":
                    Cliente = Cliente.OrderByDescending(s => s.estado);
                    break;
                case "Estado_asc":
                    Cliente = Cliente.OrderBy(s => s.estado);
                    break;
                default:
                    Cliente = Cliente.OrderBy(s => s.razonsocial);
                    break;
            }

            //return View(await Cliente.AsNoTracking().ToListAsync());
            //return View(await Cliente.ToListAsync());
            int pageSize = 3;
            return View(await Paginacion<Cliente>.CreateAsync(Cliente.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Clientes/Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.idcliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idcliente,razonsocial,ubicacion,contacto,email,telefono,estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idcliente,razonsocial,ubicacion,contacto,email,telefono,estado")] Cliente cliente)
        {
            if (id != cliente.idcliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.idcliente))
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
            return View(cliente);
        }

        // GET: Clientes/Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.idcliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.idcliente == id);
        }
    }
}
