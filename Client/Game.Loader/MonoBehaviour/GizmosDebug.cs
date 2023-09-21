using System.Collections.Generic;
using Stride.Core.Mathematics;
using UnityEngine;

namespace ET
{
    public class GizmosDebug: StartupMonoBehaviour
    {
        public static GizmosDebug Instance { get; private set; }

        public List<Vector3> Path;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDrawGizmos()
        {
            if (this.Path.Count < 2)
            {
                return;
            }
            for (int i = 0; i < Path.Count - 1; ++i)
            {
                // Gizmos.DrawLine(Path[i], Path[i + 1]);
            }
        }
    }
}