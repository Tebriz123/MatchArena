# MatchArena — Fixes & Deployment Guide

Makes the MatchArena platform (ASP.NET Core MVC + API, .NET 8) actually run and deploy.
Verified locally with Docker (SQL Server + API + MVC, all returning HTTP 200), including
a dynamic-port test and two API restarts.

## Can it be deployed publicly for FREE?

**The two .NET apps: yes.** They are tiny — MVC ~48 MB RAM, API ~144 MB RAM — and fit any
free tier. They now also bind to the platform-injected `$PORT`, so Render/Railway accept them.

**SQL Server: no, not on a free tier.** It needs ~1.25 GB RAM running (image 2.3 GB) and
won't fit free plans (Render free = 512 MB; Railway/Fly are trials, not free). Pick one:

| Path | Free? | Effort | Notes |
|------|-------|--------|-------|
| **Azure for Students** | Yes (with $100 credit + free Azure SQL offer) | Low | Best if your cousin is a student. Keeps SQL Server. |
| **Switch DB to PostgreSQL** (Neon/Supabase free) + free compute (Render) | Yes, truly free | Medium | Requires swapping EF provider to Npgsql + recreating the schema. |
| **Railway / Fly + SQL Server** | ~$5/mo | Low | Simplest, not free. |

Honest recommendation: if he's a student, **Azure for Students** is the cleanest free path
and keeps the stack unchanged. Otherwise, a $5/mo Railway plan is the least work.

## Bugs fixed (these blocked the app from running or deploying)

| # | File | Problem | Fix |
|---|------|---------|-----|
| 1 | `src/.../Contexts/AppDbContextInitializer.cs` | `EnsureCreated`+`Migrate` combo crashed the API on **every restart after the first** (SQL 2714). And the migration scripts are broken from scratch (SQL 206), so `Migrate` can't be used. | Use `EnsureCreatedAsync()` **alone** — builds schema on a fresh DB, safe no-op on restart. |
| 2 | `Presentation/MatchArena.API/Program.cs` | Seeded roles/admin against a DB that was never created (SQL 4060). The initializer existed but was never called. | Call `IAppDbContextInitializer.InitializeDbContext()` before seeding. |
| 3 | `Dockerfile.api`, `Dockerfile.mvc` | Hardcoded port 8080; free hosts inject a dynamic `$PORT` → failed health checks. | Entrypoint binds `http://+:${PORT:-8080}`. |
| 4 | `Presentation/MatchArena.MVC/Program.cs` | API base URL hardcoded to `https://localhost:7246/`. | Read `ApiSettings:BaseUrl` (env-overridable). |
| 5 | `Presentation/MatchArena.MVC/appsettings.json` | `BaseUrl` ended in `/api/` but client calls use the root. | Point at API root `/`. |
| 6 | `.gitignore` | `" appsettings.json"` had a **leading space**, so secrets were never ignored → committed to a public repo. | Proper .NET ignore rules. |

## Deployment files added (in the solution folder, next to MatchArena.sln)

`Dockerfile.api`, `Dockerfile.mvc`, `docker-compose.yml`, `.dockerignore`, `.env.example`

## Apply the fixes to the public repo

```bash
git checkout -b fixes/deploy
git apply matcharena-fixes.patch     # all 6 code fixes + 5 new files
```
Run from the directory containing the top-level `MatchArena/` folder (patch paths start with `MatchArena/`).

## Run locally to verify

```bash
cd MatchArena            # folder with MatchArena.sln
cp .env.example .env     # any strong SA_PASSWORD works locally
docker compose up -d --build
```
- MVC site → http://localhost:8080
- API      → http://localhost:5080   (5000 is taken by macOS AirPlay)

Stop: `docker compose down` (`-v` also wipes the database).

## Deploy publicly (example: Railway)

1. Push fixed code to GitHub.
2. railway.app → New Project → Deploy from GitHub repo.
3. Add a database.
4. API service: Dockerfile Path `MatchArena/Dockerfile.api`, Root Directory `MatchArena`.
   Add env vars in `__` form (`ConnectionStrings__Default`, `JWT__secretKey`, `Stripe__SecretKey`, …). Generate a domain.
5. MVC service: Dockerfile Path `MatchArena/Dockerfile.mvc`, Root Directory `MatchArena`.
   Set `ApiSettings__BaseUrl = https://<api-domain>/` (trailing slash). Generate a domain.

The MVC domain is the live site for the resume.

## ⚠️ SECURITY — do before deploying

The public repo committed real secrets in `Presentation/MatchArena.API/appsettings.json`
(Stripe, Cloudinary, Mailtrap, JWT, admin password).

1. **Rotate all of them.**
2. Stop tracking the file:
   ```bash
   git rm --cached MatchArena/Presentation/MatchArena.API/appsettings.json
   git commit -m "Stop tracking appsettings.json (contains secrets)"
   ```
3. Provide secrets only via environment variables.
