# MotorcycleFlow

Mottu Backend Challenge
API para gerenciamento de aluguel de motos e entregadores desenvolvida em .NET 8.

## Status do Projeto
Funcionalidades Principais: Sistema de motos e locações implementado

## Integrações: RabbitMQ configurado para eventos

## Arquitetura: Padrões CQRS e Clean Architecture aplicados

## Tecnologias Utilizadas
Backend: .NET 8, C# 11

## Arquitetura
Clean Architecture, CQRS, MediatR

## Banco de Dados
PostgreSQL com Entity Framework Core

## Mensageria
RabbitMQ para comunicação assíncrona

## Padrões
Repository Pattern, Dependency Injection

## Funcionalidades Implementadas:
Módulo de Motos
Cadastro de motos com validação de placa única
Consulta com filtro por placa
Atualização de informações
Exclusão condicional (sem histórico de locações)
Eventos RabbitMQ para motos cadastradas

## Módulo de Locações
Criação de locações com planos predefinidos
Cálculo automático de custos
Sistema de devolução com cálculo de multas
Validação de entregadores habilitados

## Event Driven Architecture
Publicação de eventos via RabbitMQ para motos cadastradas
Consumer para processamento de motos do ano 2024

## Logs estruturados para consulta futura
