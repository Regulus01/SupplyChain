using Microsoft.Extensions.Logging;
using SupplyChain.Domain.Database.ViewModels;
using SupplyChain.Domain.Entities;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository.Base;

namespace SupplyChain.Infra.Data.Repository;

public class TipoDeMercadoriaRepository : BaseRepository<InventarioDbContext, TipoDeMercadoriaRepository>, 
                                          ITipoDeMercadoriaRepository
{
    private readonly IDatabaseService _databaseService;

    public TipoDeMercadoriaRepository(InventarioDbContext context, ILogger<TipoDeMercadoriaRepository> logger,
        IDatabaseService databaseService)
        : base(context, logger)
    {
        _databaseService = databaseService;
    }
    
    public IEnumerable<DbObterTipoDeMercadoriaViewModel> ObterListagem(int? skip, int? take)
    {
        var query = "select t.\"Id\", t.\"Tip_Nome\"" +
                    "from \"Inventario\".\"TipoDeMercadoria\" t";

        query += " order by t.\"Tip_Nome\" desc";
        
        if (skip != null && take != null)
            query += $" offset {skip} limit {take}";
        
        var result = _databaseService.Query<DbObterTipoDeMercadoriaViewModel>(query);

        return result;
    }
}