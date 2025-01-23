# OrderManagementSystem

## Database

### Migration

Для создания миграции используется команда
``` bash
cd src | dotnet ef migrations add Init --startup-project Web --context OrderManagementSystem.Data.Common.AppDbContext --project Data
```

Для применения миграций используется команда
``` bash
cd Web | dotnet ef database update
```