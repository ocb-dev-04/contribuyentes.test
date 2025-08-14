#!/bin/bash
cd ../../../

# Variables
CONTEXT="ContribuyenteDbContext"
STARTUP_PROJECT="src/back/deployables/Web.Api"
PERSISTENCE_PROJECT="src/back/modules/contribuyente.perfiles/Module.Contribuyente.Perfiles.Persistence"
MIGRATION_OUTPUT="Migrations"
MIGRATION_NAME="v_1_0_0_Contribuyentes_Migration"

# Construir migración
echo "→ Construyendo migraciones para $CONTEXT"
dotnet ef migrations add "$MIGRATION_NAME" \
    --context "$CONTEXT" \
    --startup-project "$STARTUP_PROJECT" \
    --project "$PERSISTENCE_PROJECT" \
    -o "$MIGRATION_OUTPUT"
