﻿using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class Entrada : BaseEntity
{
    public int Quantidade { get; private set; }
    public string Local { get; private set; }
    public DateTimeOffset DataDaEntrada { get; private set; }
    
    public Guid MercadoriaId { get; private set; }
    public Mercadoria Mercadoria { get; private set; }

    public Entrada(int quantidade, string local, DateTimeOffset dataDaEntrada, Guid mercadoriaId)
    {
        Quantidade = quantidade;
        Local = local;
        DataDaEntrada = dataDaEntrada;
        MercadoriaId = mercadoriaId;
    }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        throw new NotImplementedException();
    }
}