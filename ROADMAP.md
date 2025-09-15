# Fintrack

Assistente financeiro modular em .NET, inspirado em **DDD + Clean Architecture**, começando como **monólito modular** com módulos **Identity** e **Ledger**.

---

## 📌 Roadmap

### ✅ v1.0

**Técnico**

-   [x] Arquitetura monólito modular (Identity, Ledger)
-   [x] Configuração de DI para os módulos
-   [x] API inicial com HealthChecks e Swagger
-   [x] Observabilidade: Serilog estruturado + correlação
-   [x] Testes unitários e integração mínimos
-   [x] CI/CD: build + testes + semver

---

### ⏳ v1.1

**Features**

-   [ ] Registro e login (`/api/users/register`, `/api/users/login`)
-   [ ] Criar movimento (`POST /api/movements`)

**Técnico**

-   [ ] Auth: JWT simples (sem refresh)
-   [ ] Adição das primeiras entidades em Identity e Ledger

---

### ⏳ v1.2

**Features**

-   [ ] Listar movimentos do mês (`GET /api/movements`)
-   [ ] Detalhar movimento (`GET /api/movements/{id}`)
-   [ ] Excluir movimento (`DELETE /api/movements/{id}`)

**Técnico**

-   [ ] Paginação simples em `GET /api/movements`
-   [ ] Validação leve (pipeline behavior)
-   [ ] Idempotência básica no `DELETE`
-   [ ] Logs de auditoria

---

### ⏳ v1.3

**Features**

-   [ ] Atualizar movimento (`PUT /api/movements/{id}`)
-   [ ] Resumo mensal (`GET /api/summary/monthly`)

**Técnico**

-   [ ] Cálculos otimizados no banco (SUM filtrado)
-   [ ] Índices (`occurred_on`, `user_id`)
-   [ ] Cache curto por usuário/mês
-   [ ] Concorrência otimista no `PUT` (ETag/rowversion simples)

---

### ⏳ v1.4 — Bot Telegram (básico)

**Features**

-   [ ] `/add` → cria movimento
-   [ ] `/list` → lista movimentos do mês
-   [ ] `/saldo` → resumo do mês

**Técnico**

-   [ ] Long Polling (mais simples para começar)
-   [ ] Vínculo `telegram_user_id → user_id`
-   [ ] Comandos de mensagem única (sem estado)
-   [ ] Token do bot em secret + rate limit básico
-   [ ] Logs do bot em sink separado

---

### ⏳ v1.5 — Bot produção-ready (opcional)

**Técnico**

-   [ ] Migrar para **Webhook** (menor latência, escala)
-   [ ] Infra HTTPS pública + certificado
-   [ ] Resiliência: retry/idempotência por `update_id`
-   [ ] Métricas de bot (contagem de comandos, erros)

---

## 🔮 Próximos (possíveis v2/v3)

**Produto**

-   [ ] Orçamentos (metas de gasto)
-   [ ] Categorias
-   [ ] Contas (carteira, banco, cartão)
-   [ ] Importação CSV/OFX
-   [ ] Relatórios avançados (comparativos, projeções)
-   [ ] Compartilhamento (multiusuário/família)
-   [ ] Insights inteligentes (alertas, recomendações)
-   [ ] Open Banking (integrações automáticas)

**Técnico**

-   [ ] **RLS (Row-Level Security) com GUC `app.user_id`**
-   [ ] Refresh tokens, MFA, login social
-   [ ] Feature flags
-   [ ] Background jobs (reprocessamento de importações)
-   [ ] Observabilidade+: métricas (Prometheus/OpenTelemetry), tracing distribuído
-   [ ] Escalabilidade: extrair módulos em serviços (ou Aspire)
-   [ ] Cache/Redis (para bot e preferências)
-   [ ] Hardening de DB e privilégios padrão
