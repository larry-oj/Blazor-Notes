# Blazor-Notes
PostgreSQL DB table:
```
CREATE TABLE notes (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY NOT NULL,
    title TEXT NOT NULL,
    content TEXT NOT NULL
);
```
