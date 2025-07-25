using Microsoft.EntityFrameworkCore;
using asp_net_api_tarefas.Model;

namespace asp_net_api_tarefas.Context
{
    public class TarefaContexto : DbContext
    {
        public TarefaContexto(DbContextOptions<TarefaContexto> options) :base(options)
        {

        }

        public DbSet<TarefaItem> Tarefas { get; set; } = null;
    }
}
