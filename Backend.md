# Backend sablonok (kezdőknek)

Ez a fájl minden meglévő controllerhez és endpointhoz ad egy **egyszerű sablont**.  
Mindenhol külön jelölöm: **mit kell átírni**.

---

## 1) Általános controller sablon

```csharp
using Microsoft.AspNetCore.Mvc;

namespace [NAMESPACE]
{
    [Route("api/[controller]")]
    [ApiController]
    public class [ControllerNeve] : ControllerBase
    {
        private readonly [DbContextNeve] _context;

        public [ControllerNeve]([DbContextNeve] context)
        {
            _context = context;
        }

        [HttpGet("[VegpontNev]")]
        public async Task<IActionResult> [MetodusNev]([PARAMETEREK])
        {
            try
            {
                // Itt írd meg az adatbázis műveletet
                return Ok([VALASZ_ADAT]);
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba: " + ex.Message);
            }
        }
    }
}
```

### Mit kell átírni?
- `[NAMESPACE]` → a projekt namespace-e (pl. `ReceptAPI.Controllers`)
- `[ControllerNeve]` → pl. `ReceptController`
- `[DbContextNeve]` → pl. `ReceptdbContext`
- `[VegpontNev]` → route rész (pl. `ById/{id}`)
- `[MetodusNev]` → C# metódusnév
- `[PARAMETEREK]` → pl. `int id` vagy `[FromBody] DTO dto`
- `[VALASZ_ADAT]` → amit vissza akarsz adni (`lista`, `objektum`, stb.)

---

## 2) Projectenkénti controller sablonok

## ReceptAPI

### ReceptController
Alap route: `/api/Recept`

```csharp
[Route("api/[controller]")]
[ApiController]
public class ReceptController : ControllerBase
{
    private readonly ReceptdbContext _context;

    public ReceptController(ReceptdbContext context)
    {
        _context = context;
    }

    [HttpGet("ById/{id}")]
    public IActionResult GetById(int id)
    {
        // TODO: id alapján recept lekérése + DTO visszaadás
        return Ok();
    }
}
```

### SzakacsController
Alap route: `/api/Szakacs`

```csharp
[Route("api/[controller]")]
[ApiController]
public class SzakacsController : ControllerBase
{
    private readonly ReceptdbContext _context;

    public SzakacsController(ReceptdbContext context)
    {
        _context = context;
    }

    [HttpPut("Modosit")]
    public async Task<IActionResult> Modosit([FromBody] Szakac szakacs)
    {
        // TODO: meglévő rekord keresése és frissítése
        return Ok();
    }

    [HttpDelete("Torol/{id}")]
    public IActionResult Torol(int id)
    {
        // TODO: id alapján törlés
        return Ok();
    }
}
```

### HozzavaloController
Alap route: `/api/Hozzavalo`

```csharp
[Route("api/[controller]")]
[ApiController]
public class HozzavaloController : ControllerBase
{
    private readonly ReceptdbContext _context;

    public HozzavaloController(ReceptdbContext context)
    {
        _context = context;
    }

    [HttpGet("All")]
    public IActionResult GetAll()
    {
        // TODO: összes hozzávaló lekérése
        return Ok();
    }

    [HttpPost("Uj")]
    public async Task<IActionResult> Uj([FromBody] Hozzavalo hozzavalo)
    {
        // TODO: új hozzávaló mentése
        return StatusCode(201);
    }
}
```

## WebApplication1

### CategoriesController
Alap route: `/api/Categories`

```csharp
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly LibrarydbContext _context;

    public CategoriesController(LibrarydbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        // TODO: kategóriák + kapcsolódó könyvek lekérése
        return Ok();
    }
}
```

### BooksController
Alap route: `/api/Books`

```csharp
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibrarydbContext _context;

    public BooksController(LibrarydbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        // TODO: könyvek + szerző + kategória lekérése
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromQuery] string uid, [FromBody] Book book)
    {
        // TODO: uid ellenőrzése (Program.UID) + könyv mentése
        return StatusCode(201);
    }
}
```

### AuthorsController
Alap route: `/api/Authors`

```csharp
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly LibrarydbContext _context;

    public AuthorsController(LibrarydbContext context)
    {
        _context = context;
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetBooksByAuthor(string name)
    {
        // TODO: szerző keresése név alapján + könyvek visszaadása
        return Ok();
    }

    [HttpGet("Count")]
    public async Task<IActionResult> GetAuthorsCount()
    {
        // TODO: szerzők számának visszaadása
        return Ok();
    }
}
```

## Csatahajok_backend

### CsataController
Alap route: `/api/Csata`

```csharp
[Route("api/[controller]")]
[ApiController]
public class CsataController : ControllerBase
{
    private readonly CsatahajokContext _context;

    public CsataController(CsatahajokContext context)
    {
        _context = context;
    }

    [HttpGet("Resztvevok/{name}")]
    public async Task<IActionResult> Resztvevok(string name)
    {
        // TODO: csata résztvevő hajóinak listázása
        return Ok();
    }
}
```

### KimenetController
Alap route: `/api/Kimenet`

```csharp
[Route("api/[controller]")]
[ApiController]
public class KimenetController : ControllerBase
{
    private readonly CsatahajokContext _context;

    public KimenetController(CsatahajokContext context)
    {
        _context = context;
    }

    [HttpPost("UjKimenet")]
    public async Task<IActionResult> UjKimenet([FromBody] KimenetCreateDto dto)
    {
        // TODO: validáció + létezés ellenőrzés + mentés
        return Ok();
    }

    [HttpDelete("KimenetTorles/{csata}/{hajonev}")]
    public async Task<IActionResult> KimenetTorles(string csata, string hajonev)
    {
        // TODO: összetett kulcs alapján törlés
        return Ok();
    }
}
```

