# EclipseWorks

# Documento Técnico - Projeto EclipseWorks

## Visão Geral do Projeto

O **EclipseWorks** é um sistema desenvolvido utilizando tecnologias modernas como **ASP.NET Core** e **MongoDb**. 
O banco de dados utilizado pode ser configurado e executado em um container utilizando **Docker Compose**.

Este documento descreve os passos necessários para configurar, rodar e entender esse projeto.

---

## Pré-Requisitos

Antes de iniciar, certifique-se de que as seguintes ferramentas estejam instaladas em sua máquina:

1. [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
2. [Docker](https://www.docker.com/products/docker-desktop/)
3. [Docker Compose](https://docs.docker.com/compose/)

---

## Estrutura do Projeto

O projeto possui a seguinte estrutura de diretórios:

```
/EclipseWorks
│
├── .gitignore                    # Arquivo de exclusão do git
├── docker-compose.yaml           # Configuração Docker Compose
├── Dockerfile                    # Configuração básica do Docker
├── README.md                     # Documentação principal do projeto
├── EclipseWorks.TaskManagement.sln # Arquivo de solução do projeto
│
├── /src
│   ├── /EclipseWorks.TaskManagement.Application  # Camada de aplicação (handlers, requests, responses)
│   ├── /EclipseWorks.TaskManagement.Infrastructure # Infraestrutura e configuração da aplicação
│   ├── /EclipseWorks.TaskManagement.Models       # Modelos e contratos de dados
│   └── /EclipseWorks.TaskManagement.WebApi       # API principal (controllers e configuração)
│
├── /tests
│   └── /EclipseWorks.TaskManagement.Tests        # Testes
│

```
---

## Configurando o Ambiente

Para configurar o ambiente, siga os passos abaixo:

1. Clone o repositório para sua máquina local:
   ```bash
   git clone https://github.com/mikerozendo/EclipseWorks.TaskManagement.git
   cd EclipseWorks
   ```
2. Certifique-se de que o **Docker Desktop** está em execução e com o suporte ao Docker Compose habilitado.
3. No diretório raiz, execute o seguinte comando para iniciar os containers:
   ```bash
   docker-compose up -d
   ```
4. Verifique se a API está disponível acessando `http://localhost:8080` em seu navegador.

---

## Executando os Testes

Para garantir que todas as funcionalidades estão funcionando corretamente, execute os testes automatizados:

1. No diretório raiz, vá para o projeto de testes:
   ```bash
   cd src/EclipseWorks.Tests
   ```
2. Execute os testes com o seguinte comando:
   ```bash
   dotnet test
   ```

Os resultados dos testes serão exibidos no terminal.
---

## Contribuindo para o Projeto

Se você deseja contribuir com o **EclipseWorks**, siga os passos abaixo:

1. Crie um fork do repositório para sua conta pessoal.
2. Faça suas alterações em uma nova branch, utilizando uma convenção de nomes como `feature/<NOME_DA_FEATURE>` ou
   `bugfix/<DESCRICAO_DO_FIX>`.
3. Certifique-se de executar todos os testes antes de enviar sua contribuição.
4. Envie um pull request detalhando as alterações realizadas.

---

## Fale Conosco

Caso tenha dúvidas ou encontre algum problema, entre em contato através de:

- Email: mikerozendo@gmail.com
- GitHub Issues: [EclipseWorks Issues Page](https://github.com/mikerozendo/EclipseWorks.TaskManagement/issues)

---

### Decisões Técnicas

- **Uso do ASP.NET Core**: Escolhido por sua alta performance e integração nativa com outras ferramentas do ecossistema
  .NET.
- **MongoDB como Banco de Dados**: Decidi pelo MongoDb devido à sua flexibilidade e também pelo fato do teste pedir a criação de dados do tipo históricos.
- **Containerização com Docker**: Utilizado para consistência no ambiente de desenvolvimento e implantação.
- **Docker Compose**: Utilizado pela facilidade em lidar com mais de um container e conseguir trabalhar com a Api e o Db.
- [MediatR](https://github.com/jbogard/MediatR): Utilizado para facilitar a implementação do padrão Mediator, optei pelo mesmo também pela sua facilidade de construir uma aplicação.

### Dívidas Técnicas

- **Monitoramento da Aplicação**: Falta uma solução robusta para monitoramento em produção, como
  integração com ferramentas de observabilidade.

- **Id's de usuário**: Precisa refinar melhor qual a solução de Auth para que seja feito um levantamento de como a aplicação iria saber o Id de um usuário, as partes que dependem de um user_id foram marcadas como TD

- **Analytics**: No teste foi solicitado que a consulta que retornaria as estatísticas de tasks fechadas tivesse o acesso liberado sómente ao gerente, porém ficou marcado como TD também
  ![TDs](images/TDs.png)