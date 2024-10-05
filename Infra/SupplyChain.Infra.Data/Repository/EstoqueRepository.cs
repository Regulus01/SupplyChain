using Microsoft.Extensions.Logging;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository.Base;

namespace SupplyChain.Infra.Data.Repository;

public class EstoqueRepository : BaseRepository<InventarioDbContext, EstoqueRepository>, IEstoqueRepository
{
    public EstoqueRepository(InventarioDbContext context, ILogger<EstoqueRepository> logger) : base(context, logger)
    {
    }
}