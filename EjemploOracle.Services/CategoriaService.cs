using EjemploOracle.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploOracle.Services
{
    public class CategoriaService : ICategoriaService
    {
        private ModelContext _context;

        public CategoriaService(ModelContext context)
        {
            _context = context;
        }


        public async Task<RespuestaService<Categorium>> Actualizar(Categorium c)
        {
            var res = new RespuestaService<Categorium>();
            var cat = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == c.Id);

            if (cat == null)
                res.AgregarBadRequest("El id recibido no está registrado");
            else
            {
                cat.Nombre = c.Nombre;

                try
                {
                    _context.Categoria.Update(cat);
                    await _context.SaveChangesAsync();

                    res.Objeto = cat;
                }
                catch (DbUpdateException ex)
                {
                    res.AgregarInternalServerError(ex.Message);
                }
            }

            return res;
        }

        public async Task<RespuestaService<Categorium>> BuscarPorId(decimal id)
        {
            var res = new RespuestaService<Categorium>();
            var cat = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == id);

            if (cat == null)
                res.AgregarBadRequest("El id recibido no está registrado");
            else
                res.Objeto = cat;

            return res;
        }

        public async Task<RespuestaService<bool>> Eliminar(decimal id)
        {
            var res = new RespuestaService<bool>();
            var cat = await _context.Categoria.FirstOrDefaultAsync(x => x.Id == id);

            if (cat == null)
                res.AgregarBadRequest("El id recibido no está registrado");
            else
            {
                try
                {
                    _context.Categoria.Remove(cat);
                    await _context.SaveChangesAsync();
                    res.Exito = true;
                }
                catch (DbUpdateException ex)
                {
                    res.AgregarInternalServerError(ex.Message);
                }
            }

            return res;
        }

        public async Task<RespuestaService<Categorium>> Guardar(Categorium c)
        {
            var res = new RespuestaService<Categorium>();

            try
            {
                await _context.Categoria.AddAsync(c);
                await _context.SaveChangesAsync();
                c.Id = await _context.Categoria.MaxAsync(u => u.Id);

                res.Objeto = c;
            }
            catch (DbUpdateException ex)
            {
                res.AgregarInternalServerError(ex.Message);
            }

            return res;
        }

        public async Task<RespuestaService<List<Categorium>>> Listar()
        {
            var res = new RespuestaService<List<Categorium>>();
            var lista = await _context.Categoria.ToListAsync();

            if (lista != null)
                res.Objeto = lista;
            else
                res.AgregarInternalServerError("Se encontró un error");

            return res; 
        }
    }
}
