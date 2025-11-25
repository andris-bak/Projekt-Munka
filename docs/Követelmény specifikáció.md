# 💾 Követelményspecifikáció: Click the Dog (CTD)

**Projekt neve:** Click the Dog (CTD)
**Verziószám:** 1.1 (Véglegesített)
**Dátum:** 2025.10.09.
**Készítette:** M.A.K.E Kft.
**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil

---

## 1. 📜 Revíziótörténet

| Verzió | Dátum | Készítette | Leírás |
| :--- | :--- | :--- | :--- |
| **1.1** | **2025.11.25.** | **M.A.K.E Kft.** | **Egységes számozás**, GWT formátum bevezetése és technológiai (adatbázis) pontosítás. |
| 1.0 | 2025.10.09. | M.A.K.E Kft. | A hivatalos Követelményspecifikáció kiadása, funkcionális és nem funkcionális követelmények véglegesítése. |
| 0.9 | 2025.09.28. | M.A.K.E Kft. | Első vázlat, főbb célok és mechanikák meghatározása. Belső tesztelés és hibajavítás az alapvető működésen. |
| 0.8 | 2025.09.15. | Back-End Csapat | Godot projekt inicializálása és az alapvető adatszerkezetek (pl. pénz, sebzés) felállítása. |

---

## 2. Célunk és Motiváció

A projekt célja egy **GODOT játékmotorban** készült, **inkrementális clicker játék** elkészítése. A fő cél az **egyszerű, de addiktív** játékmenet létrehozása, mely idővel **komplexebbé** válik.

* **2.1. Stílus:** **Pixel-art vizuális stílus** alkalmazása.
* **2.2. Innováció:** Egy **egyedi, újszerű harcrendszer** kidolgozása.

---

## 3. Bevezetés és Játéktípus

Alapul az inkrementális (Clicker/Idle) játékok egyszerű mechanikája. A CTD ebből indul ki: kattintással sebzés, majd automata sebzést okozó **segítő karakterek** vásárlása és szintezése.

---

## 4. Érintettek és Szerepkörök

* **4.1. Back-End:** Bak András Mátyás és Kovács Krisztián. (Játék **logikája**, **adatszerkezetek**, **mentés-betöltés**.)
* **4.2. Front-End:** Jabur Emil és Venyige Márk. (Játék **kinézete**, **UI/UX**, **animációk**.)
* **4.3. Tesztelő:** Az egész csapat. (**Közös tesztelés** és dokumentálás.)

---

## 5. Funkcionális Követelmények (F-K)

| Azonosító | Követelmény |
| :--- | :--- |
| **F-K 5.1.** | **Hangbeállítások:** A felhasználó képes legyen beállítani a **zene és hangeffektek** erősségét. |
| **F-K 5.2.** | **Mentés és Betöltés:** A játék képes legyen a játékos előrehaladását **automatikusan menteni** és betölteni. |
| **F-K 5.3.** | **Vásárlás:** A felhasználó képes legyen **új karaktereket megvásárolni** és a meglévőket **szintezni**. |
| **F-K 5.4.** | **Boss Ellenfél:** A játékban legyenek **kihívást jelentő főellenségek** (Bossok). |
| **F-K 5.5.** | **Karakterpozíció Változtatás:** A felhasználó képes legyen a **karakterek pozícióin változtatni** egy gombnyomással. |
| **F-K 5.6.** | **Történetmesélés:** Egy kisebb történet elmesélése **dialogus-ablakok** használatával. |
| **F-K 5.7.** | **Újrakezdés/Reset:** A felhasználó képes legyen az összes mentett adatot **törölni** és a játékot újra kezdeni. |
| **F-K 5.8.** | **Lokális Toplista kezelése:** A játék képes legyen a **legjobb elért pontszámokat** lokálisan tárolni és megjeleníteni. |

---

## 6. Nem Funkcionális Követelmények (NF-K)

* **6.1. Használhatóság (Usability):** A játékmenet legyen **intuitív** és áttekinthető UI/UX mellett **fokozatosan komplexé váló**.
* **6.2. Teljesítmény (Performance):** **Kisméretű** játék, mely **alacsony memória- és CPU-használatot** produkál (cél: 60 FPS).
* **6.3. Biztonság:** **Offline futás**, nincs adatgyűjtés. A mentésfájl korlátozottan védett a manipulációtól.
* **6.4. Platformfüggetlenség:** Kiadásra törekvés **Windows/Linux/Mac OS** mellett **mobiltelefonokra** (Android) is.

