#  Funkcionális specifikáció

**Projekt neve:** Click the Dog (CTD)
**Verziószám:** 1.0
**Dátum:** 2025.10.15.
**Készítette:** M.A.K.E Kft
**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil

---

# 1. Bevezetés

Ez a dokumentum a **Click the Dog (CTD)** nevű játék **funkcionális specifikációját** tartalmazza.  
Célja, hogy részletesen bemutassa a játék működési logikáját, felhasználói felületeit, interakciós folyamatait, valamint az ezekért felelős rendszereket.  
A specifikáció segíti a fejlesztői munkát, az egyes modulok összehangolását, valamint a projekt jövőbeli bővítésének megtervezését.

A dokumentum a játék **jelenlegi stabil verziójára (1.0)** vonatkozik, és meghatározza azokat a funkciókat, amelyek a beadandó projekt részeként megvalósultak. Emellett keretrendszert biztosít a későbbi fejlesztésekhez, biztosítva, hogy a csapat egységesen értelmezze a játék mechanikáit és logikai működését.

A játék alapelve egy egyszerűen érthető, mégis addiktív mechanizmuson nyugszik:  
a játékos kattintásokkal sebezheti az ellenségeket, miközben előrehalad a különböző zónákon és Boss ellenfeleken keresztül.  
Ennek a dokumentumnak a feladata minden ilyen interakciót és háttérfolyamatot részletesen meghatározni és rögzíteni.


# 2. Rendszeráttekintés

A **Click the Dog** egy helyben futó, offline üzemű **clicker–harci hibrid játék**, amely a **Godot 4.x** játékmotorban, C# programozási nyelvvel készült. A rendszer moduláris felépítésű, így könnyen módosítható, optimalizálható vagy bővíthető.

A játék fő eleme egy **harci képernyő**, ahol a játékos:
- kattintással okoz sebzést,
- passzív DPS segítségével folyamatos sebzést generál,
- zónáról zónára halad,
- időszakonként Bossokat győz le.

A motor minden interakciót és háttérfolyamatot valós időben kezel. A rendszer több almodulból épül fel:

---

## 2.1. Interakciós rendszer

A játékos minden művelete (kattintás, mozgás, menükezelés) eseménykezelőkön keresztül jut el a megfelelő modulokhoz.  
A kattintási eseményeket a játék azonnal feldolgozza, és továbbítja a sebzéskezelő komponensnek.

---

## 2.2. Harcrendszer

A harcrendszer felelős az alábbiakért:

- **Kattintási sebzés (Click Damage)** kiszámítása és alkalmazása  
- **Passzív DPS értékek** folyamatos frissítése  
- **Ellenségek életerejének** kezelése  
- Az ellenség halálának ellenőrzése  
- A zóna előrehaladásának és Boss megjelenésének szabályozása  

A rendszer minden legyőzött ellenfélnél meghatározza, hogy:
- új ellenség,
- vagy zónaváltás,
- vagy Boss betöltése következzen.

---

## 2.3. Progresszió és Gazdasági Rendszer

A játékos előrehaladásához kötődő minden érték (pl. pénz, pontszám, zóna, DPS, karakterfejlődés) nyilvántartása elkülönített modulban történik.

A modul feladatai:

- Jutalmazás ellenségek legyőzésekor  
- Pénz- és pontszám-növelés  
- Zóna előrehaladás kezelése  
- Fejlesztések és statisztikák tárolása  

Minden fontos változás menthető, ezzel biztosítva a folyamatosságot.

---

## 2.4. Mentési és Betöltési Rendszer

A játék állapotát a Godot **ConfigFile** formátuma menti, mely gyors helyi mentést és visszatöltést tesz lehetővé.

A rendszer képes:

- Manuális mentésre (gombnyomással)  
- Automatikus mentésre (zónaváltáskor, vásárláskor stb.)  
- A teljes mentésfájl törlésére  

Minden mentést rövid vizuális visszajelzés kísér.

---

## 2.5. Grafikus és Felhasználói Felület Rendszer

A játék UI-ja Godot Control node-okból épül fel, reszponzív működésre optimalizálva.

Feladataik:

- Játékállapot megjelenítése (HP, pénz, pontok, zóna)  
- Gombok és menük kezelése  
- Vizuális visszajelzések biztosítása (lebegő sebzés, mentés üzenet)  
- Játékbeli jelenetek és háttérváltások megjelenítése  

---

## 2.6. Célplatform és Teljesítmény

A játék alacsony rendszerigénnyel rendelkezik, és célja, hogy **régebbi számítógépeken is gördülékenyen fusson**.

Optimalizálási fókuszok:

- alacsony CPU-terhelés,
- gyors animációkezelés,
- minimalizált memóriaterhelés,
- skálázható felbontások támogatása.

---

## 3. Felhasználói felületek (UI/UX)
<img width="1600" height="900" alt="image" src="https://github.com/user-attachments/assets/62364b3c-7313-44fa-9de2-f444143b3d4b" />

<img width="1600" height="900" alt="image (1)" src="https://github.com/user-attachments/assets/7cccfd78-6198-400f-b44a-aa927d675fe7" />


