# Tesztlap és hibalap 

**Projekt neve:** Click the Dog (CTD)  
**Készítette:** M.A.K.E Kft.  
**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil 

*(Részletesebb leírást a Bug azonosítójára kattintva lehet megtekinteni)* 

## Hibalista 

| ID | Hiba | Várt eredmény | Tényleges eredmény | Prioritás | Állapot | Megoldás |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| [B001](#b001) | Kattintásra nincs sebzés | A kattintásra támadni tudunk | Nem történik semmi | Fontos | Javítva | Összekötni a pressed() ,Signal-t egy metódussal |
| [B002](#b002) | A HP csík nem reagál | A HP csík csökken | A HP csík nem csökken | Fontos | Javítva | Sokat kellett ezzel vacakolni |
| [B003](#b003) | A HP csík nem növekedett rendesen | A HP csík az ellenség életével nem növekedett | A HP csík csak egy adott pontig ment | Fontos | Javítva | Változók megváltoztatása |
| [B004](#b004) | A szint nem növekszik | A szint növelő gomb megnyomásával a szintünk növekszik | A szintünk ugyanolyan maradt | Fontos | Javítva | Logikák átírása |
| [B005](#b005) | A pénzünk nem lesz több | A játékban az ellenség legyőzésével növekszik a pénzünk | Az ellenség legyőzésével nem növekszik a pénzünk | Közepes | Javítva | Változók és egyéb dolgok megváltoztatása |
| [B006](#b006) | Öt nap tétlenség után megnyitottuk a játékot és nem akart működni | A játékot simán meg tudnánk nyitni | Összedőlt az egész játék | Kritikus | Javítva | "Lövésem nincs, hogy mi történt itt pontosan" |
| [B007](#b007) | Az új ellenségeket nem tudtuk betölteni | "Mikor az egyik ellenség meghal, egy új jön a helyére" | Nem lett betöltve semmi sem | Fontos | Javítva | Az ellenségeket új jelenetként hoztuk létre |
| [B008](#b008) | Korlátozási problémák | "A HP, szint illetve egyéb ilyen elemeket egyszerűen le tudnánk korlátozni" | Valamiért nem akarta rendesen ezt megoldani a játék | Fontos | Javítva | Egy-két logikát át kellett dolgoznunk |
| [B009](#b009) | Az alap játékot rá akartuk tenni a pályára | Az elkezdett játékot és a megcsinált pályát össze kellett volna csak pakolni | Nem akart a pálya rendesen elhelyezkedni | Fontos | Javítva | A pályás projektre dolgozunk már ezután |
| [B010](#b010) | Az újonnan behozott mozgás mechanika nem működött mikor a hozzá kirendelt gombokat lenyomtam | Az „E” vagy „Q” billentyűk lenyomásakor a karakternek pozíciót kell váltania | Egy helyben maradt a karakter és nem történt semmi | Fontos | Javítva | Nem volt túl nehéz kijavítani,->,_Process() |
| [B011](#b011) | Miután működésre bírtam a mozgást nem került a karakter jó helyre | Az előre megadott pontokra kerül a karakter | Teljesen máshova került a pályán belül | Fontos | Javítva | Pontosabban kellett megadni a helyeket |
| [B012](#b012) | "Mechanika és pozíciók pipa, de a rossz animációk játszódnak le" | Mikor a jobb vagy baloldalon vagyunk a megfelelő animációnak kéne lejátszódnia | Teljesen ellentétes animációk játszódnak le | Fontos | Javítva | "Elnéztük a pozíciókat, de gyorsan rájöttünk" |
| [B013](#b013) | "A játék mentését törlő gomb, ment és nem töröl" | "Ha rányomunk, akkor az addigi eredményeinket törli a játék " | Törlés helyett az ellentetjét tette és elmentette a játékot | Közepes | Javítva | Picit arrább mozgatni a gombot |
| [B014](#b014) | Kilépéskor a játék automatikusan mentett | Egyszerűen kilépünk | Mikor kiléptünk egyből mentett is a játék | Kicsi | Javítva | "Később rákérdez a játék, hogy akarunk-e menteni" |
| [B015](#b015) | "Rossz ellenséget tölt be, amikor a Boss-hoz érünk " | Egy adott ellenséget kell betöltenie és nem random | A játék az ellenségeket továbbá is random töltögette be | Közepes | Javítva | Egy-két logikán kellett változtatni |
| [B016](#b016) | Az új pajzs mechanika nem akart rendesen működni | "Egy pajzsnak kellene megjelennie mely, jelzi a játékosnak, hogy ott nem tud sebezni" | Megjelenni sem akart egy pajzs sem. | Fontos | Javítva | Próbálgatások után sikerült működésre bírni |
| [B017](#b017) | A pajzsot a játék rossz helyre töltötte be | Az ellenség egyik vagy másik oldalára kellett volna betölteni a pajzsot | Teljesen más helyre töltötte be | Közepes | Javítva | "Pontosabban kellett megadni, hogy hova kerüljenek a pajzsok" |
| [B018](#b018) | "Habár betölti a pajzsot, a mechanikának logikája nem működött rendesen" | "Mikor megjelenik a pajzs, akkor ott nem tud sebezni a játékos" | Teljes káosz. Vagy tudott sebezni az egyik helyen vagy egyiken sem. Teljesen rosszul működött | Fontos | Javítva | Az OnClick() metódusunkba kellett ellenőrizni az egészséget. |
| [B019](#b019) | Menügomb nem reagál | "Mikor ezt a gombot megnyomjuk, felnyílik egy menü" | "Semmi sem történik, mikor megnyomjuk a gombot " | Közepes | Javítva | Át kellett rendezgetni a kódban a Node-okat |
| [B020](#b020) | Az ellenség életeröjének visszatöltése nem jó | Minden egyes másodpercben az ellenségnek a HP-ja visszatöltődik | A játék nem csinál semmit sem | Fontos | Javítva | Timer-t kellett használnunk az egész |
| [B021](#b021) | A regeneráció túl viszi az ellenség maximum HP-ját | A regeneráció csak szimplán visszatölti az ellenség HP-ját | A maximum értéken túl megy az egész | Fontos | Javítva | Egy-két érték átállítása |
| [B022](#b022) | Az új mechanika az ellenségek ellenállása nem működik rendesen | "A játékos számára egyértelműen látszik, hogy az ellenségnek milyen fajta ellenállása van egy kis rajz által" | "Semmi sem töltődik be vagy, ha betölt egyet utána mikor más lesz, az ellenállás az előző nem tűnik el" | Fontos | Javítva | "Switch caseNem a legszebb megoldás vagy a legjobb, de most megteszi" |
| [B023](#b023) | "Valamiért, amikor elindul a játék a HP csík nincs teljesen feltöltve" | Egyszerűen csak simán betöltené a HP csíkot | A HP csík csak egy darabig töltődik fel.,Pl.: a feléig | Közepes | Javítva | ProgressBar-ban be kellett állítani mást |
| [B024](#b024) | Az opciók menü nem helyezkedik el rendesen | "Ahogy megnyitjuk az opciók menüt ott lesz látható, ahova tettük" | Teljesen máshova kerül az egész | Kicsi | Javítva | Az Anchor Point-ot kellett jó helyre tenni |
| [B025](#b025) | A saját kurzorok nincsenek rendesen beállítva | "Mikor rávisszük, az ellenségre a kurzort megváltozik az másra" | Ezek a kurzorok nem akartak rendesen betölteni | Kicsi | Javítva | Egy-két változtatás a szkriptben |

---

## Részletesebb leírások 

### B001
Elég elveszettek vagyunk még ebben az új programban. De utána rájöttünk, hogy Exportálni kell a gombot a szkripten belül. 
Majd ezt csak össze kell kötni a Fő jelenet Inspector fülén és már minden működött is. 

### B002
Nehéz bevallani, de beletelt némi időbe mire sikeresen összehoztuk ezt a ProgressBar-t. 
Most már nem tűnik egyáltalán nehéznek, de ez van, mikor egy teljesen új programban csinál akármit is az ember. 
A megoldás annyi volt, hogy a Main szkriptben kellett Exportálni, mint a kattintásnál is ezt össze kellett kapcsolni a Fő jelenet Inspectorában. 
Illetve meg kellett oldanunk, hogy a ProgressBar step, value, minvalue és maxvalue-jét összhangban legyen a kattintással. 

### B003 
A HP csík nagyon nagyon nehezen akart működni mivel teljesen új közegben vagyunk kértünk segitséget az AI-tól illetve oktató vieok formájában 
és ezek segitséggével sikeresen meg tudtuk oldani azt hogy a HP csík rendeltetés szerüen müködjön!

### B004 
Nem működött a szint lépés a játékban , mivel probléma volt a szint növelő gomb hitboxával , teljesen el volt csuszva az egész és ez okozta a problémát  , beállítotuk a hitboxot így már müködött

### B005
Miután legyöztünk egy ellenséget nem növekedett a pénzünk a játékban , létre kellet hozni egy új metodust, miután az ellenség életereje 0 ra eset egy másik változohoz hozzá adtuk az előre megszabott értéket 
így már müködöt is a kód

### B006
Meg niylt maga a kód viszont teljesen össze omlot az egész ugy hogy senki nem nyult hozzá, kimásoltuk az előző mentést beillesztetük és gond nélkül lefutot minden és a játék elindult, mai napig nem tudjuk mi lehetet a baj de ovosoltuk

### B007
Nem akarta érzékelni a többi ellenséget így mindet át raktuk egy másik sceenbe és egy Random segitségével mindig más és más Sceent tölbe

### B008
Nem voltak jók a korlátok mivel rossz volt a lineáris gondolkodásunk , meg cseréltünk pár sort hogy mi mi utn fusson le és utána már müködött

### B009
Baj volt a pályának a helyzetével , félre néztük a Sceen tájolásást és ezért nem akart rendesen a helyére állni , kiszámoltuk a pontos koordinátákat és a pálya a helyére került!

### B010
Nem akart a karakterünk poziciot váltani , rossz keyre volt téve a mozgás illetve rossz helyen volt a kódban és nem futot le

### B011
Nem jó koordinátára került a karakter , pontosabban utána kellett nézni hogy hova is kerüljön át!

### B012
Rossz animáciok futotak le viszont csak annyi volt a baj hogy félrenéztük és megcseréltük a kettő animácionak a koordinátáit!

### B013

