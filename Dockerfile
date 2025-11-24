# 1. Usar la imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 2. Copiar el archivo de proyecto (respetando la carpeta lab13)
COPY ["lab13/lab13.csproj", "lab13/"]

# 3. Restaurar dependencias
RUN dotnet restore "lab13/lab13.csproj"

# 4. Copiar TODO el resto de los archivos
COPY . .

# 5. Entrar a la carpeta del proyecto y publicar
WORKDIR "/src/lab13"
RUN dotnet publish "lab13.csproj" -c Release -o /app/publish

# 6. Imagen final para ejecutar (más ligera)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# --- AQUÍ ESTÁ LA CORRECCIÓN PARA REPORTES ---
# Instalamos libgdiplus para que funcionen los gráficos en Linux
RUN apt-get update && apt-get install -y libgdiplus
# ---------------------------------------------

EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "lab13.dll"]