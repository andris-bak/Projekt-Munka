#  Tesztlap és Hibalap

**Projekt neve:** Click the Dog (CTD)

**Készítette:** M.A.K.E Kft.

**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil

---

## 1.  Tesztelési Módszertan

A tesztelés elsősorban **Black-Box (Fekete dobozos)** és **Ad-Hoc (Alkalmi)** módszerekkel történt, a fejlesztés minden szakaszában.

* **Típus:** **Manuális tesztelés** (nem készült automatizált tesztcsomag).
* **Fókusz:** A fejlesztési ciklus során megvalósított funkcionális követelmények (pl. támadás, mozgás, mentés) működőképességének azonnali ellenőrzése.
* **Módszer:** **Unit Testing** (Egyedi funkciók tesztelése) és **Integration Testing** (Funkciók közötti együttműködés tesztelése).
* **Dokumentálás:** Minden rögzített hiba a Hibalistában (ID, Várt/Tényleges eredmény) került rögzítésre, a megoldás leírásával együtt.
* **Prioritás skála:**
    * **Kritikus:** A játék használhatatlan, nem indul el, vagy menthetetlen.
    * **Fontos:** Kulcsfontosságú funkció (pl. sebzés, mozgás) nem működik.
    * **Közepes:** Egy kevésbé fontos funkció hibás (pl. UI elem elcsúszása, nem kritikus mentési hiba).
    * **Kicsi:** Esztétikai vagy apróbb, kényelmi hiba.

---

## 2.  Hibalista

*(Részletesebb leírást a Bug azonosítójára kattintva lehet megtekinteni)*