---

## 7. Rendszerkörnyezet

* **7.1. Technológia:**
    * **Motor/Keretrendszer:** Godot Engine (verzió: 4.x)
    * **Backend (Logika):** C#
    * **Adatbázis/Mentés:** **Lokális fájl alapú mentés** (Godot `ConfigFile` / JSON/TXT). **Szerveroldali adatbázis (pl. MySQL) jelenleg nem kerül bevezetésre** az offline jelleg miatt.
* **7.2. Futtatás:** Telepítés után futtatható akármilyen gépen.

---

## 8. Korlátozások (Constraints)

* **8.1. Harcrendszer:** A kezdeti verzió **nem tartalmaz** mély, komplex harcrendszert.
* **8.2. Karakterek:** A legelső verzióban **korlátozott számú** karakter lesz elérhető.
* **8.3. Pályák:** Kezdeti kiadásban **csak egy pálya (világ)** játszható.

---

## 9. 👤 Felhasználói Történetek (User Stories)

| Azonosító | Szerepkör | Cél | Indoklás |
| :--- | :--- | :--- | :--- |
| **US 9.1.** | Játékosként | szeretném, ha a játék **automatikusan mentené** a játékomat, | hogy ne veszítsem el az elért előrehaladásomat (progress). |
| **US 9.2.** | Játékosként | szeretnék **pénzt elkölteni** a játékon belül, | hogy **új karaktereket vásároljak** vagy a meglévőket szintezzem. |
| **US 9.3.** | Játékosként | szeretnék **változtatni a zene és a hangeffektek** erősségén, | hogy a hangerőt a saját igényeimhez tudjam igazítani. |
| **US 9.4.** | Játékosként | szeretném, ha bizonyos zónák teljesítése után **egy nagyobb, erősebb Boss-szörny** jelenne meg, | hogy izgalmasabb kihívásban legyen részem. |
| **US 9.5.** | Játékosként | szeretnék **újra kezdeni** a játékot, | hogy egy friss starttal, a korábbi tapasztalataimat felhasználva játszhassak újra. |
| **US 9.6.** | Játékosként | szeretnék egy **Toplista felületet** látni, | hogy tudjam, melyik a legjobb pontszámom. |

---

## 10. ✅ Elfogadási Kritériumok (Acceptance Criteria - GWT)

| Azonosító | Given (Adott esetben) | When (Ha) | Then (Akkor) |
| :--- | :--- | :--- | :--- |
| **AC 10.1.** | a játékos elért egy magas szintet és sok pénzt gyűjtött. | a játékos kilép a játékból és újraindítja azt. | a játék betölti a legutóbbi mentett állapotot, és a pénz, szint, valamint a karakterek állapota **megegyezik** a kilépés előtti állapottal. |
| **AC 10.2.** | a játékos rendelkezik elegendő pénzzel egy új karakter megvásárlásához. | a játékos megnyomja a "Vásárlás" gombot. | a pénz lecsökken a vásárlás árával, az új karakter megjelenik a csapatban, és a **teljes DPS érték azonnal megnő**. |
| **AC 10.3.** | a játékos legyőzte az aktuális zónában található utolsó normál ellenfelet. | a játék megpróbálja betölteni a következő ellenfelet. | a képernyőn egy **Boss ellenfél** jelenik meg egyedi életerő csíkkal és nagyobb életerővel. |
| **AC 10.4.** | a játékos a beállítások menüben tartózkodik. | a játékos elhúzza a "Hangerő" csúszkát 50%-ról 0%-ra. | a **zene és az összes hangeffekt hangereje lenémul**, és ez az állapot mentésre kerül. |
| **AC 10.5.** | a játékos már mentett legalább egy bejegyzést a lokális toplistába. | a játékos megnyitja a "Toplista" menüpontot. | megjelenik egy lista, amely a **legjobb pontszámokat** sorrendben (legnagyobb elől) és az elért eredményt tartalmazza. |

---

## 11. 🚀 Jövőbeli Bővítések (Roadmap)

* **11.1. Komplex Harcrendszer:** Képességek, célpontválasztás, formációk.
* **11.2. Karakterdiverzitás:** Új karaktertípusok (tank, gyógyító, sebző) implementálása.
* **11.3. Több Tartalom:** Új pályák, zónák és ellenségtípusok.
* **11.4. Online Toplista és MySQL Integráció:** Szerveroldali adatbázis (pl. **MySQL**) bevezetése a globális ranglisták kezeléséhez (ehhez online funkcionalitás szükséges).
