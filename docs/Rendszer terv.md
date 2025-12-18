# Rendszerterv: Click the Dog (CTD) - Teljes Technikai Dokumentáció

**Projekt:** Click the Dog (CTD) – Battle Clicker Game  

**Fejlesztő csapat:** M.A.K.E Kft.  

**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil  

**Verzió:** 1.3 (Expanzív technikai kiadás)  

**Dátum:** 2025. december

---

## 1. Rendszerarchitektúra és Osztályszerkezet

A játék a Godot Engine objektumorientált, jelenet-alapú rendszerét használja. A rendszer dekapcsolt (decoupled), ami lehetővé teszi az egyes modulok független tesztelését.

### Osztályhierarchia és felelősségek
1. **GameManager (Singleton):** - A globális állapot tárolása: $Level$, $Gold$, $XP$.
   - A jelenetek közötti adatátvitel és a mentési folyamatok központi vezérlése.
2. **CombatHandler (Logikai modul):** - A harci logika matematikai magja.
   - Kiszámolja a sebzést a környezeti változók alapján.
3. **PlayerController (Input modul):** - Kezeli a sávváltás (Lane switching) állapotgépét (State Machine).
   - Állapotok: `IDLE`, `MOVING`, `ATTACKING`.
4. **Enemy (Base Class):** - Életerő-kezelés és vizuális visszacsatolás.
   - Dinamikus pajzs-generálás és regenerációs ciklus.



---

## 2. Szekvenciadiagram (Interakciók folyamata)

Az alábbi folyamat szemlélteti a komponensek közötti üzenetváltást egy kattintás során.



### A folyamat lépései:
1. **Input:** A játékos kattint.
2. **Sávellenőrzés:** A `CombatHandler` összeveti a játékos sávját az `Enemy` pajzsának sávjával.
3. **Számítás:** Ha nincs blokkolás, a sebzés kiszámítása:
   $$TotalDamage = (BaseDamage + LevelBonus) \times ElementMultiplier$$
4. **Frissítés:** Az `Enemy` levonja a HP-t, a `ProgressBar` pedig interpolált (lerp) mozgással frissül.
5. **Halál-esemény:** Ha $HP \le 0$, a `GameManager` új ellenséget generál.

---

## 3. Játékegyensúly és Scaling Matematika

A játék hosszú távú játszhatóságát a progresszív nehézségi görbe biztosítja. Az ellenségek életereje exponenciálisan növekszik:

* **Ellenség HP képlete:**
  $$HP_{enemy} = BaseHP \times 1.15^{(Level - 1)}$$
* **Arany jutalom kalkuláció:**
  $$Gold_{reward} = \lfloor Level \times 5 \times (1 + RandomBonus) \rfloor$$

Ez a skálázás garantálja, hogy a játékosnak folyamatosan fejlesztenie kell a szintjét a haladáshoz.



---

## 4. Adatkezelés és Mentési Rendszer

A játék a felhasználói adatokat lokálisan tárolja. A mentési ciklus a következő esetekben fut le:
1. Minden 10. (Boss) ellenség legyőzése után.
2. A játékból való kilépéskor (felhasználói jóváhagyással - B014).



---

## 5. Teszt Esetek Listája (80 Eset - 1-20 Prefixelt)

| ID | Tesztelő | Fókuszterület | Hivatkozás (Bug) |
| :--- | :--- | :--- | :--- |
| **TM01-TM20** | Venyige M. | Alap harci mechanika, stabilitás. | B018, B002, B006, **-** |
| **TA01-TA20** | Bak A. M. | Mozgásmechanika, UI horgonyok. | B010, B012, B024, **-** |
| **TK01-TK20** | Kovács K. | Betöltési logika, Pálya koordináták. | B007, B023, B015, **-** |
| **TE01-TE20** | Jabur E. | Gazdasági egyensúly, Szintlépési görbe. | B005, B008, B025, **-** |

---

## 6. Kockázati Terv és Mitigáció (Risk Management)

| Kockázat | Valószínűség | Hatás | Megoldás (Mitigáció) |
| :--- | :--- | :--- | :--- |
| **Adatvesztés (Mentési hiba)** | Közepes | Magas | Automatikus `.bak` fájl létrehozása minden mentéskor. |
| **Input lag (B029)** | Alacsony | Közepes | Az input feldolgozás áthelyezése a `_physics_process`-be. |
| **Teljesítmény romlás** | Alacsony | Alacsony | Objektum-poolozás (Object Pooling) használata az ellenségeknél. |

---

## 7. Rendszerkövetelmények

### Minimum követelmények:
* **Operációs rendszer:** Windows 10 / Linux (Ubuntu 20.04+)
* **Processzor:** Dual Core 2.0 GHz
* **Memória:** 2 GB RAM
* **Grafika:** OpenGL 3.3 kompatibilis kártya

### Fejlesztői környezet:
* Godot Engine 4.x (Standard Edition)
* VS Code (GDScript kiterjesztéssel)

---

## 8. Projekt Roadmap (Ütemterv)

### M1: Alaprendszer (KÉSZ)
- Jelenetek közötti navigáció, alap sebzés-modell.

### M2: Taktikai Mélység (KÉSZ)
- Sávváltás (`Q`, `E`) és pajzs generálás.

### M3: Gazdasági Rendszer (KÉSZ)
- Szintlépés, Boss-ciklusok és mentési mechanika.

### M4: QA és Polírozás (Jelenleg)
- Optimalizálás, utolsó hibák (B029) elhárítása és MVP kiadás.

---

## 9. Üzemeltetési Útmutató

### Fejlesztőknek:
- Új elem (pl. "Villám") hozzáadásához a `GlobalConstants.Elements` enumot kell bővíteni.
- A sebzés-lebegő szövegek (Floating Combat Text) a `UI_Layer` jelenetben módosíthatók.

### Tesztelőknek:
- **Szélsőérték teszt:** Ellenőrizni kell, hogy $Gold < 0$ állapot nem fordulhat-e elő vásárláskor.
- **Stressz teszt:** 10 perc folyamatos, gyors kattintás mellett figyelni a memóriahasználatot (Memory leak ellenőrzés).

---
**M.A.K.E Kft. - 2025**
