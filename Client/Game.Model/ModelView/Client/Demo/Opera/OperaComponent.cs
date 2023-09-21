using System;
using System.Numerics;
using Stride.Physics;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(Scene))]
	public class OperaComponent: Entity, IAwake, IUpdate
    {
        public Vector3 ClickPoint;
	    public CollisionFilterGroupFlags mapMask;
    }
}
