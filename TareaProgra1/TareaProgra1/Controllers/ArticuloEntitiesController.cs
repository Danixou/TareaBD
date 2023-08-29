using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TareaProgra1.Data;
using TareaProgra1.Models;
using Microsoft.Data.SqlClient;

namespace TareaProgra1.Controllers
{
    public class ArticuloEntitiesController : Controller
    {
        private readonly BDContext _context;

        public ArticuloEntitiesController(BDContext context)
        {
            _context = context;
        }

        // GET: ArticuloEntities
        public async Task<IActionResult> Index()
        {
            var articulosOrdenados = await _context.articulos.OrderBy(a => a.Nombre).ToListAsync();
              return _context.articulos != null ? 
                          View(articulosOrdenados) :
                          Problem("Entity set 'BDContext.articulos'  is null.");
        }

        // GET: ArticuloEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.articulos == null)
            {
                return NotFound();
            }

            var articuloEntity = await _context.articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articuloEntity == null)
            {
                return NotFound();
            }

            return View(articuloEntity);
        }

        // GET: ArticuloEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticuloEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] ArticuloEntity articuloEntity)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "insertarArticulos";
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = articuloEntity.Id;
                cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = articuloEntity.Nombre;
                cmd.Parameters.Add("@Price", System.Data.SqlDbType.Decimal).Value = articuloEntity.Precio;
                cmd.ExecuteNonQuery();

                conn.Close();
               // _context.Add(articuloEntity);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articuloEntity);
        }

        // GET: ArticuloEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.articulos == null)
            {
                return NotFound();
            }

            var articuloEntity = await _context.articulos.FindAsync(id);
            if (articuloEntity == null)
            {
                return NotFound();
            }
            return View(articuloEntity);
        }

        // POST: ArticuloEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio")] ArticuloEntity articuloEntity)
        {
            if (id != articuloEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articuloEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloEntityExists(articuloEntity.Id))
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
            return View(articuloEntity);
        }

        // GET: ArticuloEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.articulos == null)
            {
                return NotFound();
            }

            var articuloEntity = await _context.articulos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articuloEntity == null)
            {
                return NotFound();
            }

            return View(articuloEntity);
        }

        // POST: ArticuloEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.articulos == null)
            {
                return Problem("Entity set 'BDContext.articulos'  is null.");
            }
            var articuloEntity = await _context.articulos.FindAsync(id);
            if (articuloEntity != null)
            {
                _context.articulos.Remove(articuloEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloEntityExists(int id)
        {
          return (_context.articulos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