### HajoController
Alap route: `/api/Hajo`

```csharp
[Route("api/[controller]")]
[ApiController]
public class HajoController : ControllerBase
{
    private readonly CsatahajokContext _context;

    public HajoController(CsatahajokContext context)
    {
        _context = context;
    }

    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        // TODO: teljes lista visszaadása
        return Ok();
    }

    [HttpGet("ByName/{name}")]
    public async Task<IActionResult> ByName(string name)
    {
        // TODO: név alapján egy rekord visszaadása
        return Ok();
    }
}
```

---

## 3) Endpoint sablonok (egyenként)

`{BASE_URL}` példa: `http://localhost:5000` vagy `https://localhost:7000`

## ReceptAPI endpointok

### 1) GET `/api/Recept/ById/{id}`
```bash
curl -X GET "{BASE_URL}/api/Recept/ById/{id}"
```
Mit írj át:
- `{BASE_URL}` → futó API címe
- `{id}` → a keresett recept azonosítója

Válasz sablon:
```json
{
  "nev": "string",
  "elkeszitesiIdo": 0,
  "nehezsegiSzint": "string",
  "szakacsNev": "string"
}
```

### 2) PUT `/api/Szakacs/Modosit`
```bash
curl -X PUT "{BASE_URL}/api/Szakacs/Modosit" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 0,
    "nev": "string",
    "email": "string",
    "telefonszam": "string"
  }'
```
Mit írj át:
- `id` → módosítandó szakács ID
- `nev`, `email`, `telefonszam` → új adatok

### 3) DELETE `/api/Szakacs/Torol/{id}`
```bash
curl -X DELETE "{BASE_URL}/api/Szakacs/Torol/{id}"
```
Mit írj át:
- `{id}` → törlendő szakács ID

### 4) GET `/api/Hozzavalo/All`
```bash
curl -X GET "{BASE_URL}/api/Hozzavalo/All"
```
Mit írj át:
- Csak `{BASE_URL}`

### 5) POST `/api/Hozzavalo/Uj`
```bash
curl -X POST "{BASE_URL}/api/Hozzavalo/Uj" \
  -H "Content-Type: application/json" \
  -d '{
    "nev": "string",
    "kaloria": 0
  }'
```
Mit írj át:
- `nev` kötelező, `kaloria` opcionális

## WebApplication1 endpointok

### 6) GET `/api/Categories`
```bash
curl -X GET "{BASE_URL}/api/Categories"
```
Mit írj át:
- Csak `{BASE_URL}`

### 7) GET `/api/Books`
```bash
curl -X GET "{BASE_URL}/api/Books"
```
Mit írj át:
- Csak `{BASE_URL}`

### 8) POST `/api/Books?uid={uid}`
```bash
curl -X POST "{BASE_URL}/api/Books?uid={uid}" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "string",
    "publishDate": "2026-01-01",
    "authorId": 0,
    "categoryId": 0
  }'
```
Mit írj át:
- `{uid}` → **kötelező**, egyezzen `Program.UID` értékkel
- `title`, `publishDate`, `authorId`, `categoryId` a `Book` model szerint

### 9) GET `/api/Authors/{name}`
```bash
curl -X GET "{BASE_URL}/api/Authors/{name}"
```
Mit írj át:
- `{name}` → szerző neve

### 10) GET `/api/Authors/Count`
```bash
curl -X GET "{BASE_URL}/api/Authors/Count"
```
Mit írj át:
- Csak `{BASE_URL}`

## Csatahajok_backend endpointok

### 11) GET `/api/Csata/Resztvevok/{name}`
```bash
curl -X GET "{BASE_URL}/api/Csata/Resztvevok/{name}"
```
Mit írj át:
- `{name}` → csata neve

### 12) POST `/api/Kimenet/UjKimenet`
```bash
curl -X POST "{BASE_URL}/api/Kimenet/UjKimenet" \
  -H "Content-Type: application/json" \
  -d '{
    "hajo": "string",
    "csata": "string",
    "eredmeny": "string"
  }'
```
Mit írj át:
- Mindhárom mező kötelező: `hajo`, `csata`, `eredmeny`

### 13) DELETE `/api/Kimenet/KimenetTorles/{csata}/{hajonev}`
```bash
curl -X DELETE "{BASE_URL}/api/Kimenet/KimenetTorles/{csata}/{hajonev}"
```
Mit írj át:
- `{csata}` → csata neve
- `{hajonev}` → hajó neve

### 14) GET `/api/Hajo/All`
```bash
curl -X GET "{BASE_URL}/api/Hajo/All"
```
Mit írj át:
- Csak `{BASE_URL}`

### 15) GET `/api/Hajo/ByName/{name}`
```bash
curl -X GET "{BASE_URL}/api/Hajo/ByName/{name}"
```
Mit írj át:
- `{name}` → hajó neve

---

## 4) Gyors ellenőrzőlista kezdőknek

- Futtasd az API-t (`dotnet run`) a megfelelő projektben.
- Nyisd meg a Swagger felületet (`/swagger`) ha elérhető.
- Minden kérésnél cseréld ki a placeholder értékeket (`{BASE_URL}`, `{id}`, `{name}` stb.).
- POST/PUT esetén ellenőrizd, hogy a JSON mezőnevek egyeznek a modellel/DTO-val.
- Ha 401-et kapsz a `/api/Books` POST-nál, ellenőrizd a `uid` query paramétert.
