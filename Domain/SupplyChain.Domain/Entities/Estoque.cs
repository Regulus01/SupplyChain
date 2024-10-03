﻿using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class Estoque : BaseEntity
{
    public string Local { get; private set; }
    public int Quantidade { get; private set; }
    
    public Guid MercadoriaId { get; private set; }
    public Mercadoria Mercadoria { get; private set; }

    public Estoque(string local, int quantidade, Guid mercadoriaId)
    {
        Local = local;
        Quantidade = quantidade;
        MercadoriaId = mercadoriaId;
    }

    public void AdicionarEstoque(int quantidade)
    {
        Quantidade += quantidade;
    }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        var erros = new Dictionary<string, string>();
        
        return (erros.Count == 0, erros); 
    }
}