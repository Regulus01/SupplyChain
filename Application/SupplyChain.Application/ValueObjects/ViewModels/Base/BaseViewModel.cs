﻿namespace SupplyChain.Application.ValueObjects.ViewModels.Base;

public class BaseViewModel
{
    /// <summary>
    /// Id da entidade
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Data da criação
    /// </summary>
    public DateTimeOffset DataDeCriacao { get; set; }
    
    /// <summary>
    /// Data de alteração
    /// </summary>
    public DateTimeOffset DataDeAlteracao { get; set; }
}