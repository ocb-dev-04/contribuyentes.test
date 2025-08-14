#!/bin/bash
cd ../../../

# Variables
CONTEXT="ComprobanteDbContext"
STARTUP_PROJECT="src/back/deployables/Web.Api"
PERSISTENCE_PROJECT="src/back/modules/comprobante.registros/Modulo.Comprobante.Registros.Persistence"
MIGRATION_OUTPUT="Migrations"
MIGRATION_NAME="v_1_0_0_Comprobantes_Migration"

# Construir migración
echo "→ Construyendo migraciones para $CONTEXT"
dotnet ef migrations add "$MIGRATION_NAME" \
    --context "$CONTEXT" \
    --startup-project "$STARTUP_PROJECT" \
    --project "$PERSISTENCE_PROJECT" \
    -o "$MIGRATION_OUTPUT"
