using SupplyChain.Domain.Database.ViewModels;
using SupplyChain.Domain.Interface.Base;

namespace SupplyChain.Domain.Interface.Repository;

public interface IMercadoriaRepository : IBaseRepository
{
    IEnumerable<DbObterMercadoriaViewModel> ObterListagem(int? skip, int? take);
}