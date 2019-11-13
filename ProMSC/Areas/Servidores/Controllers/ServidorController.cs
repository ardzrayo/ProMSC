using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProMSC.Areas.Servidores.Models;
using ProMSC.Data;

namespace ProMSC.Areas.Servidores.Controllers
{
    [Area("Servidores")]
    [Authorize]
    public class ServidorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServidorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servidores/Servidor
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Servidor.Include(s => s.cliente);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(string BuscarNombre)
        {
            var Servidor = from cr in _context.Servidor select cr;
            if (!String.IsNullOrEmpty(BuscarNombre))
            {
                Servidor = Servidor.Where(c => c.nombrevps.Contains(BuscarNombre) /*|| c.idcliente.Contains(BuscarNombre)*/);
            }
            return View(await Servidor.ToListAsync());
        }

        // GET: Servidores/Servidor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servidor = await _context.Servidor
                .Include(s => s.cliente)
                .FirstOrDefaultAsync(m => m.idvps == id);
            if (servidor == null)
            {
                return NotFound();
            }

            return View(servidor);
        }

        // GET: Servidores/Servidor/Create
        public IActionResult Create()
        {
            //Modificación Create Servidor
            ViewData["idcliente"] = new SelectList(_context.Cliente, "idcliente", "razonsocial");
            return View();
        }

        // POST: Servidores/Servidor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idvps,idcliente,nombrevps,cpu,ram,ips,discoduro,osystem,database,desktop,anchobanda,ip_publica,ip_privada,descripcion,estado")] Servidor servidor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servidor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idcliente"] = new SelectList(_context.Cliente, "idcliente", "email", servidor.idcliente);
            return View(servidor);
        }

        // GET: Servidores/Servidor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servidor = await _context.Servidor.FindAsync(id);
            if (servidor == null)
            {
                return NotFound();
            }
            ViewData["idcliente"] = new SelectList(_context.Cliente, "idcliente", "email", servidor.idcliente);
            return View(servidor);
        }

        // POST: Servidores/Servidor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idvps,idcliente,nombrevps,cpu,ram,ips,discoduro,osystem,database,desktop,anchobanda,ip_publica,ip_privada,descripcion,estado")] Servidor servidor)
        {
            if (id != servidor.idvps)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServidorExists(servidor.idvps))
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
            ViewData["idcliente"] = new SelectList(_context.Cliente, "idcliente", "email", servidor.idcliente);
            return View(servidor);
        }

        // GET: Servidores/Servidor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servidor = await _context.Servidor
                .Include(s => s.cliente)
                .FirstOrDefaultAsync(m => m.idvps == id);
            if (servidor == null)
            {
                return NotFound();
            }

            return View(servidor);
        }

        // POST: Servidores/Servidor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servidor = await _context.Servidor.FindAsync(id);
            _context.Servidor.Remove(servidor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServidorExists(int id)
        {
            return _context.Servidor.Any(e => e.idvps == id);
        }
    }
}
