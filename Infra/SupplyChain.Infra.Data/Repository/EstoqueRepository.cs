using Microsoft.Extensions.Logging;
using SupplyChain.Domain.Database.ViewModels;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository.Base;

namespace SupplyChain.Infra.Data.Repository;

public class EstoqueRepository : BaseRepository<InventarioDbContext, EstoqueRepository>, IEstoqueRepository
{
    private readonly IDatabaseService _databaseService;
    public EstoqueRepository(InventarioDbContext context, ILogger<EstoqueRepository> logger, IDatabaseService databaseService) : base(context, logger)
    {
        _databaseService = databaseService;
    }

    public IEnumerable<DbObterLocaisDeEstoqueViewModel> ObterListagemDeLocais(Guid mercadoriaId, int? skip = null,
        int? take = null)
    {
        var query = "select e.\"Est_Local\"" +
                    "from \"Inventario\".\"Estoque\" e";

        query += $" where e.\"Merc_MercadoriaId\" = '{mercadoriaId}'";
        query += " order by e.\"Est_Local\" asc";
        
        if (skip != null && take != null)
            query += $" offset {skip} limit {take}";
        
        var result = _databaseService.Query<DbObterLocaisDeEstoqueViewModel>(query);

        return result;
    }
}