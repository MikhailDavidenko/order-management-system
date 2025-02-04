# OrderManagementSystem

## WEB

При первом запуске будет создана запись в бд пользователя с ролью "manager". Далее для входа используется логин и пароль "manager@local.ru" и "Qwerty123!@#".

Для проверки работы необходимо запустить бэкенд и фронтэнд приложения. Ниже будут приведены команды для запуска.

### Backend

Из корневого каталога. Будет доступен на http://localhost:5026/swagger/index.html
``` bash
dotnet run --project .\src\Web
```

### Frontend

Из корневого каталога. Будет доступен на http://localhost:5173/login
``` bash
cd frontend | npm install | npm run dev
```

## База данных

В примере используется PostgreSQL. Для корректной работы необходимо в файле конфигурации добавить строку подключения к базе данных.
Для примера добавил возможную строку подключения.

### Migration

При каждом запуске будет выполняться миграция недостающих изменений в бд.

Для создания миграции используется команда
``` bash
cd src | dotnet ef migrations add Init --startup-project Web --context OrderManagementSystem.Data.Common.AppDbContext --project Data
```

Для применения миграций вручную используется команда
``` bash
cd Web | dotnet ef database update
```

## Тесты

Для запуска тестов из корневой директории проекта:
```shell
dotnet test
```