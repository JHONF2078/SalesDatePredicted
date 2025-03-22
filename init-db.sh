#!/bin/sh

echo "Esperando a que SQL Server esté disponible..."
sleep 30

echo "Ejecutando script SQL..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "Jhon.123" -d master -i /usr/src/app/DBSetup.sql

echo "Script ejecutado correctamente."
