# 🧱 Base API (.NET 8)

    API RESTful construída com ASP.NET Core 8, aplicando os princípios da Clean Architecture, MediatR, CQRS, autenticação via JWT e validação com FluentValidation. Documentação via Swagger.

---

## 🚀 Tecnologias Utilizadas

    - ✅ ASP.NET Core 8
    - ✅ Entity Framework Core
    - ✅ PostgreSQL
    - ✅ MediatR + CQRS
    - ✅ FluentValidation
    - ✅ JWT Bearer Authentication
    - ✅ Swagger (Swashbuckle)

---

## 📁 Estrutura do Projeto

    /src 
        ├── Application/ # Camada de aplicação (handlers, comandos, queries, DTOs) 
        ├── Domain/ # Entidades, agregados e interfaces do domínio 
        ├── Infrastructure/ # Implementações de repositórios, banco de dados e serviços externos 
        └── Presentation/ # API (controllers, filters, middlewares, autenticação) Base.sln # Solução principal do projeto

---

## ✅ Pré-requisitos

    - [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
    - [PostgreSQL](https://www.postgresql.org/download/)
    - Visual Studio ou VS Code
    - Ferramenta de HTTP (Postman, Insomnia ou similar)

---

## ⚙️ Configuração do Projeto

### 1. Clonar o repositório

    bash
    git clone https://github.com/gmmoraesbr/api-net-core-8.0.git
    cd api-net-core-8.0

### 2. Configurar appsettings.json
    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "JwtSettings": {
            "Secret": "chave-super-segura-para-testes-1234567890123456",
            "Issuer": "suaapi.com",
            "Audience": "suaapi.com",
            "ExpirationMinutes": 60
        },
        "Correlation": {
            "ExpectedId": "d9a5a5f2-6a93-4a71-9db9-f74c78e32ec5"
        }
    }

### 3. Configurar appsettings.Development.json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Host=localhost;Port=5432;Database=SEU_BANCO;Username=SEU_USUARIO;Password=SUA_SENHA"
        }
    }

🛠️ Migrations & Banco de Dados
Para aplicar as migrations e criar o schema no PostgreSQL:

    cd src/Presentation/Base.Api
    dotnet ef database update

▶️ Executando a Aplicação
    
    dotnet run --project src/Presentation/Base.Api

Acesse o Swagger em:
    👉 http://localhost:5000/swagger

🔐 Autenticação e Segurança

    Para autenticar, utilize o endpoint:
    POST /api/auth/login

    O token JWT deve ser enviado nos endpoints protegidos via header:
    Authorization: Bearer {token}

    Todas as requisições exigem o cabeçalho obrigatório:
    X-Correlation-ID: {uuid}

📌 Endpoints Principais
    Método	Rota	Descrição
    POST	/api/auth/register	Registro de novo usuário
    POST	/api/auth/login	Login e retorno do token
    GET	/api/products	Listar produtos (paginado)
    GET	/api/products/{id}	Buscar produto por ID
    POST	/api/products	Criar novo produto
    PUT	/api/products/{id}	Atualizar produto existente
    DELETE	/api/products/{id}	Remover produto

🧪 Testes
✅ Testes de integração para autenticação implementados.
    Para executar os testes:
        dotnet test

📬 Suporte
    Dúvidas, sugestões ou problemas?
    Abra uma (https://github.com/gmmoraesbr/api-net-core-8.0/issues) ou entre em contato. 🚀