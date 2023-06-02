# HotelReservationPF
Proyecto final Universidad Privada Domingo Savio
Paquetes necesarios:
  *Microsoft.EntityFrameworkCore
  *SqlServer
  *Microsoft.VisualStudio.Web.CodeGeneration.Design
  *Jwt "Generador de Tokkens"
# Comandos de consola para la migración:
  *add-migration "Migracion"
  *update-database
  *remove migration "en caso de fallos"
# Comandos de consola de migración Linux:
   *dotnet ef migrations add "nombre migracion"
   *dotnet ef database update
      * Comandos extras Linux
        *dotnet ef clean "comando para limpiar proyecto"
        *dotnet ef build "comando necesario para buildear despues de limpiar"
        
