using SupplyChain.Domain.Database.ViewModels;
using SupplyChain.Domain.Interface.Base;

namespace SupplyChain.Domain.Interface.Repository;

public interface IEstoqueRepository : IBaseRepository
{
    IEnumerable<DbObterLocaisDeEstoqueViewModel> ObterListagemDeLocais(Guid mercadoriaId, int? skip = null,
        int? take = null);
}