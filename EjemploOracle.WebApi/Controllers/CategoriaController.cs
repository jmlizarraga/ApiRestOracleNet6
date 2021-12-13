using EjemploOracle.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EjemploOracle.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private ModelContext _context;

        public CategoriaController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Categorium>> Listar()
        {
            return await _context.Categoria.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categorium>> BuscarPorId(decimal id)
        {
            var retorno = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == id);

            if (retorno != null)
                return retorno;
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Categorium>> Guardar(Categorium c)
        {
            try
            {
                await _context.Categoria.AddAsync(c);
                await _context.SaveChangesAsync();
                c.Id = await _context.Categoria.MaxAsync(u => u.Id);

                return c;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Categorium>> Actualizar(Categorium c)
        {
            if (c == null || c.Id == 0)
                return BadRequest("Faltan datos");

            Categorium cat = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == c.Id);

            if (cat == null)
                return NotFound();

            try
            {
                cat.Nombre = c.Nombre;
                _context.Categoria.Update(cat);
                await _context.SaveChangesAsync();

                return cat;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(decimal id)
        {
            Categorium cat = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == id);

            if (cat == null)
                return NotFound();

            try
            {
                _context.Categoria.Remove(cat);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }
        }
    }
}
