RECENZEX

1. WYMAGANIA

* Visual Studio 2026
* .NET 10.0
* SQL Server Express
* (opcjonalnie) SQL Server Management Studio

---

2. KONFIGURACJA BAZY DANYCH

Serwer: localhost\SQLEXPRESS
Baza danych: Recenzex
Connection String: "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Recenzex;Trusted_Connection=True;TrustServerCertificate=True;"

---

3. ROLE

* Admin – pe³ny dostêp do aplikacji
* User – przegl¹danie filmów, dodawanie recenzji i komentarzy

Konto administratora:
Login: admin@recenzex.pl
Has³o: Admin!12345

---

4. FUNKCJONALNOŒCI

U¿ytkownik niezalogowany mo¿e:

	* przegl¹daæ listê filmów,
	* przechodziæ do szczegó³ów filmu,
	* przegl¹daæ recenzje i komentarze innych u¿ytkowników.

U¿ytkownik zalogowany mo¿e:
	* dodawaæ recenzje i komentarze do filmów,
	* edytowaæ i usuwaæ swoje recenzje i komentarze,

---

API dosepne pod /api/films
