using SupplyChain.Domain.Database.ViewModels;
using SupplyChain.Domain.Interface.Base;

namespace SupplyChain.Domain.Interface.Repository;

public interface ITipoDeMercadoriaRepository : IBaseRepository
{
    IEnumerable<DbObterTipoDeMercadoriaViewModel> ObterListagem(int? skip, int? take);
}