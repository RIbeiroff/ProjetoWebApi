using Microsoft.EntityFrameworkCore;
using Projeto.WebAPI.Model;

namespace Projeto.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<Compra> Compras { get; set; }

    }
}