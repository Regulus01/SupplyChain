namespace SupplyChain.Domain.Interface.Repository;

public interface IDatabaseService
{ 
    /// <summary>
    /// Executa uma query SQL e mapeia os resultados utilizando o ORM
    /// </summary>
    /// <param name="query">Consulta SQL a ser executada</param>
    /// <typeparam name="T">Entidade que será retornada</typeparam>
    /// <returns>Uma coleção de objetos do tipo <typeparamref name="T"/></returns>
    IEnumerable<T> Query<T>(string query) where T : class;
}