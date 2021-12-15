using EjemploOracle.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploOracle.Services
{
    public interface ICategoriaService
    {
        Task<RespuestaService<List<Categorium>>> Listar();

        Task<RespuestaService<Categorium>> BuscarPorId(decimal id);

        Task<RespuestaService<Categorium>> Guardar(Categorium c);

        Task<RespuestaService<Categorium>> Actualizar(Categorium c);

        Task<RespuestaService<bool>> Eliminar(decimal id);
    }
}
