using Microsoft.Extensions.Logging;
using SupplyChain.Domain.Database.ViewModels;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository.Base;

namespace SupplyChain.Infra.Data.Repository;

public class MercadoriaRepository : BaseRepository<InventarioDbContext, MercadoriaRepository>, IMercadoriaRepository
{
    private readonly IDatabaseService _databaseService;

    public MercadoriaRepository(InventarioDbContext context, ILogger<MercadoriaRepository> logger,
        IDatabaseService databaseService) : base(context, logger)
    {
        _databaseService = databaseService;
    }
    
    public IEnumerable<DbObterMercadoriaViewModel> ObterListagem(int? skip, int? take)
    {
        var query = "select t.\"Id\", t.\"Mer_Nome\"" +
                    "from \"Inventario\".\"Mercadoria\" t";

        query += " order by t.\"Mer_Nome\" desc";
        
        if (skip != null && take != null)
            query += $" offset {skip} limit {take}";
        
        var result = _databaseService.Query<DbObterMercadoriaViewModel>(query);

        return result;
    }
}