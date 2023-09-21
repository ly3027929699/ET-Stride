using Stride.Engine;

namespace UnityEngine;

public abstract class Object
{
    
}
public abstract class MonoBehaviour: ScriptComponent
{
    public static void DontDestroyOnLoad(Entity entity)
    {
        
    }
}
public abstract class UpdateMonoBehaviour: SyncScript
{
    public static void DontDestroyOnLoad(Entity entity)
    {
        
    }
}
public abstract class StartupMonoBehaviour: StartupScript
{
    public static void DontDestroyOnLoad(Entity entity)
    {
        
    }
}

public enum LoadSceneMode
{
    Single
}