| ID | Hiba | Várt eredmény | Tényleges eredmény | Prioritás | Állapot | Megoldás |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| [B001](#b001) | Kattintásra nincs sebzés | A kattintásra támadni tudunk | Nem történik semmi | Fontos | Javítva | Összekötni a pressed() Signal-t egy metódussal |
| [B002](#b002) | A HP csík nem reagál | A HP csík csökken | A HP csík nem csökken | Fontos | Javítva | Sokat kellett ezzel vacakolni |
| [B003](#b003) | A HP csík nem növekedett rendesen | A HP csík az ellenség életével növekedett volna | A HP csík csak egy adott pontig ment | Fontos | Javítva | Változók megváltoztatása |
| [B004](#b004) | A szint nem növekszik | A szint növelő gomb megnyomásával a szintünk növekszik | A szintünk ugyanolyan maradt | Fontos | Javítva | Logikák átírása |
| [B005](#b005) | A pénzünk nem lesz több | A játékban az ellenség legyőzésével növekszik a pénzünk | Az ellenség legyőzésével nem növekszik a pénzünk | Közepes | Javítva | Változók és egyéb dolgok megváltoztatása |
| [B006](#b006) | Öt nap tétlenség után megnyitottuk a játékot és nem akart működni | A játékot simán meg tudnánk nyitni | Összedőlt az egész játék | Kritikus | Javítva | "Lövésem nincs, hogy mi történt itt pontosan" (Korábbi mentés visszaállítása) |
| [B007](#b007) | Az új ellenségeket nem tudtuk betölteni | Mikor az egyik ellenség meghal, egy új jön a helyére | Nem lett betöltve semmi sem | Fontos | Javítva | Az ellenségeket új jelenetként hoztuk létre |
| [B008](#b008) | Korlátozási problémák | A HP, szint illetve egyéb ilyen elemeket egyszerűen le tudnánk korlátozni | Valamiért nem akarta rendesen ezt megoldani a játék | Fontos | Javítva | Egy-két logikát át kellett dolgoznunk |
| [B009](#b009) | Az alap játékot rá akartuk tenni a pályára | Az elkezdett játékot és a megcsinált pályát össze kellett volna csak pakolni | Nem akart a pálya rendesen elhelyezkedni | Fontos | Javítva | A pályás projektre dolgozunk már ezután (Koordináták beállítása) |
| [B010](#b010) | Az újonnan behozott mozgás mechanika nem működött | Az „E” vagy „Q” billentyűk lenyomásakor a karakternek pozíciót kell váltania | Egy helyben maradt a karakter és nem történt semmi | Fontos | Javítva | Nem volt túl nehéz kijavítani (A `_Process()` metódus használata) |
| [B011](#b011) | Működésre bírtam a mozgást, de a karakter rossz helyre került | Az előre megadott pontokra kerül a karakter | Teljesen máshova került a pályán belül | Fontos | Javítva | Pontosabban kellett megadni a helyeket |
| [B012](#b012) | Rossz animációk játszódtak le | Mikor a jobb vagy baloldalon vagyunk a megfelelő animációnak kéne lejátszódnia | Teljesen ellentétes animációk játszódtak le | Fontos | Javítva | Elnéztük a pozíciókat, de gyorsan rájöttünk (Koordináták felcserélése) |
| [B013](#b013) | A játék mentését törlő gomb, ment és nem töröl | Ha rányomunk, akkor az addigi eredményeinket törli a játék | Törlés helyett elmentette a játékot | Közepes | Javítva | Picit arrább mozgatni a gombot (Hitbox javítás) |
| [B014](#b014) | Kilépéskor a játék automatikusan mentett | Egyszerűen kilépünk | Kilépéskor egyből mentett is a játék | Kicsi | Javítva | Később rákérdez a játék, hogy akarunk-e menteni (A mentési logika eltávolítása a kilépés funkcióból) |
| [B015](#b015) | Rossz ellenséget tölt be a Boss-hoz érve | Egy adott ellenséget kell betöltenie és nem random | A játék az ellenségeket továbbra is random töltötte be | Közepes | Javítva | Logikán kellett változtatni, a Fő ellenséget külön jelenetként hívjuk elő |
| [B016](#b016) | Az új pajzs mechanika nem akart rendesen működni | Egy pajzsnak kellene megjelennie, mely jelzi a játékosnak, hogy ott nem tud sebezni | Megjelenni sem akart egy pajzs sem | Fontos | Javítva | Próbálgatások után sikerült működésre bírni (Külön jelenetbe helyezés) |
| [B017](#b017) | A pajzsot a játék rossz helyre töltötte be | Az ellenség egyik vagy másik oldalára kellett volna betölteni a pajzsot | Teljesen más helyre töltötte be | Közepes | Javítva | Pontosabban kellett megadni, hogy hova kerüljenek a pajzsok |
| [B018](#b018) | A pajzs logikája nem működött rendesen | Mikor megjelenik a pajzs, ott nem tud sebezni a játékos | Teljes káosz. Vagy tudott sebezni az egyik helyen vagy egyiken sem | Fontos | Javítva | Az `OnClick()` metódusba kellett ellenőrizni az egészséget |
| [B019](#b019) | Menügomb nem reagál | Mikor ezt a gombot megnyomjuk, felnyílik egy menü | Semmi sem történik, mikor megnyomjuk a gombot | Közepes | Javítva | Át kellett rendezgetni a kódban a Node-okat |
| [B020](#b020) | Az ellenség életerejének visszatöltése nem jó | Minden egyes másodpercben az ellenségnek a HP-ja visszatöltődik | A játék nem csinál semmit sem | Fontos | Javítva | Timer-t kellett használnunk az egészhez |
| [B021](#b021) | A regeneráció túl viszi az ellenség maximum HP-ját | A regeneráció csak szimplán visszatölti az ellenség HP-ját | A maximum értéken túl megy az egész | Fontos | Javítva | Egy-két érték átállítása (Korlátozás változóval) |
| [B022](#b022) | Az új mechanika (ellenségek ellenállása) nem működik | A játékos számára egyértelműen látszik, hogy az ellenségnek milyen fajta ellenállása van | Semmi sem töltődik be, vagy a régi ellenállás nem tűnik el | Fontos | Javítva | Globális változó és `switch case` logikával megoldva |
| [B023](#b023) | Amikor elindul a játék a HP csík nincs teljesen feltöltve | Egyszerűen csak simán betöltené a HP csíkot | A HP csík csak egy darabig töltődik fel (pl.: a feléig) | Közepes | Javítva | `ProgressBar`-ban be kellett állítani mást (Logikai rendezés) |
| [B024](#b024) | Az opciók menü nem helyezkedik el rendesen | Ahogy megnyitjuk az opciók menüt ott lesz látható, ahova tettük | Teljesen máshova kerül az egész | Kicsi | Javítva | Az Anchor Point-ot kellett jó helyre tenni |
| [B025](#b025) | A saját kurzorok nincsenek rendesen beállítva | Mikor rávisszük, az ellenségre a kurzort megváltozik | Ezek a kurzorok nem akartak rendesen betölteni | Kicsi | Javítva | Egy-két változtatás a szkriptben (Metódus helyes beillesztése) |

---

## 3. Részletesebb leírások

### B001
Elég elveszettek vagyunk még ebben az új programban. De utána rájöttünk, hogy Exportálni kell a gombot a szkripten belül. Majd ezt csak össze kell kötni a Fő jelenet Inspector fülén és már minden működött is.

### B002
Nehéz bevallani, de beletelt némi időbe mire sikeresen összehoztuk ezt a `ProgressBar`-t. Most már nem tűnik egyáltalán nehéznek, de ez van, mikor egy teljesen új programban csinál akármit is az ember. A megoldás annyi volt, hogy a Main szkriptben kellett Exportálni, mint a kattintásnál is, ezt össze kellett kapcsolni a Fő jelenet Inspectorában. Illetve meg kellett oldanunk, hogy a `ProgressBar` step, value, minvalue és maxvalue-jét összhangban legyen a kattintással.

### B003
A HP csík nagyon nagyon nehezen akart működni. Mivel teljesen új közegben vagyunk, kértünk segitséget az AI-tól, illetve oktató videók formájában, és ezek segitségével sikeresen meg tudtuk oldani, hogy a HP csík rendeltetés szerüen müködjön!

### B004
Nem működött a szint lépés a játékban, mivel probléma volt a szint növelő gomb hitboxával. Teljesen el volt csúszva az egész, és ez okozta a problémát. Beállítotuk a hitboxot, így már müködött.

### B005
Miután legyőztünk egy ellenséget, nem növekedett a pénzünk a játékban. Létre kellet hozni egy új metódust. Miután az ellenség életereje 0-ra esett, egy másik változóhoz hozzá adtuk az előre megszabott értéket, így már müködöt is a kód.

### B006
Meg nyílt maga a kód, viszont teljesen össze omlott az egész, úgyhogy senki nem nyúlt hozzá. Kimásoltuk az előző mentést, beillesztettük, és gond nélkül lefutott minden és a játék elindult. Mai napig nem tudjuk, mi lehetett a baj, de orvosoltuk.

### B007
A problémánk az volt, hogy nem akarta érzékelni a többi ellenséget a kód. Így minden képet át raktunk egy másik Scene-be, és egy Random változó segitségével mindig más és más Scene-t tölt be a játék, ha az előző ellenség életereje 0-ra csökken!

### B008
Nem voltak jók a korlátok, mivel rossz volt a lineáris gondolkodásunk és a sorok teljesen rossz sorrendben futottak le. Ez okozta a problémát, tehát meg cseréltünk pár sort, átgondoltuk, hogy mi mi után fusson le, és utána már müködött a program.

### B009
Baj volt a pályának a helyzetével. Félrenéztük a Scene tájolásást, és ezért nem akart rendesen a helyére állni. Kiszámoltuk a pontos koordinátákat és a pálya a helyére került!

### B010
Nem akart a karakterünk pozíciót váltani. Rossz key-re volt téve a mozgás, illetve rossz helyen volt a kódban, és nem futott le.

### B011
Nem jó koordinátára került a karakter, mivel a Scene teljesen el volt tájolva, és 1-2 koordinátát teljesen más kép kellett megnézni, mint ahogy mi azt gondoltuk. Kiszámítottuk, hogy hova kell, behelyeztük, és már müködött is!

### B012
Rossz animációk futottak le, viszont csak annyi volt a baj, hogy félrenéztük, és megcseréltük a kettő animációnak a koordinátáit!

### B013
Azért nem mentett a gomb, mivel hitbox problémánk volt megint. Valamiért a mentés gomb hitboxa óriási volt, így lefedte a törlés gombot is.

### B014
Szintén ugyan az volt a probléma, mint a törlésnél, akkor még nem volt át állítva a hitbox!

### B015
A kód meg kavarodott a Scene-ek és a random számok miatt, valamint hogy a Fő ellenség csak egy adott szint felett jelenik meg. Azt is kitettük külön Scene-be, és egy segéd változóval hívjuk elő!

### B016
Nem akarta érzékelni a programsor a pajzsot, mivel ugyan úgy, mint az ellenségeknél nem segített semmi más, csak az, hogy kiraktuk egy külön Scene-be a pajzsokat és már müködött is!

### B017
Rossz helyen voltak a koordináták. Egy hatalmas pajzsot töltött be a játék, így kisebbre kellett venni, illetve egy külön Scene-be is ki kellett tenni, hogy rendesen be lehessen illeszteni a pontos helyére!

### B018
Rossz helyre tettük be a metódust és össze kavarodott a program. Át kellett nézni az egészet, majd lineárisan átgondolni, és utána jó helyre sikerült be illeszteni.

### B019
Rossz helyen volt a metódus, és ezért nem tudod lefutni rendesen, ami azt eredményezte, hogy nem csinált a menü gomb semmit. Át kellett látni a programot, jó helyre beilleszteni, és már müködött is.

### B020
Nem töltődött vissza az ellenség élet ereje, mivel nem volt hozzá megfelelő Timer. Be lett rakva egy a megfelelő helyre és müködött.

### B021
Logikai hiba történt, mivel túl ment a maximum élet erőn. Le kellett korlátozni egy változóval a regenerációt.

### B022
Random változóval akartuk megoldani, de mivel nem volt globális, ezért nem müködött. Miután globális lett, egy `switch case` logikával megoldottuk a problémát.

### B023
A regeneráció és egyéb programok állítása okozta a hibát a kódban. Kicsit át kellett rendezni logikailag és utána müködött.

### B024
Szerencsére csak koordinációs probléma volt, rossz helyre illesztettük be az Anchor Point-ot, de amint átnéztük a programot, rájöttünk, hogy hol van a pontos helye és gond nélkül le futott.

### B025
Logikai hibába ütköztünk csak szerencsére, rossz helyre volt beillesztve a metódus, ami ezzel foglalkozik, viszont egy kis gondolkodás után sikeresen meg találtuk a helyét és működik is !

---

## 4.  Összefoglalás és Következtetések

| Elem | Érték | Állapot |
| :--- | :--- | :--- |
| **Összes talált hiba** | 25 | Teljes |
| **Kritikus hibák** | 1 (B006) | Javítva |
| **Fontos hibák** | 16 | Javítva |
| **Jelenlegi állapot** | **0 nyitott hiba** | **A projekt készen áll az MVP kiadásra.** |

---

## 5.  Tanulságok és Tapasztalatok (Lessons Learned)

A projekt tesztelése és hibajavítása során a csapat számos értékes tapasztalattal gazdagodott:

* **A `Signal`-ok és Exportálás fontossága (Godot specifikus):** A kezdeti hibák (B001, B002) rávilágítottak arra, hogy az új motorban (Godot) mennyire kritikus a `pressed()` `Signal`-ok megfelelő összekötése és a komponensek **Exportálása** a fő Scene-ekben, szemben a korábbi programozási paradigmákkal.
* **A lineáris gondolkodás felülvizsgálata (B008):** A korlátozási és logikai problémák (B008) gyakran abból adódtak, hogy a sorok nem a megfelelő sorrendben futottak le. Megtanultuk, hogy alaposabban át kell gondolni a metódusok hívási sorrendjét (`_Process()`, `_Ready()` stb.) a várt eredmény eléréséhez.
* **A Scene-ek szerepe a komplexitás kezelésében (B007, B016):** Ahelyett, hogy egyetlen Scene-t próbálnánk túltömöríteni, az ellenségeket, a pajzsot és a Boss-t (B007, B016) külön Scene-ekbe helyezve sokkal átláthatóbbá és kezelhetőbbé vált a kód, különösen a véletlenszerű elemek betöltésénél.
* **Hitbox és UI prioritások (B013, B014):** A UI elemek (gombok, hitboxok) helytelen elhelyezése komoly funkcionalitási problémákat okozott (pl. a törlés gomb nem működött). Ez megerősítette a **pontos koordináta- és `Anchor Point`-beállítások** fontosságát a platformfüggetlenség érdekében.
* **Változókezelés és hatókör (B022):** A regeneráció és az ellenállás mechanikájának bevezetésénél (B022) felmerült, hogy a **globális változók** helyes kezelése kritikus, amikor különböző funkciók (pl. randomizálás, `switch case` logika) hivatkoznak rájuk.

**Javaslat a jövőre:** A következő projekt során a csapatnak érdemes lenne a fejlesztés korai szakaszában a UI és hitbox beállításokat külön, dedikált tesztekkel ellenőrizni, mielőtt a mélyebb logikai implementációba belekezdenek.

---
