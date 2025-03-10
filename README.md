# Описание
В корне репозитория лежит скрипт создания базы данных: `create-db.sql`. Он не используется напрямую в проекте, поскольку база создаётся с помощью миграций Entity Framework.

Для удобства сделал дополнительный эндпоинт для создания `Interview`. Он присылает `InterviewId` и `Id` первого вопроса, которые нужно использовать для тестирования других эндпоинтов. Пример ответа:
```json
{
  "interviewId": "d27e3af5-c321-4b67-b7bb-c70ab77ecf00",
  "firstQuestionId": "1ff64650-26ac-4785-8336-f2eb141becb3"
}
```

# Запуск
Скачать репозиторий и из корня выполнить в терминале команду:
```sh
docker compose up
```

Проверить эндпоинты можно по ссылке:
```
http://localhost:8080/swagger/index.html
```
