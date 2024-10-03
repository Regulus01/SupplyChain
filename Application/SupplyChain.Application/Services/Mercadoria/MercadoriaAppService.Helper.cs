using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Domain.Entities;
using SupplyChain.Domain.Entities.Base;
using SupplyChain.Domain.Resourcers;

namespace SupplyChain.Application.Services.Mercadoria;

using TipoDeMercadoriaDomain = Domain.Entities.TipoDeMercadoria;
using MercadoriaDomain = Domain.Entities.Mercadoria;

public partial class MercadoriaAppService
{
    /// <summary>
    /// Valida se uma mercadoria já existe no sistema com o mesmo código e se o tipo de mercadoria fornecido é válido.
    /// </summary>
    /// <param name="codigoDaMercadoria">Código da mercadoria a ser verificado.</param>
    /// <param name="tipoDaMercadoriaId">Identificador do tipo de mercadoria a ser validado.</param>
    /// <param name="tipoDeMercadoria">Retorna o objeto TipoDeMercadoriaDomain correspondente se encontrado,
    /// caso contrário, será null.</param>
    /// <returns>Retorna <c>false</c> se a mercadoria está inválida e <c>true</c> caso válida.</returns>
    private bool ValidarTipoEMercadoriaExistente(string codigoDaMercadoria, Guid tipoDaMercadoriaId,
        out TipoDeMercadoriaDomain? tipoDeMercadoria)
    {
        tipoDeMercadoria = _repository.Query<TipoDeMercadoriaDomain>(x => x.Id.Equals(tipoDaMercadoriaId))
                                      .FirstOrDefault();
        
        var mercadoria = _repository.Query<MercadoriaDomain>(x => x.NumeroDeRegistro.Equals(codigoDaMercadoria))
            .FirstOrDefault();

        if (mercadoria != null)
        {
            _bus.Notify.NewNotification(ErrorMessage.MER_CODIGOS_IGUAIS.Code, 
                ErrorMessage.MER_CODIGOS_IGUAIS.Message);
            return false;
        }
        
        if (tipoDeMercadoria == null)
        {
            _bus.Notify.NewNotification(ErrorMessage.TIP_MERCADORIA_NAO_EXISTE.Code, 
                ErrorMessage.TIP_MERCADORIA_NAO_EXISTE.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida se uma mercadoria é valida para a inserção no sistema
    /// </summary>
    /// <param name="entity">Entidade a ser validada </param>
    /// <returns>Retorna <c>false</c> se a entity está inválida e <c>true</c> caso válida.</returns>
    private bool Validar(BaseEntity entity)
    {
        var validacao = entity.Validate();
        
        if (!validacao.IsValid)
        {
            _bus.Notify.NewNotification(validacao.Erros);
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Cria uma entidade de dominio de mercadoria a partir de um dto
    /// </summary>
    /// <param name="dto">Dados necessários para a criação</param>
    /// <returns>Entidade de dominio de mercadoria</returns>
    private MercadoriaDomain CriarMercadoriaDomain(CriarMercadoriaDto dto)
    {
        return new MercadoriaDomain(
            dto.NumeroDeRegistro,
            dto.Nome,
            dto.Fabricante,
            dto.Descricao,
            dto.TipoMercadoriaId
        );
    }
    
    /// <summary>
    /// Cria uma entrada a partir de um dto
    /// </summary>
    /// <param name="dto">Dados necessários para criação</param>
    /// <returns>Entidade entrada</returns>
    private Entrada CriarEntrada(CadastrarEntradaDto dto)
    {
        var dataDeEntrada = AlterarDataParaUtc(dto.DataDaEntrada);
        
        var entrada = new Entrada(dto.Quantidade, dto.Local, dataDeEntrada, dto.MercadoriaId);
        return entrada;
    }

    /// <summary>
    /// Altera uma data para o formato utc -3 hr
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private DateTime AlterarDataParaUtc(DateTime data)
    {
        return data.AddHours(-3);  
    }
    
    /// <summary>
    /// Salva no banco de dados, caso haja erro, gera uma notificação
    /// </summary>
    /// <returns><c>true</c> caso não haja erros, caso contrário <c>false</c> </returns>
    private bool SaveChanges()
    {
        if (!_repository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.ERRO_SALVAR.Code, 
                ErrorMessage.ERRO_SALVAR.Message);
            return false;
        }

        return true;
    }
}