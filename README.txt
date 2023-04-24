# ElectronicShopStock-repository
Simulation of simple stock of electronic shop

OPIS APLIKACJI
----------------------------------

Aplikacja własnego autorstwa. Z założenia implementuje konsolowe menu, żeby symulować duży magazyn sklepu z urządzeniami elektronicznymi.

Aby jej używać trzeba zrobić dwie rzeczy:

1. W klasie FileAddressesManager.cs (ElectronicShopStoreApp\3_Other\FileAddressesExtension\FileAddressesManager.cs) należy ustawić pełny, lokalny (z danego komputera) adres do folderu Files (ElectronicShopStoreApp\1_DataAccess\Files) w zmiennej 'folderAddress_Files'
2. W pliku database_address.txt (Files\database_address.txt) jako Data Source podać jakiś serwer SQL, na którym bazę danych utworzymy

Oprócz tego przy pierwszym uruchomieniu można odkomentować funkcję SaveStandardDevicesToSqlDatabase() w App.Run(), żeby zapisać do bazy danych przykładowe urządzenia (zgodnie z załączonym plikiem PDF: trzy firmy produkujące laptopy, PC i smartphony mające jakieś tam parametry jak cena, czy ocena w internecie).

Głównia logika aplikacji zawarta jest w App.Run(), która wywołuje metody z wstrzykniętej klasy LogicAndCommunicationProvider.

Operuje ona na modelach urządzeń elektronicznych, których najważniejszym parametrem jest ilość urządzeń danego modelu w magazynie (InStock). Można tą ilość zmieniać jak również prosto takowe filtrować np. po innych parametrach. Kiedy w magazynie pojawi się nowe urządzenie (typ urządzenia też jest parametrem, to nie musi być np. Laptop), aplikacja oferuje możliwość dodania jego modelu.

Zawiera także metody LINQ oraz proste walidacje.
