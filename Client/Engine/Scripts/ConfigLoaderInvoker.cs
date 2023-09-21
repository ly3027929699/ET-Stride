using System;
using System.Collections.Generic;
using System.IO;
using Stride.Core.IO;
using Stride.Engine.Design;
using UnityEngine;

namespace ET
{
    class AllStartConfigs
    {
        public static readonly List<string> startConfigs = new List<string>()
        {
            "StartMachineConfigCategory", "StartProcessConfigCategory", "StartSceneConfigCategory", "StartZoneConfigCategory",
        };
    }

    [Invoke]
    public class GetAllConfigBytes: AInvokeHandler<ConfigLoader.GetAllConfigBytes, ETTask<Dictionary<Type, byte[]>>>
    {
        public override async ETTask<Dictionary<Type, byte[]>> Handle(ConfigLoader.GetAllConfigBytes args)
        {
            Dictionary<Type, byte[]> output = new Dictionary<Type, byte[]>();
            HashSet<Type> configTypes = CodeTypes.Instance.GetTypes(typeof (ConfigAttribute));

            foreach (Type type in configTypes)
            {
                string configName = type.Name;
                string configFilePath;
                if (AllStartConfigs.startConfigs.Contains(configName))
                {
                    configFilePath = $"Bundles/Configs/{Options.Instance.StartConfig}/{configName}";
                }
                else
                {
                    configFilePath = $"Bundles/Configs/{configName}";
                }

                output[type] = await ResourcesLoader.Instance.LoadBytes(configFilePath);
            }

            return output;
        }
    }

    [Invoke]
    public class GetOneConfigBytes: AInvokeHandler<ConfigLoader.GetOneConfigBytes, ETTask<byte[]>>
    {
        public override async ETTask<byte[]> Handle(ConfigLoader.GetOneConfigBytes args)
        {
            string configName = args.ConfigName;

            string configFilePath;
            if (AllStartConfigs.startConfigs.Contains(configName))
            {
                configFilePath = $"Bundles/Configs/{Options.Instance.StartConfig}/{configName}";
            }
            else
            {
                configFilePath = $"Bundles/Configs/{configName}";
            }

            return await ResourcesLoader.Instance.LoadBytes(configFilePath);
        }
    }
}