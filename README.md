RECENZEX

1. WYMAGANIA

* Visual Studio 2026
* .NET 10.0
* SQL Server Express
* (opcjonalnie) SQL Server Management Studio

---

2. KONFIGURACJA BAZY DANYCH

* Serwer: localhost\SQLEXPRESS
* Baza danych: Recenzex
* Connection String: "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Recenzex;Trusted_Connection=True;TrustServerCertificate=True;"
  
---

3. ROLE

* Admin – pełny dostęp do aplikacji
* User – przeglądanie filmów, dodawanie recenzji i komentarzy

Konto administratora:
Login: admin@recenzex.pl
Hasło: Admin!12345

---

4. FUNKCJONALNOŚCI

Użytkownik niezalogowany może:
	* przeglądać listę filmów,
	* przechodzić do szczegółów filmu,
	* przeglądać recenzje i komentarze innych użytkowników.

Użytkownik zalogowany może:
	* dodawać recenzje i komentarze do filmów,
	* edytować i usuwać swoje recenzje i komentarze,

---

API dosepne pod /api/films
