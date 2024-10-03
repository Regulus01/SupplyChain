using Microsoft.Extensions.Logging;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository.Base;

namespace SupplyChain.Infra.Data.Repository;

public class TipoDeMercadoriaRepository : BaseRepository<InventarioDbContext, TipoDeMercadoriaRepository>, 
                                          ITipoDeMercadoriaRepository
{
    public TipoDeMercadoriaRepository(InventarioDbContext context, ILogger<TipoDeMercadoriaRepository> logger) 
        : base(context, logger)
    {
    }
}