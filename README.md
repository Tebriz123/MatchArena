# MatchArena

A football matchmaking platform — players create profiles, build teams, book pitches,
and sign up for tournaments. It's built with ASP.NET Core on .NET 8 and split into two
apps: a Web API that holds all the logic and data, and an MVC site that people actually
use in the browser.


## What you can do with it

- Sign up, log in, and manage a player profile
- Create teams, invite players, and join existing ones
- Browse football fields and make reservations
- Organize tournaments and register teams for them
- Rate players, fields, and shop products
- A small store with categories/colours/sizes and Stripe checkout
- An admin area for managing the catalogue and the rest of the data

Auth is JWT on the API side and cookie-based on the MVC side. Images go to Cloudinary,
emails go out through SMTP (Mailtrap in development), and payments run on Stripe test keys.

## How it's put together

It follows a fairly standard onion/clean-architecture layout:

``
MatchArena/
├─ src/
│  ├─ Core/
│  │  ├─ MatchArena.Domain         # entities, enums
│  │  └─ MatchArena.Application     # interfaces, services, DTOs
│  └─ Infrastructure/
│     ├─ MatchArena.Persistence     # EF Core, DbContext, repositories
│     └─ MatchArena.Infrastructure  # Cloudinary, email, Stripe, etc.
└─ Presentation/
   ├─ MatchArena.API                # the Web API + Swagger
   └─ MatchArena.MVC                # the website (calls the API over HTTP)
```

The MVC site doesn't touch the database directly — it talks to the API through typed
HttpClient services. Data lives in SQL Server via Entity Framework Core.

## Running it

The easiest way is Docker. From the `MatchArena` folder (the one with the `.sln`):

```bash
cp .env.example .env        # fill in the values — any strong SA_PASSWORD is fine locally
docker compose up -d --build
```

That brings up SQL Server, the API, and the MVC site together. Once it's up:

- Website: http://localhost:8080
- API + Swagger: http://localhost:5080

(The API is on 5080 rather than the usual 5000 because macOS already grabs 5000 for
AirPlay.) The database schema is created automatically the first time the API starts.

To stop everything: `docker compose down`, or `docker compose down -v` if you also want to
throw away the database.

### Without Docker

If you'd rather run it from Visual Studio / the CLI, you'll need .NET 8 and a SQL Server
instance. Copy `Presentation/MatchArena.API/appsettings.Example.json` to `appsettings.json`,
fill in a connection string and the rest of the keys, then start the API and the MVC
project together.

## Configuration and secrets

Nothing secret is committed. The API reads its configuration from environment variables
(or a local `appsettings.json` you create yourself), and `appsettings.Example.json` shows
every key it expects. In Docker and on a host, values are passed in as env vars using the
usual `Section__Key` form, e.g. `ConnectionStrings__Default`, `Stripe__SecretKey`,
`JWT__secretKey`. See `.env.example` for the full set.

## Deploying

The two apps are small and containerised, so they'll run on most hosts. The one thing to
plan for is the database: SQL Server needs around 2 GB of RAM, which rules out the free
tiers — so you either pay a few dollars a month for it, use Azure SQL (free for students),
or switch the provider to PostgreSQL. There's a step-by-step for Railway and the trade-offs
in [CHANGES.md](CHANGES.md).

## A note on the migrations

The EF Core migrations in this repo don't apply cleanly from an empty database, so the app
builds its schema with `EnsureCreated` instead of running migrations. That's fine for
getting it up and running, but if you plan to evolve the schema properly you'll want to
regenerate the migrations from scratch first.
