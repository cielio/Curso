using curso.api.Business.Repositories;
using cursos.api.Business.Entities;
using cursos.api.Infrastruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Infrastruture.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _cursoDbContext;

        public UsuarioRepository(CursoDbContext cursoDbContext)
        {
            _cursoDbContext = cursoDbContext;
        }

        public void Adicionar(Usuario usuario)
        {
            _cursoDbContext.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _cursoDbContext.SaveChanges();
        }

        public Usuario ObterUsuario(string login)
        {
            return _cursoDbContext.Usuario.FirstOrDefault(usuario => usuario.Login == login);
        }
    }
}
