# 💾 Követelményspecifikáció: Click the Dog (CTD)

**Projekt neve:** Click the Dog (CTD)
**Verziószám:** 1.0 (Kezdeti Kiadás)
**Dátum:** 2025.10.09.
**Készítette:** M.A.K.E Kft.
**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil

## 📜 Revíziótörténet

| Verzió | Dátum | Készítette | Leírás |
| :--- | :--- | :--- | :--- |
| 0.9 | 2025.09.28. | M.A.K.E Kft. | Első vázlat, főbb célok és mechanikák meghatározása. |
| **1.0** | **2025.10.09.** | **M.A.K.E Kft.** | **A hivatalos Követelményspecifikáció kiadása, funkcionális és nem funkcionális követelmények véglegesítése.** |

---

## 1. Célunk és Motiváció

* **Motor:** GODOT játékmotorban készített, **inkrementális clicker játék** elkészítése.
* **Fő cél:** Egy **egyszerű, de addiktív** játékmenet létrehozása, mely a játékban eltöltött idő, avagy az **előrehaladás** (progress) után **nehezebbé és komplexebbé válik**.
* **Stílus:** **Pixel-art vizuális stílus** alkalmazása.
* **Innováció:** Egy **egyedi, újszerű harcrendszer** kidolgozása a hagyományos clicker játékokhoz képest.

---

## 2. Bevezetés és Játéktípus

A projekt alapul a híres, egyszerű játékmenettel rendelkező inkrementális (Clicker/Idle) játékokon (pl. *Clicker Heroes*).

* **Alapmechanika:** A felhasználó a bal egérgombbal/érintéssel győzi le az ellenfeleket.
* **Fejlődés:** A játékos segítő **karakterekre** tehet szert, akik **automatikus sebzést** (DPS) okoznak. Ezek a karakterek a játékbeli valutából **szintezhetők**.
* **CTD sajátossága:** Ezt az alapvető mechanikát implementáljuk, de egy **saját, egyedi harcrendszerrel** egészítjük ki.

---

## 3. Érintettek és Szerepkörök

| Szerepkör | Tagok | Feladatok |
| :--- | :--- | :--- |
| **Back-End** | Bak András Mátyás, Kovács Krisztián | Játék **logikája**, **adatszerkezetek**, **mentés-betöltés** és a háttérprogramok. |
| **Front-End** | Jabur Emil, Venyige Márk | Játék **kinézete**, **UI/UX**, **animációk**, **vizuális effektek** és grafikai implementáció. |
| **Tesztelő** | Az egész csapat | **Közös tesztelés** és dokumentálás a minőségbiztosítás érdekében. |

---

## 4. Funkcionális Követelmények (F-K)

| Azonosító | Követelmény |
| :--- | :--- |
| **F-K 4.1.** | **Hangbeállítások:** A felhasználó képes legyen beállítani a **zene és hangeffektek** erősségét. |
| **F-K 4.2.** | **Mentés és Betöltés:** A játék képes legyen a játékos előrehaladását (pontok, pénz, szintek) **automatikusan menteni** és betölteni. |
| **F-K 4.3.** | **Vásárlás:** A felhasználó képes legyen **új karaktereket megvásárolni** és a meglévőket **szintezni**. |
| **F-K 4.4.** | **Boss Ellenfél:** A játékban legyenek **kihívást jelentő főellenségek** (Bossok). |
| **F-K 4.5.** | **Karakterpozíció Változtatás:** A felhasználó képes legyen a **karakterek pozícióin változtatni** egy gombnyomással. |
| **F-K 4.6.** | **Történetmesélés:** Egy kisebb történet elmesélése **dialogus-ablakok** használatával. |
| **F-K 4.7.** | **Újrakezdés/Reset:** A felhasználó képes legyen az összes mentett adatot **törölni** és a játékot újrakezdeni. |

---

## 5. Nem Funkcionális Követelmények (NF-K)

* **NF-K 5.1. Használhatóság (Usability):** A játékmenet legyen **intuitív** és áttekinthető UI/UX mellett **fokozatosan komplexé váló**.
* **NF-K 5.2. Teljesítmény (Performance):** **Kisméretű** játék, mely **alacsony memória- és CPU-használatot** produkál (cél: 60 FPS).
* **NF-K 5.3. Biztonság:** **Offline futás**, nincs adatgyűjtés. A mentésfájl korlátozottan védett a manipulációtól.
* **NF-K 5.4. Platformfüggetlenség:** Kiadásra törekvés **Windows/Linux/Mac OS** mellett **mobiltelefonokra** (Android) is.

---

## 6. Rendszerkörnyezet

* **Motor/Keretrendszer:** Godot Engine (verzió: 4.x)
* **Backend (Logika):** C#
* **Frontend (Grafika):** Aseprite, Paint3D
* **Terjesztés:** Itch.io
* **Adatbázis/Mentés:** **Lokális fájl alapú mentés** (pl. JSON vagy Godot ConfigFile).

---

## 7. Korlátozások (Constraints)

* A játék legelső **kiadási verziója (MVP)** **nem tartalmaz** mély, komplex harcrendszert.
* A legelső verzióban **korlátozott számú** feloldható karakter lesz elérhető.
* Kezdeti kiadásban **csak egy pálya (világ)** játszható pár ellenfél típussal.

---

## 8. Példa Felhasználói Történet

> **Felhasználóként,** avagy Játékosként szeretnék huzamosabb játékidő után **elmenteni** az elért pontjainkat, szintünket, pénzünket, hogy a játékból kilépve **ott folytathassam**, ahol abbahagytam.

---

## 9. Elfogadási Kritériumok (Acceptance Criteria - AC)

| Azonosító | Kritikus Esemény | Elfogadási Feltétel |
| :--- | :--- | :--- |
| **AC 9.1.** | Mentés/Betöltés | A játék sikeresen elmenti, és a következő megnyitáskor **hibátlanul betölti** az aktuális állapotot. |
| **AC 9.2.** | Pályaváltás | Egy meghatározott számú ellenfél legyőzése után a játék **betölti a következő "zónát"**, új ellenfelekkel. |
| **AC 9.3.** | Boss Megjelenés | Egy elért szint/zóna után a játék **betölti és megjeleníti a főellenséget**. |
| **AC 9.4.** | Karakter Vásárlás | A vásárláskor a pénz levonásra kerül, az új karakter/szint megjelenik az UI-n, és a **DPS növekszik**. |

---

## 10. Jövőbeli Bővítések (Roadmap)

* **Komplex Harcrendszer:** Képességek, célpontválasztás, formációk.
* **Karakterdiverzitás:** Új karaktertípusok (tank, gyógyító, sebző) implementálása.
* **Több Tartalom:** Új pályák, zónák és ellenségtípusok.
* **Ellenség AI Fejlesztés:** Visszatámadó, védekező és életet visszatöltő ellenfelek.
* **Hang/Zene:** Új, egyedi zenék és hangeffektek.
* **Végtelen Játékmód:** Speciális, ranglistás végtelen játékmód.
