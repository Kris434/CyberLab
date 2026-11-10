# CyberLab

CyberLab to interaktywna aplikacja internetowa o charakterze edukacyjnym, której celem jest demonstracja wybranych podatności bezpieczeństwa aplikacji webowych oraz sposobów ich eliminowania.

Platforma umożliwia przeprowadzanie kontrolowanych symulacji ataków, analizowanie ich skutków oraz porównywanie działania aplikacji w wersji podatnej i zabezpieczonej.

## Scenariusze bezpieczeństwa

Projekt zakłada implementację trzech scenariuszy:

- SQL Injection (SQLi) – demonstracja manipulacji zapytaniami SQL oraz zabezpieczeń wykorzystujących zapytania parametryzowane.
- Cross-Site Scripting (XSS) – demonstracja wstrzykiwania kodu JavaScript i metod bezpiecznego przetwarzania danych użytkownika.
- Insecure Direct Object Reference (IDOR) – demonstracja nieautoryzowanego dostępu do zasobów oraz mechanizmów kontroli uprawnień.

## Technologie

- Backend: ASP.NET Core 10 (C#)
- Frontend: React + TypeScript
- Baza danych: PostgreSQL + Entity Framework Core
- Konteneryzacja: Docker + Docker Compose

## Założenia projektu

CyberLab jest projektem akademickim realizowanym przez dwuosobowy zespół w ramach przedmiotu Cyberbezpieczeństwo.

Aplikacja będzie uruchamiana lokalnie w środowisku Docker, z wykorzystaniem fikcyjnych danych testowych. Wszystkie symulacje będą wykonywane w kontrolowanym środowisku, bez udostępniania podatnych mechanizmów w publicznej sieci.

Status projektu: W trakcie realizacji.
