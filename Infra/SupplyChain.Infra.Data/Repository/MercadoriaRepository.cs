using Microsoft.Extensions.Logging;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository.Base;

namespace SupplyChain.Infra.Data.Repository;

public class MercadoriaRepository : BaseRepository<InventarioDbContext, MercadoriaRepository>, IMercadoriaRepository
{
    public MercadoriaRepository(InventarioDbContext context, ILogger<MercadoriaRepository> logger) : base(context, logger)
    {
    }
}