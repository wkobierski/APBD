# Uczelniana Wypożyczalnia Sprzętu

Aplikacja konsolowa w C# (.NET 9) obsługująca uczelnianą wypożyczalnię sprzętu. System pozwala na rejestrowanie sprzętu, wypożyczanie go użytkownikom, zwroty z naliczaniem kar oraz generowanie raportów.

## Uruchomienie

```bash
cd cwiczenia2
dotnet run
```

Aplikacja uruchamia interaktywne menu konsolowe z 10 dostępnymi operacjami. Dane są automatycznie zapisywane do pliku `data.json` przy wyjściu i wczytywane przy starcie.

## Struktura projektu

```
cwiczenia2/
  Application/       - Warstwa prezentacji (konsola)
    Program.cs        - Punkt wejścia
    App.cs            - Główna pętla aplikacji, menu, nawigacja
    AppActions.cs     - Obsługa interakcji z użytkownikiem dla każdej operacji
    Utils.cs          - Narzędzia do obsługi wejścia (ReadInt, ToTitleCase, ConfirmAction)
  Model/              - Warstwa domenowa (encje)
    Devices/          - Device (abstrakcyjna), Camera, Laptop, Projector, AvailabilityStatus
    Users/            - User, UserType
    Rentals/          - Rental
  Logic/              - Warstwa logiki biznesowej (serwisy)
    DeviceService.cs  - Operacje na sprzęcie
    UserService.cs    - Operacje na użytkownikach
    RentalService.cs  - Wypożyczenia, zwroty, naliczanie kar
  Data/               - Warstwa persystencji
    DataStore.cs      - Zapis/odczyt danych do JSON
    *Data.cs          - Klasy DTO do serializacji
  Constants.cs        - Stałe biznesowe i konfiguracyjne
```

## Decyzje projektowe

### Podział na warstwy

Projekt jest podzielony na 4 warstwy z jasno określonymi odpowiedzialnościami:

- **Application** — odpowiada wyłącznie za interakcję z użytkownikiem: wyświetlanie komunikatów, zbieranie danych wejściowych, walidację formatu. Nie zawiera logiki biznesowej.
- **Model** — czyste encje domenowe (Device, User, Rental) z właściwościami i minimalnym zachowaniem (ToString). Nie wiedzą nic o konsoli ani o persystencji.
- **Logic** — serwisy (DeviceService, UserService, RentalService) zawierają logikę biznesową: tworzenie obiektów, walidację reguł (limity wypożyczeń, dostępność sprzętu), naliczanie kar.
- **Data** — odpowiada za serializację/deserializację do JSON. Klasy DTO (UserData, DeviceData itd.) są oddzielone od encji domenowych, co pozwala na niezależną zmianę formatu zapisu.

Taki podział sprawia, że zmiana w jednej warstwie nie wymaga zmian w pozostałych. Na przykład zmiana sposobu zapisu danych (z JSON na bazę danych) wymaga modyfikacji tylko katalogu `Data/`, bez dotykania logiki biznesowej ani interfejsu.

### Kohezja

Każda klasa ma jedną, wyraźną odpowiedzialność:

- `App.cs` — tylko pętla główna i nawigacja między akcjami
- `AppActions.cs` — zbieranie inputu i wyświetlanie wyników (kontroler)
- `RentalService.cs` — cała logika wypożyczeń i zwrotów
- `Utils.cs` — generyczne narzędzia do obsługi wejścia (ReadInt, ReadCancellableString, DisplayList)
- `Constants.cs` — wszystkie stałe biznesowe w jednym miejscu

Logika biznesowa nie jest rozproszona — reguły takie jak limity wypożyczeń czy naliczanie kar znajdują się wyłącznie w `RentalService`, a ich parametry (14 dni, 5 PLN/dzień, limit 2/5) w `Constants`.

### Coupling (sprzężenie)

Warstwy komunikują się jednokierunkowo: Application -> Logic -> Model. Warstwa prezentacji nie ma dostępu do wewnętrznych list modelu — korzysta z serwisów. Serwisy operują na encjach domenowych, ale nie wiedzą o konsoli.

Klasy DTO w `Data/` są oddzielone od encji domenowych, dzięki czemu format serializacji nie wpływa na model domeny.

### Dziedziczenie

Dziedziczenie jest użyte tam, gdzie wynika z domeny:

- `Device` (klasa abstrakcyjna) -> `Camera`, `Laptop`, `Projector` — wspólne pola (Id, Name, AvailabilityStatus) w klasie bazowej, specyficzne pola w podklasach
- `UserType` i `AvailabilityStatus` jako enumy — proste typy bez potrzeby hierarchii klas

Nie tworzę sztucznych hierarchii dla UserType (Student/Employee) — enum z warunkiem w serwisie jest prostszy i wystarczający.

### Obsługa błędów

Naruszenia reguł biznesowych (przekroczenie limitu wypożyczeń, wypożyczenie niedostępnego sprzętu) są sygnalizowane przez `InvalidOperationException` rzucany w warstwie serwisowej. Warstwa prezentacji łapie wyjątek i wyświetla komunikat użytkownikowi.

## Reguły biznesowe

Wszystkie reguły są zdefiniowane w `Constants.cs`:

| Reguła | Wartość |
|--------|---------|
| Darmowy okres wypożyczenia | 14 dni |
| Kara za każdy dzień opóźnienia | 5 PLN |
| Maks. aktywnych wypożyczeń (student) | 2 |
| Maks. aktywnych wypożyczeń (pracownik) | 5 |

Zmiana dowolnej reguły wymaga edycji jednej linii w jednym pliku.

## Funkcjonalności

1. Dodanie użytkownika
2. Dodanie sprzętu (Camera/Laptop/Projector)
3. Lista całego sprzętu ze statusami
4. Lista sprzętu dostępnego do wypożyczenia
5. Wypożyczenie sprzętu użytkownikowi
6. Zwrot sprzętu z naliczeniem kary
7. Oznaczenie sprzętu jako niedostępnego
8. Aktywne wypożyczenia użytkownika
9. Przeterminowane wypożyczenia
10. Raport podsumowujący

Dodatkowo: zapis/odczyt danych do JSON, interaktywne menu konsolowe, możliwość anulowania operacji (komenda "cancel").
