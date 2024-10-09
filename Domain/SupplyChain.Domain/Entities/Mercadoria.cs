using SupplyChain.Domain.Entities.Base;
using SupplyChain.Domain.Resourcers;
using NotificationDomain = SupplyChain.Domain.Notification.Notification;

namespace SupplyChain.Domain.Entities;

public class Mercadoria : BaseEntity
{
    public string NumeroDeRegistro { get; private set; }
    public string Nome { get; private set; }
    public string Fabricante { get; private set; }
    public string Descricao { get; private set; }
    
    public Guid TipoMercadoriaId { get; private set; }
    public TipoDeMercadoria TipoDeMercadoria { get; private set; }
    public IEnumerable<Estoque> Estoque { get; private set; }
    public virtual IEnumerable<Entrada> Entradas { get; private set; }
    public virtual IEnumerable<Saida> Saidas { get; private set; }

    public Mercadoria(string numeroDeRegistro, string nome, string fabricante, string descricao, Guid tipoMercadoriaId)
    {
        NumeroDeRegistro = numeroDeRegistro;
        Nome = nome;
        Fabricante = fabricante;
        Descricao = descricao;
        TipoMercadoriaId = tipoMercadoriaId;
    }
    
    public override (bool IsValid, IEnumerable<NotificationDomain> Erros) Validate()
    {
        var erros = ValidarCampos();

        return (erros.Count == 0, erros);
    }

    /// <summary>
    /// Valida os campos da entidade e adiciona uma mensagem de erro no dicionário caso houver.
    /// </summary>
    /// <returns>Dicionario com mensagem de erros</returns>
    private List<NotificationDomain> ValidarCampos()
    {
        var erros = new List<NotificationDomain>();
        
        ValidarCampoObrigatorio(NumeroDeRegistro, ErrorMessage.MER_NUM_REGISTRO_COM_ESPACOS, erros);
        ValidarCampoObrigatorio(Nome, ErrorMessage.MER_NOME_VAZIO, erros);
        ValidarCampoObrigatorio(Fabricante, ErrorMessage.MER_FABRICANTE_VAZIO, erros);
        ValidarCampoObrigatorio(Descricao, ErrorMessage.MER_DESCRICAO_VAZIA, erros);
        ValidarTipoMercadoria(erros);

        return erros;
    }

    /// <summary>
    /// Realiza validações para o tipo de mercadoria
    /// </summary>
    /// <param name="erros">Dicionário que armazena os códigos e mensagens de erro encontrados durante a validação</param>
    private void ValidarTipoMercadoria(List<NotificationDomain> erros)
    {
        if (TipoMercadoriaId == Guid.Empty)
        {
            erros.Add(new NotificationDomain(ErrorMessage.MER_TIPO_MERCADORIA_VAZIO.Code, 
                                             ErrorMessage.MER_TIPO_MERCADORIA_VAZIO.Message));
        }
    }

    /// <summary>
    /// Verifica se um campo obrigatório está vazio ou nulo, e adiciona um erro se necessário.
    /// </summary>
    /// <param name="campo">Campo a ser validado</param>
    /// <param name="errorMessage">Messagem de erro a ser armazenada caso o campo seja invalido </param>
    /// <param name="erros">Dicionário que armazena os códigos e mensagens de erro encontrados durante a validação</param>
    private static void ValidarCampoObrigatorio(string campo, (string Code, string Message) errorMessage, 
        List<NotificationDomain> erros)
    {
        if (string.IsNullOrWhiteSpace(campo))
        {
            erros.Add(new NotificationDomain(errorMessage.Code, errorMessage.Message));
        }
    }
}