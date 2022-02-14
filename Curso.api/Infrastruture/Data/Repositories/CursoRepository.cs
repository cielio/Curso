using curso.api.Business.Repositories;
using cursos.api.Business.Entities;
using cursos.api.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Infrastruture.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _cursoDbContext;

        public CursoRepository(CursoDbContext cursoDbContext)
        {
            _cursoDbContext = cursoDbContext;
        }

        public void Adicionar(Curso curso)
        {
            _cursoDbContext.Add(curso);
        }

        public void Commit()
        {
            _cursoDbContext.SaveChanges();
        }

        public IList<Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _cursoDbContext.Curso.Include(i => i.Usuario).Where(curso => curso.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