### 3.1. Főmenü

A Főmenü szolgál a játék indítására és alapvető beállítások elérésére.

| Elem | Típus | Leírás | Funkcionális Cél |
| :--- | :--- | :--- | :--- |
| **Start** | Gomb | Új játék indítása. | Létrehoz egy új játékállapotot, és átdob a 3.2. Játékfelület jelenetre. |
| **Load** | Gomb | Korábban mentett játék betöltése. | Betölti a lokális mentésfájl tartalmát, és folytatja az előrehaladást a Játékfelületen. |
| **Options** | Gomb | Beállítások menü megnyitása. | Egy felugró beállítások menüt (3.3. Opciók menü) dob fel a főmenü fölé. |
| **Quit** | Gomb | Kilépés a játékból. | Leállítja a játék alkalmazást. |

### 3.2. Játékfelület (Harci nézet)

Ez a fő képernyő, ahol a játékos interakcióba lép az ellenséggel, és figyelemmel kíséri a haladását.

| Elem | Típus | Leírás | Frissítés / Művelet |
| :--- | :--- | :--- | :--- |
| **Ellenség (Vizuális)** | Vizuális | Az aktuális ellenfél, mely a kattintások célpontja. | Változik, ha egy ellenség legyőzésre került, vagy ha új zónába lép a játékos. |
| **Háttér** | Vizuális | A pálya aktuális zónáját jelző háttérkép (pixel-art). | Változik, ha a játékos sikeresen legyőzte az adott zóna Bossát. |
| **Életerő (UI)** | Kijelző | Az aktuális ellenség hátralévő életereje. | Csökken a játékos kattintása és a segítő karakterek sebzése (DPS) után. |
| **Szint/Zóna** | Kijelző | Az aktuális zónaszám vagy szint (pl. Zóna 1-5). | Nő, ha a Boss legyőzésre kerül. |
| **Pénz** | Kijelző | A játékos jelenlegi játékbeli valutája. | Nő, ha az ellenség legyőzésre kerül. |
| **Pontok** | Kijelző | A játékos összesített pontszáma (Top Score alapja). | Folyamatosan növekszik a sebzés és a legyőzött ellenfelek után. |
| **Mentés Gomb** | Gomb | Manuális mentés funkció. | Elindítja az 4.4. Mentési folyamatot. |
| **Mentés Törlése** | Gomb | Megerősítés után törli a mentésfájlt. | Visszaállítja a játékot a kezdő állapotra (Új Játék). |
| **Bezárás (X)** | Gomb/UI | Kilép a játékból. | Leállítja a játék alkalmazást. |

### 3.3. Opciók menü (Beállítások)

Az `Options` menü a felhasználói élmény személyre szabására szolgál.

| Elem | Típus | Leírás | Funkcionális Cél |
| :--- | :--- | :--- | :--- |
| **Főhangerő** | Csúszka | A teljes játék hangerejének szabályozása. | 0% és 100% között állítható, elmentődik. |
| **Zene Hangerő** | Csúszka | A háttérzene hangerejének szabályozása. | 0% és 100% között állítható, elmentődik. |
| **SFX Hangerő** | Csúszka | A hangeffektek (támadás, vásárlás) hangerejének szabályozása. | 0% és 100% között állítható, elmentődik. |
| **Vissza Gomb** | Gomb | Bezárja az opciók menüt. | Visszatér a Főmenübe vagy a Játékfelületre. |

---

## 4. Funkcionális folyamatok (Működési logika)


### 4.1. Támadás és Sebzés (Player Click)

Ez a folyamat írja le a fő interakciót: a játékos kattintása az ellenségre.

  A[Start: Ellenség Élete = 0] --> B[Jutalmazás: Pénz és Pontok Szerzése];
  
  B --> C(Automatikus Mentés (Auto-Save) Indítása);
    
  C --> D{Volt az Ellenség Boss?};
    
  D -- Igen --> E[Zónaváltás: Következő Zóna Betöltése];
    
  D -- Nem --> F{Zóna Vége (Boss Következik)?};
    
  E --> H[Játék Folytatódik az új zónában];
    
  F -- Igen --> G[Boss Ellenség Betöltése];
    
  F -- Nem --> I[Random Normál Ellenség Betöltése az Aktuális Zónából];
    
  G --> H;
    
  I --> H;

1.  **Trigger:** A játékos a bal egérgombbal kattint az **Ellenség (Vizuális)** elemre.
2.  **Kattintás Érvényesítés:** A rendszer ellenőrzi, hogy a kattintás a hitboxon belül történt-e.
3.  **Animáció:** A karakter lejátsza a **támadási animációját** (Front-End feladat).
4.  **Sebzés Számítása:** A rendszer kiszámítja a **kattintás okozta sebzést** (Player Click Damage).
5.  **Élet Csökkentése:** Az Ellenség életereje csökken a kiszámított sebzés értékével.
6.  **Sebzés Szöveg:** Megjelenik egy lebegő sebzés szöveg az ellenség felett.
7.  **Ellenőrzés:** A rendszer ellenőrzi, hogy az Ellenség élete elérte-e a nullát (lásd 4.2. Az ellenség legyőzve).
8.  **Visszatérés:** A karakter visszakerül az alapállapot (Idle) animációjába.

