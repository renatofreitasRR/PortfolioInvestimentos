
# Gestão de Portfólio de Investimentos

Sistema para gestão de portfólio de investimentos


## Stack utilizada

**Back-end:** .NET 8, Redis, Quartz, Docker

**Banco de dados:** SQL Server


## Uso de Tecnologias

- Redis para gerenciamento de caching distribuido
- Quartz para Eventos agendados
- FluentValidation para validação dos métodos de POST e PUT



## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/renatofreitasRR/PortfolioInvestimentos.git
```

Abra o Projeto com o Visual Studio

Selecione o Docker Compose como Projeto de Inicialização

Inicie o servidor

Substitua os valores para envio do email no arquivo appsettings.json:

```bash
   "EmailSettings": {
        "Email": "youremail",
        "DisplayName": "PortfolioInvestimentos",
        "Password": "yourpassword",
        "Host": "your.host.smtp",
        "Port": 587
    },
```




## Fluxo de funcionamento

- 1º Passo - Criação do Usuário (Manager/Client) 

```http
  POST /api/User/Post
```

- 2º Passo - Autenticação

```http
  POST /api/User/SignIn
```

- 3º Passo - Criar uma Conta

```http
  POST /api/Account/Post
```

- 4º Passo - Realizar um depósito

```http
  POST /api/Account/Deposit
```

- 5º Passo - Realizar transação de Compra

```http
   POST /api/Transaction/BuyTransaction
```

- 6º Passo - Retirar Extrato de Transações

```http
   GET /api/Transaction/GetExtractPaginated/${userId}
```
## Documentação Entidades

### User

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` |Id do Usuário|
| `Name`      | `string` |Nome do usuário|
| `Profile`      | `UserInvestorProfile` | Perfil de investidor do usuário |
| `Role`      | `UserRole` |Acessos do usuário |
| `Email`      | `string` |Email do usuário |
| `PasswordHash`      | `string` |Senha do usuário em Hash|
| `CreatedAt`      | `DateTime` |Data da Criação do Registro|

### Product

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` |Id do Produto|
| `Name`      | `string` |Nome do produto|
| `Type`      | `ProductType` |Tipo do produto |
| `Value`      | `decimal` |Valor do produto |
| `QuantityAvailable`      | `int` |Quantidade disponível |
| `DueDate`      | `DateTimme` | Data de Vencimento|
| `CreatedAt`      | `DateTime` |Data da Criação do Registro|

### Account

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` |Id da Conta|
| `UserId`      | `int` |Id do Usuário|
| `Value`      | `decimal` |Valor inicial da conta|
| `Transactions`      | `IEnumerable<Transaction>` |Transações|
| `CreatedAt`      | `DateTime` |Data da Criação do Registro|

### Transaction

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` |Id da Transação|
| `AccountId`      | `int` |Id da Conta|
| `ProductId`      | `string` |Id do Produto|
| `Quantity`      | `int` | Quantidade de Produtos Comprados ou Vendidos |
| `OperationType`      | `OperationType` | Compra ou Venda |
| `Value`      | `decimal` |Valor da Transação |
| `CreatedAt`      | `DateTime` |Data da Criação do Registro|

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
Acesso: Manager

```http
  GET /api/user/GetAll
```

#### Retorna um usuário
Acesso: Manager, Client

```http
  GET /api/User/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do usuário |

#### Criar um usuário
Acesso: Sem Restrições

```http
  POST /api/User/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Name`      | `string` | **Obrigatório**. Nome do usuário|
| `Profile`      | `UserInvestorProfile` | Perfil de investidor do usuário |
| `Role`      | `UserRole` | **Obrigatório**. Acessos do usuário |
| `Email`      | `string` | **Obrigatório**. Email do usuário |
| `Password`      | `string` | **Obrigatório**.  Senha do usuário|
| `ConfirmPassword`      | `string` | **Obrigatório**. Confirmação de senha do usuário |

#### Login
Acesso: Sem Restrições
```http
  POST /api/User/SignIn
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Email`      | `string` | **Obrigatório**. Email do usuário |
| `Password`      | `string` | **Obrigatório**.  Senha do usuário|


### Products

#### Retorna todos os produtos
Acesso: Manager, Client

```http
  GET /api/Product/GetAllPaged
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `PageNumber`      | `int` |  Número da página |
| `PageSize`      | `int` |  Quantidade de elementos a serem retornados |



#### Retorna um produto
Acesso: Manager, Client

```http
  GET /api/Product/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do produto |

#### Criar um Produto
Acesso: Manager
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
Acesso: Manager

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
Acesso: Manager
```http
  GET /api/Account/GetAll
```


#### Retorna uma conta
Acesso: Manager, Client
```http
  GET /api/Account/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID da conta |

#### Criar uma conta
Acesso: Manager, Client
```http
  POST /api/Account/Post
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `UserId`      | `int` | **Obrigatório**. Id do usuário|
| `Value`      | `decimal` | **Obrigatório**. Valor inicial da conta|

#### Depositar valor na conta
Acesso: Manager, Client
```http
  PUT /api/Account/Deposit
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` | **Obrigatório**. Id da conta|
| `Value`      | `decimal` | **Obrigatório**. Valor a depositar na conta|

#### Retirar valor da conta
Acesso: Manager, Client
```http
  PUT /api/Account/Withdraw
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `int` | **Obrigatório**. Id da conta|
| `Value`      | `decimal` | **Obrigatório**. Valor a se retirar da conta|


### Transactions

#### Retorna todos as transações
Acesso: Manager
```http
  GET /api/Transaction/GetAll
```

#### Retorna uma transação
Acesso: Manager, Client
```http
  GET /api/Transaction/GetById/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `string` | **Obrigatório**. O ID da transação |

#### Retorna histórico de transação por usuário
Acesso: Manager, Client
```http
  GET /api/Transaction/GetExtractPaginated/${userId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `string` | **Obrigatório**. Id do usuário |
| `PageNumber`      | `int` |  Número da página |
| `PageSize`      | `int` |  Quantidade de elementos a serem retornados |

#### Criar uma transação de compra
Acesso: Manager, Client
```http
  POST /api/Transaction/BuyTransaction
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `AccountId`      | `int` | **Obrigatório**. Id da conta|
| `ProductId`      | `int` | **Obrigatório**. Id do produto |
| `Quantity`      | `int` | **Obrigatório**. Quantidade de produtos na transação |

#### Criar uma transação de venda
Acesso: Manager, Client
```http
  POST /api/Transaction/SellTransaction
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `AccountId`      | `int` | **Obrigatório**. Id da conta|
| `ProductId`      | `int` | **Obrigatório**. Id do produto |
| `Quantity`      | `int` | **Obrigatório**. Quantidade de produtos na transação |








