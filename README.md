# Pzpp

**Opis projektu**

PhoneNest to aplikacja webowa stworzona w technologii ASP.NET Core, umożliwiająca użytkownikom zarządzanie własną książką kontaktów. Użytkownicy mogą dodawać, edytować oraz usuwać kontakty, wprowadzając takie informacje jak imię, nazwisko, adres e-mail, numer telefonu. 


**Wymagania funkcjonalne**


Zarządzanie kontaktami:

Dodawanie kontaktów: Użytkownik może dodać nowy kontakt, podając imię, nazwisko, adres e-mail, numer telefonu.

Edycja kontaktów: Możliwość edytowania danych istniejącego kontaktu.

Usuwanie kontaktów: Opcja usunięcia kontaktu z listy.


Przeglądanie kontaktów:

Lista kontaktów: Wyświetlanie wszystkich kontaktów z podstawowymi informacjami.

Szczegóły kontaktu: Możliwość wyświetlenia pełnych informacji o wybranym kontakcie.


*Wymagania niefunkcjonalne*


Technologie:

Platforma: Aplikacja została stworzona w oparciu o .NET 9.

Język programowania: C#.

Baza danych: Entity Framework Core do zarządzania danymi.

Bezpieczeństwo: ASP.NET Core Identity do zarządzania użytkownikami i uwierzytelniania.


Wydajność:

Szybkość działania: Aplikacja powinna działać płynnie przy obsłudze dużej liczby kontaktów.

Skalowalność: Możliwość rozszerzenia funkcjonalności w przyszłości.


Interfejs użytkownika:

Responsywność: Aplikacja dostosowuje się do różnych rozmiarów ekranów, zapewniając wygodne użytkowanie na urządzeniach mobilnych i desktopowych.

Intuicyjność: Prosty i przejrzysty interfejs umożliwiający łatwe zarządzanie kontaktami.


Testowanie:

Testy jednostkowe: Zaimplementowane testy jednostkowe zapewniające poprawność działania poszczególnych komponentów aplikacji.

Testy integracyjne: Sprawdzenie współpracy między różnymi modułami aplikacji.


*Potencjalne ryzyka*

Zarządzanie danymi:

Bezpieczeństwo danych: Ryzyko nieautoryzowanego dostępu do danych kontaktowych użytkowników.

Integralność danych: Możliwość wystąpienia błędów prowadzących do utraty lub duplikacji danych.

Wydajność:

Obciążenie serwera: Przy dużej liczbie użytkowników aplikacja może wymagać optymalizacji pod kątem wydajności.

Kompatybilność:

Różne przeglądarki: Aplikacja powinna być testowana pod kątem kompatybilności z różnymi przeglądarkami internetowymi.