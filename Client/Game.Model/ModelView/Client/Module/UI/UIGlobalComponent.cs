using System.Collections.Generic;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class UIGlobalComponent: Entity, IAwake
    {
        public Dictionary<int, TransformComponent> UILayers = new Dictionary<int, TransformComponent>();
    }
}