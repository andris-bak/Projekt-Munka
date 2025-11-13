# Követelményspecifikáció

**Projekt neve:** Click the Dog (CTD)
**Dátum:** 2025.10.09.
**Készítette:** M.A.K.E Kft
**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil

## 1. Célunk

* A projektünk célja, hogy egy GODOT játékmotorban készített játékot adjunk ki a félév végére.
* Elsősorban egyszerű játékmenet, mely a játékban eltöltött idő után nehezebbé és komplexebbé válik.
* Továbbá stílusa pixel-art, amely reméljük, hogy minden játékosunknak tetszeni fog.

## 2. Bevezetés

Alapul a híres, de egyszerű játék menettel rendelkező játék(ok), mint a Clicker Heroes.

Önmagában nem olyan érdekfeszítő mivel javarészt csak az egerünk bal gombját használjuk arra, hogy ellenségeket győzzünk le.

Viszont, egy idő után képesek vagyunk más és más karakterekre szert tenni, kik segítenek a játék végig jázásában. Illetve képesek vagyunk őket szintezni, hogy minél többet sebezzenek. Így mi is ezt a játékmentet próbáljuk megcsinálni, de a saját és újszerű harcrendszerrel.

## 3. Érintettek és szerepkörök

* **Back-End:** Bak András Mátyás és Kovács Krisztián. A játék logikájáért és a háttérben futó programokért felelősek
* **Front-End:** Jabur Emil és Venyige Márk. A játék kinézetéért, animációiért, kisebb effektekért felelősek
* **Tesztelő:** Az egész csapat. Minden kis lépést, minden kis változtatást legyen, az back- vagy front-end együtt próbálunk tesztelni és dokumentálni.

## 4. Funkcionális követelmények

* A felhasználó képes legyen kedve szerint beállítani a hangok erősségét.
* A játék képes legyen menteni.
* A felhasználó képes legyen új karaktereket megvásárolni
* A játékban legyenek sokkal erősebb fő ellenségek
* A felhasználó képes legyen a játék a karakterei pozícióin egy gombnyomással változtatni.
* Egy kisebb történet elmesélése dialouge-boxok használatával.
* Későbbiekben egy végtelen játékmód.

## 5. Nem funkcionális követelmények

* **Használhatóság:** egyszerű, letisztult, de szórakoztató játékmenet, mely komplexebbé válik.
* **Teljesítmény:** kisméretű játék, mely futáskor nem használ sok memóriát.
* **Biztonság:** a játék offline fut, nincs bejelentkezés, nem gyűjt adatokat.
* **Platformfüggetlenség:** megpróbáljuk mindenfajta platformra kiadni, köztük mobiltelefonra is.

## 6. Rendszerkörnyezet

* **Technológia:**
    * Backend: C#, Godot
    * Frontend: Aseprite, Paint3D, Itch.io
    * Adatbázis:-------------
* **Futtatás:** telepítés után futtatható akármilyen gépről

## 7. Korlátozások

* A játék legelső verziója nem tartalmaz, feloldható karaktereket, mélyebb harcrendszert
* Eddig még csak egy pálya játszható pár ellenséggel.

## 8. Példa felhasználói történet

> Felhasználóként, avagy Játékosként szeretnénk huzamosabb játék idő után elmenteni az elért pontjainkat, szintünket, pénzünket. Vagy esetleg le akarjuk törölni, ha szeretnénk az egészet újra kezdeni

## 9. Elfogadási kritériumok

* A játék sikeresen elmenti, és a következő megnyitáskor betölti azt.
* A játék betölti a következő pályát, új ellenségekkel s más logikával.
* A játék egy elért szint után betölti a pálya végén lévő fő ellenséget

## 10. Jövőbeli bővítések

* Harcrendszer normális kidolgozása
* Többfajta karakter feloldása ás használata,
* Új pályák
* Új ellenségek, kik sebeznek, visszatöltődik az életük, támadásokat védenek le és új sebzéstípusok
* Más-más zenék
