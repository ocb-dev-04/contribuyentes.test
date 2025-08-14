#!/bin/bash
cd ../../../

# Variables
CONTEXT="ComprobanteDbContext"
STARTUP_PROJECT="src/back/deployables/Web.Api"
PERSISTENCE_PROJECT="src/back/modules/comprobante.registros/Modulo.Comprobante.Registros.Persistence"
OUTPUT_DIR="CompiledEntities"

echo --> Pre-compilando entidades relacionada a $CONTEXT

# Limpiar carpeta de compilación previa
rm -rf "$PERSISTENCE_PROJECT/$OUTPUT_DIR"/*

# Ejecutar precompilación
dotnet ef dbcontext optimize \
    --context "$CONTEXT" \
    --startup-project "$STARTUP_PROJECT" \
    --project "$PERSISTENCE_PROJECT" \
    --output-dir "$OUTPUT_DIR"