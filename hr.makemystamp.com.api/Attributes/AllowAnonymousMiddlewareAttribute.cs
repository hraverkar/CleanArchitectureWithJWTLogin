namespace hr.makemystamp.com.api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited =true, AllowMultiple =false)]
    public class AllowAnonymousMiddlewareAttribute : Attribute
    {
    }
}
