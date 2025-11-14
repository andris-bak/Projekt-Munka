# Funkcionális specifikáció 

**Projekt neve:** Click the Dog (CTD)  
**Dátum:** 2025.10.15.  
**Készítette:** M.A.K.E Kft  
**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil 

## 1. Bevezetés 

Ez a dokumentum a Click the Dog játékunk funkcionális működését írja le. 
Célja, hogy pontosan meghatározza, a játék milyen funkciókat biztosít, hogyan kezeli a játékosokat. 

## 2. Rendszeráttekintés 

A játék telepítés után egyből elérhető. A játékosok amint elindítják a játékot és betöltik azonnal képesek belecsöppenni a videó játék világába. 

## 3. Felhasználói felületek 

### 3.1 Főmenü 

**Gomb:** Start, Load, Options, Quit. 

**Műveletek:** 
* **Start:** elindítja a játékot és átdob egy másik jelenetre, ahol a játék folyni fog 
* **Load:** betölti a legújabb mentésünket. 
* **Options:** egy beállítások menüt dob fel, ahol személyre lehet szabni egy-két dolgot. 
* **Quit:** kilép a játékból 

### 3.2 Játékfelület 

**Pálya:** Ellenség, Háttér, Mentés, Mentés törlése UI elemek. 

**Műveletek:** 
* **Kattintás az ellenségre** a karakterünk támad, az ellenség élete csökken. 
* **Mentés gombra kattintás** elmenti a játékot. 
* **Mentés törlése** törli a mentésünkét és így lényegében újra kezdhetjük a játékot a nulláról. 
* **Nagy X gombra kattintás** bezárja a játékot. 

**UI elemek:** szint, pénz, pontok, az ellenségek élete. 
Ezek mind akkor változnak, ha megtámadjuk az ellenségeket vagy legyőzzük őket 
Illetve, ha egy pályán elérünk egy adott szintet, utána a játék betölti a következő pályát, melyeken más és más fajta ellenségek lesznek. 

## 4. Funkcionális folyamatok 

### 4.1 Támadás 
* A játékos rákattint az ellenségre. 
* A karakter lejátsza a támadási animációját. 
* Az ellenségnek lemegy az élete egy adott értékkel. 
* A karakter visszakerül az idle animációjába. 

### 4.2 Az ellenség legyőzve 
* A játékos levitte az ellenség Életét nullára vagy az alá. 
* A játék automatikusan betölt egy random ellenséget. 
* A játék folytatódik. 

### 4.3 Mozgás 
* A játékos képes az E és Q billentyűkkel mozogni balra és jobbra. 
* Ezzel bővítjük tovább a harcrendszert. 

## 5. Rendszerkövetelmények 

* Nem kell hozzá erős gép, kicsi igényű a játék 
* Képernyőméret: mobil és asztali támogatás. 
* Backend: C# és Godot. 
* Frontend: Aseprite 

## 6. Jövőbeli funkciók 

* Az ellenségek képesek legyenek levédeni a támadásainkat. 
* Több karakter. 
* Egyéb QOL változtatások a legjobb felhasználói élmény érdekében.
