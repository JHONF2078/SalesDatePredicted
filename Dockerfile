# =============================
# 1️⃣ Imagen base para la ejecución
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# =============================
# 2️⃣ Imagen para la compilación y publicación
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar la solución
COPY ["DataPrediction.sln", "./"]

# Copiar proyectos individualmente para aprovechar caché
COPY ["src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj", "src/SalesDatePrediction.Api/"]
COPY ["src/SalesDatePrediction.Application/SalesDatePrediction.Application.csproj", "src/SalesDatePrediction.Application/"]
COPY ["src/SalesDatePrediction.Domain/SalesDatePrediction.Domain.csproj", "src/SalesDatePrediction.Domain/"]
COPY ["src/SalesDatePrediction.Infrastructure/SalesDatePrediction.Infrastructure.csproj", "src/SalesDatePrediction.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj"

# Copiar todo el código fuente después de restaurar dependencias
COPY src/ src/

# ✅ Especificar archivo de proyecto directamente
RUN dotnet build "src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj" -c Release -o /app/build

# =============================
# 3️⃣ Publicar la aplicación
# =============================
FROM build AS publish
RUN dotnet publish "src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj" -c Release -o /app/publish

# =============================
# 4️⃣ Imagen final para ejecución
# =============================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SalesDatePrediction.Api.dll"]
