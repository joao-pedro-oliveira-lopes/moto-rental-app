Moto Rental App - Desafio de Desenvolvimento

Visão Geral

Este projeto é um sistema para gestão de aluguel de motos, seguindo práticas avançadas como Domain-Driven Design (DDD), princípios SOLID, e Arquitetura Hexagonal. Foi projetado para ser modular, fácil de manter e escalável, com foco em resolver os requisitos apresentados no desafio, incluindo a criação de testes unitários para demonstrar habilidades de teste.

Casos de Uso Implementados
	1.	Locação de motos por entregadores habilitados
	•	Implementado no método RentAsync em DeliveryPersonService e exposto no controller DeliveryPersonController.
	•	Validações de CNH, data de início e planos de locação estão centralizadas no serviço.
	2.	Informar data de devolução e cálculo do valor total
	•	Implementado no método CalculateReturnAmountAsync no serviço DeliveryPersonService.
	•	Regras de multa por devolução antecipada e diária adicional foram incluídas conforme especificado.
	3.	Validação de CNPJ
	•	Método CheckCnpjAsync no DeliveryPersonService validado com teste unitário (CheckCnpj_ReturnsSuccess_ForValidCnpj).

Como os Casos de Uso Foram Mapeados
	•	Domínio: Os casos de uso estão diretamente ligados às entidades Rent e DeliveryPerson, encapsulando lógica de negócios.
	•	Aplicação: A camada de aplicação realiza a ponte entre o domínio e a API.
	•	Interface: A exposição dos métodos se dá através dos controllers da API.

Arquitetura e Design

Domain-Driven Design (DDD)

O projeto foi estruturado com base no DDD, separando:
	•	Domínio: Contém entidades, enums e regras de negócios (Rent, DeliveryPerson).
	•	Aplicação: Define os serviços que orquestram operações de negócios (DeliveryPersonService, IRentRepository).
	•	Infraestrutura: Implementa os repositórios concretos e integrações com banco de dados (via Entity Framework e PostgreSQL).

Arquitetura Hexagonal
	•	Portas e Adaptadores: Os controllers são portas para comunicação externa, enquanto os repositórios são adaptadores para persistência.
	•	Independência: Toda lógica de negócio é desacoplada da infraestrutura.

Como Rodar o Projeto

Pré-requisitos
	•	Docker instalado.
	•	.NET SDK 8.0 instalado.
	•	WSL2 configurado para o ambiente Linux (caso use Windows).

Passos para Executar
	1.	Clone o repositório:
    git clone https://github.com/joao-pedro-oliveira-lopes/moto-rental-app.git
    cd moto-rental-app


	2.	Execute os containers com Docker:
    docker-compose up


	3.	Atualize o banco de dados:
    dotnet ef database update


	4.	Execute o projeto:
    dotnet run --project src/MotoRentalApp.API


	5.	Acesse a aplicação em: http://localhost:5000.

Testes Unitários

Os testes foram criados na pasta tests/MotoRentalApp.Tests.

Teste Adicionado

CheckCnpj_ReturnsSuccess_ForValidCnpj
	•	Verifica a funcionalidade do método CheckCnpj.
	•	Demonstra a capacidade de escrever testes unitários utilizando xUnit e Moq.

Como Rodar os Testes

Execute o seguinte comando para rodar os testes:

dotnet test

Conclusão

Este projeto demonstra a implementação de uma solução robusta para o desafio, com arquitetura moderna e testes bem definidos. Ele cobre os requisitos propostos, valida a aplicação de conceitos avançados e inclui exemplos práticos para destacar a habilidade de desenvolver testes unitários.