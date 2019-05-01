using System.Collections.Generic;

public class Root
{
    private static Dictionary<System.Type, System.Object> singletons;

    public static T GetInstance<T>() where T : new()
    {
        if (!singletons.ContainsKey(typeof(T)))
        {
            singletons.Add(typeof(T), new T());
        }
        return (T) singletons[typeof(T)];
    }
}
