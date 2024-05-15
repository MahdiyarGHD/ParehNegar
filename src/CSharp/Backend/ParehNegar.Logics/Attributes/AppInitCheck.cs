namespace ParehNegar.Logics.Attributes;

public class AppInitCheckAttribute : CustomAuthorizeCheckAttribute
{
    public AppInitCheckAttribute() : base("CurrentLanguage")
    {
    }
}