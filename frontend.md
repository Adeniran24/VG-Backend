# Frontend Összefoglaló és Sablonok - Vizsga Készítés

## 📋 Tartalomjegyzék
1. API Összefoglalók (frontend szemmel)
2. Közös Frontend API Hívás Sablonok
3. CRUD Műveletek Sablonok (fetch/axios)
4. Hiba Kezelés Sablonok
5. UI/Állapot tippek

---

## 1) API Összefoglalók (frontend szemmel)

**Fontos:** Minden kéréshez használj közös `BASE_URL` változót, és **mindig** `Content-Type: application/json` header-t küldj POST/PUT esetén.

### Books API (BooksAPI)
- **Port:** `5085`
- **BASE_URL:** `http://localhost:5085`

**Gyakori kérések:**
- `GET /api/books` – összes könyv
- `POST /api/books?uid=FKB3F4FEA09CE43C` – új könyv (UID kötelező)
- `GET /api/authors/{authorName}` – szerző könyvei
- `GET /api/authors/Count` – szerzők száma
- `GET /api/categories` – kategóriák könyvekkel

### Halak API (HalakAPI)
- **Port:** `5028`
- **BASE_URL:** `http://localhost:5028`

**Gyakori kérések:**
- `GET /halak/kifogott` – DTO lista
- `POST /halak` – új hal
- `PUT /halak` – hal módosítása
- `DELETE /halak/{id}` – hal törlése
- `GET /horgaszok` – összes horgász
- `GET /horgaszok/{id}` – horgász ID alapján

### Recept API (ReceptAPI)
- **Port:** `5287`
- **BASE_URL:** `http://localhost:5287`

**Gyakori kérések:**
- `GET /api/recept/ById/{id}` – recept DTO
- `GET /api/hozzavalo/All` – hozzávalók
- `POST /api/hozzavalo/Uj` – új hozzávaló
- `PUT /api/szakacs/Modosit` – szakács módosítás
- `DELETE /api/szakacs/Torol/{id}` – szakács törlés

---

## 2) Közös Frontend API Hívás Sablonok

### 📌 GET sablon (fetch)
```js
const response = await fetch(`${BASE_URL}/api/books`);
if (!response.ok) throw new Error("Hiba a lekérésnél!");
const data = await response.json();
```

### 📌 POST sablon (fetch)
```js
const response = await fetch(`${BASE_URL}/api/books?uid=${uid}`, {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify(payload),
});

if (!response.ok) throw new Error("Sikertelen mentés!");
```

### 📌 GET sablon (axios)
```js
const { data } = await axios.get(`${BASE_URL}/api/books`);
```

### 📌 POST sablon (axios)
```js
await axios.post(`${BASE_URL}/api/hozzavalo/Uj`, payload, {
  headers: { "Content-Type": "application/json" },
});
```

---

## 3) CRUD Műveletek Sablonok (frontend)

### 🔍 READ - Összes elem
```js
const items = await fetch(`${BASE_URL}/api/categories`).then(r => r.json());
```

### 🔍 READ - ID alapján
```js
const item = await fetch(`${BASE_URL}/api/recept/ById/${id}`).then(r => r.json());
```

### ➕ CREATE
```js
await fetch(`${BASE_URL}/api/hozzavalo/Uj`, {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify(newItem),
});
```

### ✏️ UPDATE
```js
await fetch(`${BASE_URL}/api/szakacs/Modosit`, {
  method: "PUT",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify(updatedItem),
});
```

### 🗑️ DELETE
```js
await fetch(`${BASE_URL}/api/szakacs/Torol/${id}`, { method: "DELETE" });
```

---

## 4) Hiba Kezelés Sablonok

### ✅ Try/Catch minta
```js
try {
  const response = await fetch(url);
  if (!response.ok) throw new Error("Hibás válasz!");
  const data = await response.json();
} catch (err) {
  console.error(err);
  setError("Hiba történt a kérés során.");
}
```

### ✅ Alap státuszkód ellenőrzés
```js
if (response.status === 401) {
  setError("Nincs jogosultság!");
}
```

---

## 5) UI/Állapot tippek (vizsgán hasznos)

1. **Loading állapot**: kérés előtt `setLoading(true)`, végén `setLoading(false)`.
2. **Hiba kezelés**: minden API hívás után kezeld az `error` állapotot.
3. **Form mezők**: a `name` attribútum egyezzen a backend modellel.
4. **JSON kulcsok**: tartsd azonosan a backend DTO-val (pl. `nehezsegiSzint`).
5. **UID/Token**: ha 401-et kapsz, ellenőrizd a query paramétert.

---

## ✅ Gyors összefoglaló

- GET → adatlekérés
- POST → új adat
- PUT → módosítás
- DELETE → törlés
- Mindig ellenőrizd `response.ok` vagy `status` értéket
- Mindig `Content-Type: application/json` POST/PUT esetén
