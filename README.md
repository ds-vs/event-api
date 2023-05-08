# Event API

## Цель
Разработать Backend-сервис, который позволяет автоматизировать процесс отслеживания актуальных мероприятий в городе.

## Описание
*тут будет более подробное описание проекта*

#### Используемые технологии:
- .NET 7
- ASP.NET Core Web API
- SwaggerUI
- Entity Framework Core
- PostgreSQL
- BackgroundService
- Moq
- xUnit
#### Архитектура:
- [Clean architecture](https://learn.microsoft.com/ru-ru/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Repositories](https://medium.com/net-core/repository-pattern-implementation-in-asp-net-core-21e01c6664d7)

<details>
<summary>База данных</summary>

### Схема
![Тут должна быть схема базы данных.](https://github.com/ds-vs/event-api/blob/write-readme/Schemes/%D1%81%D1%85%D0%B5%D0%BC%D0%B0_%D0%B1%D0%B0%D0%B7%D1%8B_%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85.PNG)

### Описание
#### 1. Roles - таблица для хранения информации о ролях.
- role_id - уникальный идентификатор роли (uuid);
- name - наименование роли (text);
- description - описание роли (text);
#### 2. Accounts - таблица для хранения информации об учетных записях.
- account_id - уникальный идентификатор акаунта (uuid);
- role_id - идентификатор роли, который связан с учетной записью (связь один ко многим с таблицей Roles);
- login - логин пользователя (text);
- email - электронная почта (text);
- password_hash - хэш пароля (text);
- refresh_token - токен обновление (text);
- token_created - дата и время создания токена (timestamp);
- token_expires - дата и время истечения токена (timestamp);
#### 3. Events - таблица для хранения информации о событиях.
- event_id - уникальный идентификатор события (uuid);
- title - наименование мероприятия (text);
- date - дата проведения мероприятия (timestamp);
- description - описание мероприятия (text);
- subscribers_number - число подписчиков (bigint);
- status - статус мероприятия (integer);
- account_id - идентификатор учетной записи, которая создала событие (связь один ко многим с таблицей Accounts);
#### 4. Subscriptions - таблица для хранения информации о подписках на события.
- account_id - идентификатор учетной записи, которая подписалась на событие (связь многие ко многим);
- event_id - идентификатор события, на которое подписалась учетная запись (связь многие ко многим);

</details>

## Инструкция по запуску
1. Запустить [Docker Desktop](https://www.docker.com/)
2. Склонировать репозиторий
3. Перейти в Event.Backend
4. Выполнить команду: **docker-compose up -d**
5. Перейти к документации: **https://localhost:5000/swagger/index.html**

<details>
<summary>Adminer</summary>

- localhost:8080
- Database: **PostgreSQL**
- Server: **postgres_db**
- User: **postgres**
- Password: **postgres**

</details>
