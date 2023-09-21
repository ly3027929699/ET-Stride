using UnityEngine;

namespace ET
{
    public enum CodeMode
    {
        Client = 1,
        Server = 2,
        ClientServer = 3,
    }
    
    public enum BuildType
    {
        None,
        Debug,
        Release,
    }
    
    public class GlobalConfig
    {
        public CodeMode CodeMode;
        
        public BuildType BuildType;

        public AppType AppType;
    }
}