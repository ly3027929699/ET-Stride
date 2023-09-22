using Stride.Engine;
using UnityEngine;

namespace ET
{
    public enum SetupMode
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
        public SetupMode SetupMode;
        
        //no use
        public BuildType BuildType;

        public AppType AppType;
    }
}