### 4.2. Az Ellenség Legyőzve (Defeat)

Ez a folyamat felel a zónák és ellenfelek váltásáért.


 A[Start: Ellenség Élete = 0] --> B[Jutalmazás: Pénz és Pontok Szerzése];
 
  B --> D{Volt az Ellenség Boss?};
    
  D -- Igen --> E[Zónaváltás: Következő Zóna Betöltése];
    
  D -- Nem --> F{Zóna Vége (Boss Következik)?};
    
  E --> H[Játék Folytatódik az új zónában];
    
  F -- Igen --> G[Boss Ellenség Betöltése];
    
  F -- Nem --> I[Random Normál Ellenség Betöltése az Aktuális Zónából];
    
  G --> H;
    
  I --> H;
    
1.  **Trigger:** Az Ellenség élete eléri a **nullát vagy az alá csökken**.
2.  **Jutalmazás:** A játékos pénzt (Gold) és pontokat (Score) kap.
3.  **Mentés:** Automatikus mentés (Auto-Save) indul.
4.  **Zóna Ellenőrzés:** A rendszer ellenőrzi a legyőzött ellenfelek számát:
    * **Ha Boss volt:** A játékos belép a következő **Zónába**, és a Háttér frissül.
    * **Ha normál ellenség volt:**
        * Ha a zóna vége (Boss) még nem érkezett el: A játék automatikusan betölt egy **random ellenséget** az aktuális zónából.
        * Ha a zóna vége elérkezett: A játék betölti a **Boss ellenséget** (lásd F-K 5.4.).
5.  **Folytatás:** A játék folytatódik az új ellenséggel.

### 4.3. Mozgás (Előzetes Harcrendszer Kiegészítés)

Ez a mozgásfunkció az **5.5. Karakterpozíció Változtatás** funkcionális követelmény alapját képezi.

1.  **Trigger:** A játékos megnyomja az **E** (jobbra) vagy **Q** (balra) billentyűt.
2.  **Váltás:** A rendszer a parancsnak megfelelően **megváltoztatja a karakterek pozícióját** az előre definiált helyek között (pl. Front-line, Back-line).
3.  **Vizuális Visszajelzés:** A karakterek vizuálisan pozíciót váltanak a képernyőn.
4.  **Mentés:** Automatikus mentés (Auto-Save) indul.

### 4.4. Mentési Folyamat

1.  **Trigger:** Manuális (Mentés gomb) vagy automatikus (pl. Zónaváltás, vásárlás).
2.  **Adatgyűjtés:** A rendszer összegyűjti az összes releváns adatot: szint, pénz, pontok, karakterek szintezése, aktuális zóna.
3.  **Írás:** Az adatokat a **Godot `ConfigFile`** formátumában írja a lokális mentésfájlba.
4.  **Visszajelzés:** A képernyőn megjelenik egy rövid üzenet: "Játék elmentve."

---

## 5. Rendszerkövetelmények 

* **5.1. Hardver Teljesítmény:** Alacsony gépigényű játék, minimális erőforrás-használattal.

| Követelmény | Minimális Specifikáció | Ajánlott Specifikáció |
| :--- | :--- | :--- |
| **Operációs Rendszer** | Windows 7/10/11 (64-bit), Linux, macOS | Windows 10/11, vagy frissebb Linux/macOS |
| **Processzor (CPU)** | 2.0 GHz Dual Core | 3.0 GHz+ Quad Core |
| **Memória (RAM)** | 2 GB | 4 GB vagy több |
| **Grafikus Kártya (GPU)** | OpenGL 3.3 / Vulkan 1.0 támogatás | OpenGL 4.5 / Vulkan 1.1 támogatás (Dedikált VRAM ajánlott) |
| **Szabad lemezterület** | 50 MB | 100 MB |

* **5.2. Képernyőméret:** Reszponzív UI elemek, **mobil és asztali támogatással**.
* **5.3. Backend:** C# és Godot 4.x.
* **5.4. Frontend:** Aseprite és Godot UI eszközök.

---

## 6. Jövőbeli funkciók 

| Funkció | Prioritás | Leírás |
| :--- | :--- | :--- |
| **6.1. Ellenség Védekezés** | **Rövid táv** | Az ellenségek képesek legyenek **levédeni** a támadásainkat (sebzés csökkentés). Ez a harcrendszer mélységéhez szükséges. |
| **6.2. Karakter Diverzitás** | **Hosszú táv** | Több feloldható karakter, eltérő funkciókkal (pl. gyógyító, tank, DPS). Ez a tartalom bővítését szolgálja. |
| **6.3. QOL (Quality of Life)** | **Rövid táv** | Kisebb változtatások a legjobb felhasználói élmény érdekében (pl. jobb beállítások kezelése, apróbb kényelmi funkciók). Ezek javítják a visszatérő felhasználók élményét. |
