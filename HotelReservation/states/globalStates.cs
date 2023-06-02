using HotelReservation.Models;
public static class GlobalVariables
{
    public static string? StateGlobal { get; set; }
    public static  Usuario? usuario { get; set; }
    public static  Persona? persona { get; set; }

    public static Persona posiblePersona { get; set; }

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
    public static void AgregarPersona(Persona person)
    {
        persona = person;
    }
    public static void EliminarPersona(){
        persona = null;
    }

    public static void AgregarPosiblePersona(Persona person)
    {
        posiblePersona = person;
    }
    public static void EliminarPosiblePersona(){
        posiblePersona = null;
    }
}
