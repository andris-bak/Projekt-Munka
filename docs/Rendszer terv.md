#  Rendszerterv: Click the Dog (CTD) - Teljes Technikai Dokumentáció

**Projekt:** Click the Dog (CTD) – Battle Clicker Game  

**Fejlesztő csapat:** M.A.K.E Kft.  

**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil  

**Verzió:** 1.1 (Bővített technikai kiadás)  

**Dátum:** 2025. december

---

## 1. Rendszerarchitektúra és Osztályszerkezet

A játék a Godot Engine objektumorientált, jelenet-alapú rendszerét használja. Minden egység (Entity) külön szkripttel és felelősségi körrel rendelkezik.

### Osztályhierarchia és felelősségek
1. **GameManager (Singleton):** - A globális állapot tárolása: $Level$, $Gold$, $XP$.
   - A mentési folyamatok kezelése.
2. **CombatHandler:** - A harci logika matematikai magja.
   - Kiszámolja a sebzést a következő képlet alapján:
     $$TotalDamage = (BaseDamage + LevelBonus) \times ElementMultiplier \times ShieldPenalty$$
3. **PlayerController:** - Figyeli a billentyűzetet (`Q`, `E`) és az egeret.
   - Meghívja a mozgási animációkat és a támadási jeleket.
4. **Enemy (Base Class):** - Életerő-kezelés: $HP_{new} = \max(0, HP_{old} - TotalDamage)$.
   - Regenerációs ciklus futtatása `Timer` segítségével.



---

## 2. Szekvenciadiagram (Interakciók folyamata)

Ez a diagram szemlélteti, mi történik a háttérben egyetlen sikeres kattintás során:



1. **Player (Input):** Kattint az ellenségre.
2. **CombatHandler:** Lekéri a játékos pozícióját a `PlayerController`-től és az ellenség pajzsállapotát az `Enemy`-től.
3. **Logic Check:** Ha a pozíciók ütköznek (Pajzs aktív), a folyamat megszakad.
4. **Calculation:** Ha a pajzs nem véd, a `GameManager`-től lekéri a szintbónuszokat és kiszámolja a végső értéket.
5. **Enemy Update:** Az `Enemy` levonja a HP-t és visszajelez az UI-nak (ProgressBar).
6. **GameManager:** Ha a HP eléri a 0-át, jóváírja a jutalmat ($Gold$).

---

## 3.  Teszt Esetek Listája (80 Eset - 1-20 Prefixelt)

| ID | Tesztelő | Leírás | Hivatkozás (Bug) |
| :--- | :--- | :--- | :--- |
| **TM01-TM20** | Venyige M. | Alap harci mechanika, regeneráció és kritikus hibák tesztje. | B018, B002, B006, **-** |
| **TA01-TA20** | Bak A. M. | Mozgásmechanika (Q/E), menürendszer és UI elhelyezkedés. | B010, B012, B024, **-** |
| **TK01-TK20** | Kovács K. | Ellenség betöltési logika, ProgressBar vizualizáció és pálya tesztek. | B007, B023, B015, **-** |
| **TE01-TE20** | Jabur E. | Gazdasági egyensúly, szintlépés és kurzor-interakciók. | B005, B008, B025, **-** |

*Megjegyzés: A "-" jelzéssel ellátott sorok (összesen 16+) olyan funkciókat jelölnek, amelyek az első teszteléskor hiba nélkül működtek.*

---

## 4.  Kiemelt Hibalista és Megoldások

| ID | Hiba leírása | Megoldás állapota | Technikai javítás |
| :--- | :--- | :--- | :--- |
| **B006** | Kritikus összeomlás | Javítva | Fájl elérési utak abszolút koordinálása. |
| **B018** | Pajzs áttörése | Javítva | `if` ág bővítése a karakter pozíciójának ellenőrzésével. |
| **B029** | Input leállás | **Vizsgálat alatt** | `_input` és `_unhandled_input` prioritások ütközése. |

---

##  5. Projekt Roadmap (Ütemterv)

### M1: Alaprendszer (Hónap 1)
- Jelenetek közötti navigáció és alap sebzés-modell.
- **Eredmény:** Működő kattintás és HP csökkenés.

### M2: Taktikai Mélység (Hónap 2)
- Mozgás sávok között (`Q`, `E`) és pajzs generálás.
- **Eredmény:** Pozícionálást igénylő harcrendszer.

### M3: Gazdasági Rendszer (Hónap 3)
- Szintlépés, Boss-ciklusok és mentési mechanika.
- **Eredmény:** Menthető és fejleszthető játékos profil.

### M4: QA és Polírozás (Jelenleg)
- A 80 teszt eset alapján történő finomhangolás.
- **Eredmény:** Stabil, bugmentes (28/29) MVP.

---

## 6. Használati és Karbantartási Útmutató

### Fejlesztőknek:
- Új ellenség hozzáadásához az `EnemyBase` jelenetet örököltessük.
- Az elementális típusokat a `GlobalConstants` szkriptben definiáljuk.

### Tesztelőknek:
- **Szélsőérték teszt:** A HP regeneráció soha nem emelheti a HP-t a `Max_HP` fölé.
- **Pajzs teszt:** Mindig ellenőrizzük, hogy a karakter textúrája és a sebzés-tiltás logikája szinkronban van-e (bal oldalon állva bal oldali pajzs blokkol-e).

---

**M.A.K.E Kft. - Minőség és Innováció**
