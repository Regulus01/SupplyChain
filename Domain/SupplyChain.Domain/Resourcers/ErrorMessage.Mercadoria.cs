﻿namespace SupplyChain.Domain.Resourcers;

public partial class ErrorMessage
{
    public static (string Code, string Message) MER_NOME_VAZIO =
        ("Mer00x1", "O nome para a mecadoria é obrigatório.");

    public static (string Code, string Message) MER_FABRICANTE_VAZIO =
        ("Mer00x2", "O fabricante é obrigatório.");
    
    public static (string Code, string Message) MER_DESCRICAO_VAZIA =
        ("Mer00x3", "A descrição é obrigatória.");
    
    public static (string Code, string Message) MER_NUM_REGISTRO_COM_ESPACOS =
        ("Mer00x4", "O numero de registro possui caracters em branco.");
    
    public static (string Code, string Message) MER_TIPO_MERCADORIA_VAZIO =
        ("Mer00x5", "É necessário informar um id válido para o tipo de mercadoria");
    
    public static (string Code, string Message) MER_CODIGOS_IGUAIS =
        ("Mer00x6", "A mercadoria com o nome de código informados, já foi cadastrada");
}