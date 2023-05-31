using HotelReservation.Models;
public static class GlobalVariables
{
    public static string? StateGlobal { get; set; }
    public static  Usuario? usuario { get; set; }
    public static void Agregar(string newState)
    {
        StateGlobal = newState;
    }
    public static void Eliminar(){
        StateGlobal = "";
    }
    public static void AgregarUsuario(Usuario user)
    {
        usuario = user;
    }
    public static void EliminarUsuario(){
        usuario = null;
    }
}
