
# SupplyChain

Esse projeto é uma gerenciador de entrada e saidas de mercadoria. Cujo o objetivo faz parte do processo seletivo da mStar.





## Features

- Classes de dominio ricas
- Notification pattern
- Encapsulamento
- Inversão de dependência
- Testes unitários
- Utilização de classes genêricas
- Uso de DTO e ViewModels
- Documentação do swagger
- Versionamento
- API Rest

## Fluxo de Processos de Gestão de Mercadorias

* Cadastro de Tipo de Mercadoria:
Inicie o processo cadastrando um novo tipo de mercadoria no sistema.

*  Cadastro da Mercadoria:
Após cadastrar o tipo, registre a mercadoria correspondente.

* Cadastro de Estoque:
Após o cadastro da mercadoria, registe um estoque em uma localidade.

* Gerenciamento de Entradas e Saídas:
Com o estoque configurado, inicie o fluxo de entradas e saídas, registrando movimentações de mercadorias.




# API Reference

## Estoque

#### Post de Estoque

```http
  Post /api/v1/Estoque
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `local` | `string` | **Required**. Titulo  |
| `mercadoriaId` | `guid` | **Estoque** Id da mercadoria |


#### Post de Entradas

```http
  PUT /api/v1/Estoque/Entrada
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `quantidade` | `id` | **Required**. Quantidade de itens  |
| `local` | `string` | **Required**. Local do estoque  |
| `DataDeEntrada` | `DateTime` | **Required**. Data da entrada   |


#### Post de Saidas

```http
  PUT /api/v1/Estoque/Saidas
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `quantidade` | `id` | **Required**. Quantidade de itens  |
| `local` | `string` | **Required**. Local do estoque  |
| `DataDaSaida` | `DateTime` | **Required**. Data da saida   |


#### Get De Locais

```http
  GET /api/v1/Estoque/{MercadoriaId}/Locais
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `mercadoriaId` | `Guid` | **Required**. Id da mercadoria |
| `skip` | `int` | Inicio da paginação |
| `take` | `int` | Total de itens |


#### Get De relatorio Anual

```http
  GET /api/v1/Estoque/Relatorio/{MercadoriaId}/Ano
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `mercadoriaId` | `Guid` | **Required**. Id da mercadoria |
| `ano` | `int` | **Required**. Ano que o relatorio será gerado na faixa de 1900 e o ano atual |

#### Get De Entradas

```http
  GET /api/v1/Estoque/Entradas/{MercadoriaId}/Ano/Mes
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `mercadoriaId` | `Guid` | **Required**. Id da mercadoria |
| `ano` | `int` | **Required**. Ano que o relatorio será gerado na faixa de 1900 e o ano atual |
| `mes` | `int` | **Required**. Mes que o relatório será gerado na faixa de 1 a 12 |

#### Get De Saidas

```http
  GET /api/v1/Estoque/Saidas/{MercadoriaId}/Ano/Mes
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `mercadoriaId` | `Guid` | **Required**. Id da mercadoria |
| `ano` | `int` | **Required**. Ano que o relatorio será gerado na faixa de 1900 e o ano atual |
| `mes` | `int` | **Required**. Mes que o relatório será gerado na faixa de 1 a 12 |

## Mercadoria

#### Post de mercadoria

```http
  Post /api/v1/Mercadoria
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `numeroDoRegistro` | `string` | **Required**. Numero de registro da mercadoria  |
| `nome` | `string` | **Required**. Nome da mercadoria |
| `fabricante` | `string` | **Required**. Nome do fabricante da mercadoria |
| `descricao` | `string` | **Required**. Descrição da mercadoria |
| `tipoMercadoriaId` | `Guid` | **Required** id do tipo da mercadoria |

```http
  GET /api/v1/Mercadoria
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `skip` | `int` | Inicio da paginação |
| `take` | `int` | Total de itens |

#### Tipo De Mercadoria

#### Post de mercadoria

```http
  Post /api/v1/TipoDeMercadoria
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `nome` | `string` | **Required**. Nome do tipo de mercadoria  |

```http
  GET /api/v1/TipoDeMercadoria
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `skip` | `int` | Inicio da paginação |
| `take` | `int` | Total de itens |
## Authors

- [@jose](https://github.com/Regulus01)

