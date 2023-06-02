<h1 align="center">HotelReservationPF</h1>

<p align="center">
  Proyecto final de la Universidad Privada Domingo Savio.
</p>

## 📦 Paquetes necesarios:
- Microsoft.EntityFrameworkCore
- SqlServer
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Jwt "Generador de Tokens"

## ⚙️ Comandos de consola para la migración Windows:
- add-migration "Migracion"
- update-database
- remove migration "en caso de fallos"

## ⚙️ Comandos de consola de migración en Linux:
- dotnet ef migrations add "nombre migracion"
- dotnet ef database update

### Comandos extras en Linux:
- dotnet ef clean "comando para limpiar proyecto"
- dotnet ef build "comando necesario para buildear despues de limpiar"
