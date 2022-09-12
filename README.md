# Cinema

[![NPM](https://img.shields.io/npm/l/react)](https://github.com/danielbgusmao/Cinema/blob/main/LICENCE)

# Sobre o projeto

 Esse projeto é composto por dois produtos o server **Cinema** e o client **CinemaClient**. O server é um produto baseado em WebApi e o client consome seus serviços.

- **Link do Server: Cinema:** https://github.com/danielbgusmao/Cinema

- **Link do Client: CinemaClient:** https://github.com/danielbgusmao/CinemaClient



**1)** Criar uma base de dados no Sql Server.

**2)** Alterar as configurações do arquivo ~\Cinema\Cinema.Service\Settings.cs com as credenciais de acordo com o servidor Sql Server desejado.

**3)** No projeto 4.1 Cinema.Infra.Data execute os comandos (sem aspas):

**3.1)** "Add-Migration Teste"

**3.2)** "Update-Database"

**4)** Executar o script para criação das funções na base de dados:

```bash

CREATE FUNCTION fn_DuracaoFilme(@filmeId UNIQUEIDENTIFIER)
RETURNS varchar(5)
AS
BEGIN
    Declare @duracao varchar(5);
    SELECT @duracao = fi.Duracao from Filme fi
		where fi.Id = @filmeId
		RETURN  @duracao
END
GO
CREATE FUNCTION fn_SugestaoDeSessoes(@dataInicio DATETIME2, @dataFim DATETIME2, 
		@filmeId UNIQUEIDENTIFIER, @salaId UNIQUEIDENTIFIER)
	RETURNS TABLE
	AS
	RETURN(
		select se.Id,
		se.DataInicio,
		se.DataFim,
		fi.Duracao
		from Sessao se
		INNER JOIN Filme fi on se.FilmeId = fi.Id

	where SalaId = @salaId 
		AND
		((se.DataInicio >= @dataInicio
		AND se.DataInicio <= @dataFim) OR
		(se.DataFim >= @dataInicio
		AND se.DataFim <= @dataFim)))
	
```

**5)** Execute o script de criação de um usuário para autenticar na aplicação CinemaClient:

```bash

INSERT INTO Usuario (Id, Nome, Email, Senha) VALUES (NEWID(),'Seu nome','seuemail@gmail.com','123456');

```
**6)** As salas serão automaticamente criadas pelo serviço "VerificarEPopularTabela" ao acessar o menu "Sala" ou "Sessão".


**7)** Depois de seguir os passos anteriores execute no seu Visual Studio a aplicação Cinema e posteriormente a CinemaClient.

# Autor

Daniel Bringel Gusmão.

https://www.linkedin.com/in/daniel-gusmão-a2b2b011/

