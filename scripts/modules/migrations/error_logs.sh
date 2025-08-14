#!/bin/bash
cd ../../../

# Variables
CONTEXT="ErrorLogDbContext"
STARTUP_PROJECT="src/back/deployables/Web.Api"
PERSISTENCE_PROJECT="src/back/modules/internal.error_logs/Module.Internal.Error.Logs.Persistence"
MIGRATION_OUTPUT="Migrations"
MIGRATION_NAME="v_1_0_0_ErrorLogs_Migration"

# Construir migración
echo "→ Construyendo migraciones para $CONTEXT"
dotnet ef migrations add "$MIGRATION_NAME" \
    --context "$CONTEXT" \
    --startup-project "$STARTUP_PROJECT" \
    --project "$PERSISTENCE_PROJECT" \
    -o "$MIGRATION_OUTPUT"
