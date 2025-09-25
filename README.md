# NotaSys

NotaSys é uma aplicação web desenvolvida em ASP.NET Core MVC para o gerenciamento e processamento de arquivos XML de Notas Fiscais Eletrônicas (NF-e). O sistema permite o upload de múltiplos arquivos XML, extrai informações essenciais e as armazena em um banco de dados para fácil consulta e gerenciamento.
Também conta com uma validação sequencial de nota, para que em trabalho de massa de notas seja validada a sequência afim de evitar eventuais problemas

## 📄 Funcionalidades

*   **Upload de NF-e**: Faça o upload de um ou mais arquivos XML de NF-e de uma só vez através de uma interface simples.
*   **Extração de Dados**: O sistema processa os arquivos XML para extrair dados importantes como:
    *   Chave de Acesso (ID)
    *   CNPJ do emissor
    *   Número e Série da nota
    *   Data de emissão
    *   Unidade Federativa
*   **Armazenamento**: As informações extraídas, juntamente com o conteúdo XML original, são salvas em um banco de dados SQL Server.
*   **Interface Web**: Uma interface web intuitiva para interagir com as funcionalidades do sistema.

## 🛠️ Tecnologias Utilizadas

*   **Backend**: .NET 7, ASP.NET Core MVC, Entity Framework Core 7
*   **Banco de Dados**: SQL Server
*   **Frontend**: HTML, CSS, JavaScript, Bootstrap
*   **Linguagem**: C#

## 📂 Estrutura do Projeto

O projeto segue a estrutura padrão de uma aplicação ASP.NET Core MVC:

-   `Controllers/`: Contém os controladores que gerenciam as requisições do usuário.
    -   [`HomeController.cs`](Controllers/HomeController.cs): Controlador para as páginas iniciais.
    -   [`FerramentasController.cs`](Controllers/FerramentasController.cs): Controlador para as funcionalidades de upload e processamento das notas.
-   `Models/`: Define as classes de modelo de dados e enums.
    -   [`Arquivo.cs`](Models/Arquivo.cs): Modelo que representa os dados de uma nota fiscal.
    -   [`Enums/UfEnum.cs`](Models/Enums/UnidadeFederativaEnum.cs): Enum para as Unidades Federativas do Brasil.
-   `Views/`: Contém os arquivos Razor (`.cshtml`) que compõem a interface do usuário.
-   `Context/`: Contém a classe de contexto do Entity Framework.
    -   [`NotaSysContext.cs`](Context/NotaSysContext.cs): Contexto do banco de dados que gerencia a entidade `Arquivo`.
-   `Migrations/`: Arquivos de migração do Entity Framework Core para o versionamento do banco de dados.
-   `wwwroot/`: Diretório para arquivos estáticos como CSS, JavaScript e bibliotecas de terceiros.
-   [`Program.cs`](Program.cs): Ponto de entrada da aplicação, onde os serviços e o pipeline de requisições são configurados.
-   `appsettings.json`: Arquivo de configuração da aplicação, incluindo a string de conexão com o banco de dados.

## 🚀 Como Executar o Projeto

1.  **Clone o repositório:**
    ```sh
    git clone https://github.com/LGretti/NotaSys.git
    cd NotaSys
    ```

2.  **Configure a Conexão com o Banco de Dados:**
    Abra o arquivo `appsettings.json` e modifique a string de conexão `Main` para apontar para a sua instância do SQL Server.
    ````json
    // filepath: appsettings.json
    // ...código existente...
    "ConnectionStrings": {
        "Main": "Server=SEU_SERVIDOR;Database=NotaSysDB;Trusted_Connection=True;TrustServerCertificate=true;"
    },
    // ...código existente...
    ````

3.  **Aplique as Migrações:**
    Execute o comando a seguir no terminal, na raiz do projeto, para criar o banco de dados e a tabela `Arquivos`.
    ```sh
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    ```sh
    dotnet run
    ```

## 📋 Como Usar

1.  Na página inicial, navegue até a seção **Ferramentas** no menu superior.
2.  Clique no link **Upload dos arquivos**.
3.  Na página de upload, clique no botão para selecionar arquivos e escolha o diretório que contém os arquivos XML das notas fiscais.
4.  Clique no botão **Upload** para iniciar o processamento.
5.  Após o processamento, os dados das notas estarão salvos no banco de dados.