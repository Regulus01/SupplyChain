using System.Globalization;
using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
using SupplyChain.Domain.Entities;
using SupplyChain.Domain.Entities.Base;
using SupplyChain.Domain.Resourcers;
using EstoqueDomain = SupplyChain.Domain.Entities.Estoque;
using MercadoriaDomain = SupplyChain.Domain.Entities.Mercadoria;

namespace SupplyChain.Application.Services.Estoque;

public partial class EstoqueAppService
{
    /// <summary>
    /// Valida se existe um estoque no local informado
    /// </summary>
    /// <remarks>
    ///  Retorna o estoque como variável de saída caso exista
    /// </remarks>
    /// <param name="local">Local que o estoque se encontra</param>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="estoque">Variável de saida para o estoque existente</param>
    /// <returns>Retorna <c>false</c> se o local não existe e <c>true</c> caso exista.</returns>
    private bool ValidarLocalDoEstoqueExistente(string local, Guid mercadoriaId, out EstoqueDomain? estoque)
    {
        estoque = _repository.Query<EstoqueDomain>(x => x.Local.ToLower().Equals(local.ToLower()) &&
                                                        x.MercadoriaId == mercadoriaId)
            .FirstOrDefault();

        if (estoque == null)
        {
            _bus.Notify.NewNotification("Erro", "Não existe um estoque ou mercadoria para o local informado.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida se existe uma mercadoria existente para o mercadoriaId informado
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="mercadoria">Váriavel de saída para a mercadoria, caso exista</param>
    /// <returns>Retorna <c>false</c> se mercadoria não existe e <c>true</c> caso exista.</returns>
    private bool ValidarMercadoriaExistente(Guid mercadoriaId, out MercadoriaDomain? mercadoria)
    {
        mercadoria = _repository.Query<MercadoriaDomain>(x => x.Id.Equals(mercadoriaId))
            .FirstOrDefault();

        if (mercadoria == null)
        {
            _bus.Notify.NewNotification("Erro", "A mercadoria informada não existe.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// A partir de um dto de castrar estoque, cria um estoque
    /// </summary>
    /// <param name="dto">Dados necessários para criação</param>
    /// <returns>Estoque</returns>
    private EstoqueDomain CriarEstoque(CadastrarEstoqueDto dto)
    {
        var estoque = new EstoqueDomain(dto.Local, dto.MercadoriaId);

        return estoque;
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
    /// Altera uma data para o formato utc −3 hr
    /// </summary>
    /// <param name="data"></param>
    /// <returns>Data convertida</returns>
    private DateTimeOffset AlterarDataParaUtc(DateTime data)
    {
        return data.AddHours(-3).ToUniversalTime();
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
    /// Valida o estoque atual
    /// </summary>
    /// <param name="dto">Dto do tipo <see cref="CadastrarSaidaDto"/> utilizado para as validações</param>
    /// <param name="estoque"><see cref="EstoqueDomain"/> a ser validado</param>
    /// <returns>Se valido <c>true</c> caso contrário <c>false</c></returns>
    private bool ValidarEstoque(CadastrarSaidaDto dto, EstoqueDomain? estoque)
    {
        if (estoque is { Quantidade: <= 0 } || estoque?.Quantidade < dto.Quantidade)
        {
            _bus.Notify.NewNotification("Erro", "A quantidade do estoque não é o suficiente para a retirada");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Cria uma entidade do tipo <see cref="Saida"/>
    /// </summary>
    /// <param name="dto">Dados do tipo <see cref="CadastrarSaidaDto"/> para a criação da entidade</param>
    /// <returns>Entidade do tipo Saida </returns>
    private Saida CriarSaida(CadastrarSaidaDto dto)
    {
        var dataDeSaida = AlterarDataParaUtc(dto.DataDaSaida);

        var saida = new Saida(dto.Quantidade, dto.Local, dataDeSaida, dto.MercadoriaId);

        return saida;
    }

    /// <summary>
    /// Salva no banco de dados, caso possua erros, gera uma notificação
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

    /// <summary>
    /// Preenche o dicionário de totais mensais com a contagem de saídas de uma mercadoria em um determinado ano.
    /// </summary>
    /// <param name="cultura">Objeto CultureInfo que define a cultura a ser utilizada, como formato de data
    /// (ex: "pt-BR").</param>
    /// <param name="meses">Dicionário onde as chaves são os nomes dos meses e os valores são os totais de
    /// entrada e saída.</param>
    /// <param name="mercadoriaId">Identificador único da mercadoria cujas saídas serão contadas.</param>
    /// <param name="ano">Ano de referência para filtrar as saídas.</param>
    private void AtualizarTotaisSaidasPorMes(CultureInfo cultura, Dictionary<string, TotaisMensaisViewModel> meses,
        Guid mercadoriaId, int ano)
    {
        var saidas = _repository.Query<Saida>(x => x.DataDaSaida.Year == ano && x.MercadoriaId == mercadoriaId)
            .ToList();

        foreach (var saida in saidas)
        {
            var mes = saida.DataDaSaida.ToString("MMMM", cultura);
            meses[mes].TotalDeSaida += 1;
        }
    }

    /// <summary>
    /// Atualiza o dicionário de totais mensais com a contagem de entradas de uma mercadoria em um determinado ano.
    /// </summary>
    /// <param name="cultura">Objeto CultureInfo que define a cultura a ser utilizada, como formato de data
    /// (ex: "pt-BR").</param>
    /// <param name="meses">Dicionário onde as chaves são os nomes dos meses e os valores são os totais de
    /// entrada e saída.</param>
    /// <param name="mercadoriaId">Identificador único da mercadoria cujas entradas serão contadas.</param>
    /// <param name="ano">Ano de referência para filtrar as entradas.</param>
    private void AtualizarTotaisEntradasPorMes(CultureInfo cultura, Dictionary<string, TotaisMensaisViewModel> meses,
        Guid mercadoriaId, int ano)
    {
        var entradas = _repository.Query<Entrada>(x => x.DataDaEntrada.Year == ano && x.MercadoriaId == mercadoriaId)
            .ToList();

        foreach (var entrada in entradas)
        {
            var mes = entrada.DataDaEntrada.ToString("MMMM", cultura);
            meses[mes].TotalDeEntrada += 1;
        }
    }

    /// <summary>
    /// Gera um dicionário contendo os meses do ano e inicializa os totais mensais com valores zero.
    /// </summary>
    /// <param name="cultura">Saída que retorna o objeto CultureInfo para a cultura "pt-BR".</param>
    /// <returns>Dicionário onde as chaves são os nomes dos meses e os valores são instâncias de
    /// TotaisMensaisViewModel, com os totais inicializados.</returns>
    private static Dictionary<string, TotaisMensaisViewModel> GerarDicionarioDeTotaisMensais(out CultureInfo cultura)
    {
        cultura = new CultureInfo("pt-BR");

        var meses = new Dictionary<string, TotaisMensaisViewModel>();

        for (var i = 1; i <= 12; i++)
        {
            var nomeMes = cultura.DateTimeFormat.GetMonthName(i);
            meses[nomeMes] = new TotaisMensaisViewModel();
            meses[nomeMes] = new TotaisMensaisViewModel();
        }

        return meses;
    }
}