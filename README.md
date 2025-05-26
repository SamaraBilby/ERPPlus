# 📦 ERP Plus - Sistema de Gestão com Blazor e ASP.NET Core

Este projeto é um sistema de gestão ERP simples, desenvolvido com tecnologias modernas como:

- Blazor Server para a interface web
- ASP.NET Core (.NET 8)
- Entity Framework Core com PostgreSQL
- Docker para conteinerização e execução local

---

## 🛠️ Funcionalidades

- ✅ Cadastro de **clientes**
- ✅ Cadastro de **produtos**
- ✅ Criação de **pedidos** com múltiplos produtos e clientes
- ✅ Listagem paginada e detalhada
- ✅ Validações com **FluentValidation**
- ✅ Manipulação de exceções com **middleware global**
- ✅ API RESTful com **Swagger**
- ✅ Deploy local com **Docker**

---

## 🚀 Como executar o projeto com Docker

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio 2022/2023 ou VS Code](https://visualstudio.microsoft.com/) (opcional)

---

### 🧱 Passo a passo para rodar com Docker

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/ERPPlus.git
cd ERPPlus
```

2. **Suba o banco PostgreSQL com Docker**
```bash
docker-compose up -d
```

> Esse comando sobe um container do PostgreSQL com as credenciais configuradas no `appsettings.json`.

3. **Crie o banco e execute as migrations**
```bash
cd ERPPlus.WebAPI
dotnet ef database update
```

> ⚠️ Se for a primeira vez, adicione o pacote do EF:
```bash
dotnet tool install --global dotnet-ef
```

4. **Execute a aplicação**

```bash
# No terminal da WebAPI
dotnet run --project ERPPlus.WebAPI

# Em outro terminal (para o Blazor Server)
dotnet run --project ERPPlus.BlazorUI
```

---

## 🌐 URLs Padrão

| Projeto       | URL                             |
|---------------|----------------------------------|
| API (Swagger) | https://localhost:7237/swagger   |
| Blazor UI     | https://localhost:7052           |

---

## 🧪 Endpoints disponíveis

Acesse o Swagger em `https://localhost:7237/swagger` para visualizar todos os endpoints da API.

- `GET /api/customer`
- `POST /api/customer`
- `GET /api/product`
- `POST /api/order`
- ...

---

## 📂 Estrutura do Projeto

```
ERPPlus/
├── ERPPlus.Domain/            # Entidades e interfaces
├── ERPPlus.Application/       # DTOs, Services e Validators
├── ERPPlus.Infrastructure/    # Repositórios e DbContext
├── ERPPlus.WebAPI/            # API ASP.NET Core
├── ERPPlus.BlazorUI/          # Interface web Blazor Server
└── docker-compose.yml         # Infraestrutura local
```

---

## ⚙️ Configurações

O arquivo `appsettings.json` possui as configurações do banco

---

## 🧾 Tecnologias Usadas

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- Docker
- Blazor Server
- FluentValidation
- Swagger
- Clean Architecture (Camadas separadas)

---

## 📌 Observações

- Para resetar o banco de dados, execute:
```bash
docker-compose down -v
```

- Para aplicar uma nova migration:
```bash
dotnet ef migrations add NomeDaMigration -p ERPPlus.Infrastructure -s ERPPlus.WebAPI -o Persistence/Migrations
```

---

