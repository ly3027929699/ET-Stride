using Stride.Engine;
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
    
    public class GlobalConfig: ScriptComponent
    {
        //no use
        public CodeMode CodeMode;
        
        //no use
        public BuildType BuildType;

        public AppType AppType;
    }
}