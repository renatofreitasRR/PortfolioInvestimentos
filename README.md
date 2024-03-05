
# Gestão de Portfólio de Investimentos

Sistema para gestão de portfólio de investimentos


## Stack utilizada

**Back-end:** .NET 8, Redis, Docker

**Banco de dados:** SQL Server


## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/renatofreitasRR/PortfolioInvestimentos.git
```

Abra o Projeto com o Visual Studio

Selecione o Docker Compose como Projeto de Inicialização

Inicie o servidor




## Documentação dos Enumerados

### User
#### UserInvestorProfile

| Nome   | Valor       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| Conservative       | 1 | Conservador |
| Moderate       | 2 | Moderado |
| Aggressive       | 3 | Expert/Agressivo |

#### UserRole

| Nome   | Valor       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| Manager       | 1 | Gerente |
| Client       | 2 | Cliente |

### Product
#### ProductType

| Nome   | Valor       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| Stocks       | 1 | Ações |
| FixedIncomeSecurities       | 2 | Títulos de Renda Fixa |
| GovernmentBonds       | 3 | Títulos Públicos |
| InvestmentFunds       | 4 | Fundos de Investimento |
| ExchangeTradedFunds       | 5 | ETFs (Fundos Negociados em Bolsa) |
| RealEstate       | 6 | Investimento em Imóveis |
| Derivatives       | 7 | Derivativos |
| Commodities       | 8 | Commodities |
| Cryptocurrencies       | 9 | Criptomoedas |
| RetirementSavings       | 10 | Previdência Privada |

### Transaction
#### OperationType


| Nome   | Valor       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| Buy       | 1 | Compra |
| Sell       | 2 | Venda |




## Documentação da API


### Retorno
#### Toda end-point tem o retorno padrão no formato: 

```http
  {
    Data: object,
    Errors: List<string>
  }
```

### Exceções
#### Para toda exceção retornamos no mesmo formato

```http
  {
    Data: null,
    Errors: List<string>
  }
```

### Users

#### Retorna todos os usuários

```http
  GET /api/user/GetAll
```

#### Retorna um usuário

```http
  GET /api/User/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do usuário |

#### Criar um usuário

```http
  POST /api/User/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Name`      | `string` | **Obrigatório**. Nome do usuário|
| `Profile`      | `UserInvestorProfile` | **Opcional**. Perfil de investidor do usuário |
| `Role`      | `UserRole` | **Obrigatório**. Acessos do usuário |
| `Email`      | `string` | **Obrigatório**. Email do usuário |
| `Password`      | `string` | **Obrigatório**.  Senha do usuário|
| `ConfirmPassword`      | `string` | **Obrigatório**. Confirmação de senha do usuário |

#### Login

```http
  POST /api/User/SignIn
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Email`      | `string` | **Obrigatório**. Email do usuário |
| `Password`      | `string` | **Obrigatório**.  Senha do usuário|


### Products

#### Retorna todos os produtos

```http
  GET /api/Product/GetAll
```

#### Retorna um produto

```http
  GET /api/Product/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto |

#### Criar um Produto

```http
  POST /api/Product/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Name`      | `string` | **Obrigatório**. Nome do produto|
| `Type`      | `ProductType` | **Obrigatório**. Tipo do produto |
| `Value`      | `decimal` | **Obrigatório**. Valor do produto |
| `QuantityAvailable`      | `int` | **Obrigatório**. Quantidade disponível |
| `DueDate`      | `DateTimme` | **Obrigatório**.  Data de Vencimento|

#### Editar um Produto

```http
  PUT /api/Product/Put
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` | **Obrigatório**. Id do Produto|
| `Name`      | `string` | **Obrigatório**. Nome do produto|
| `Type`      | `ProductType` | **Obrigatório**. Tipo do produto |
| `Value`      | `decimal` | **Obrigatório**. Valor do produto |
| `QuantityAvailable`      | `int` | **Obrigatório**. Quantidade disponível |
| `DueDate`      | `DateTimme` | **Obrigatório**.  Data de Vencimento|
| `IsActive`      | `bool` | **Obrigatório**.  Produto está ativo|


### Accounts

#### Retorna todas as contas

```http
  GET /api/Account/GetAll
```


#### Retorna uma conta

```http
  GET /api/Account/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID da conta |

#### Criar uma conta

```http
  POST /api/Account/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `UserId`      | `int` | **Obrigatório**. Id do usuário|
| `Value`      | `decimal` | **Obrigatório**. Valor inicial da conta|

#### Depositar valor na conta

```http
  PUT /api/Account/Deposit
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` | **Obrigatório**. Id da conta|
| `Value`      | `decimal` | **Obrigatório**. Valor a depositar na conta|

#### Retirar valor da conta

```http
  PUT /api/Account/Withdraw
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` | **Obrigatório**. Id da conta|
| `Value`      | `decimal` | **Obrigatório**. Valor a se retirar da conta|


### Transactions

#### Retorna todos as transações


```http
  GET /api/Transaction/GetAll
```

#### Retorna uma transação

```http
  GET /api/Transaction/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID da transação |

#### Criar uma transação

```http
  POST /api/Transaction/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `AccountId`      | `int` | **Obrigatório**. Id da conta|
| `ProductId`      | `int` | **Obrigatório**. Id do produto |
| `Quantity`      | `int` | **Obrigatório**. Quantidade de produtos na transação |
| `OperationType`      | `OperationType` | **Obrigatório**. Tipo da operação |
| `Value`      | `Decimal` | **Obrigatório**.  Valor total da transação|





## Funcionalidades

- Temas dark e light
- Preview em tempo real
- Modo tela cheia
- Multiplataforma

