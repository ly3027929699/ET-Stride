using System;
using Stride.Input;
using Stride.Physics;
using Unity.Mathematics;

namespace ET.Client
{
    [EntitySystemOf(typeof (OperaComponent))]
    [FriendOf(typeof (OperaComponent))]
    public static partial class OperaComponentSystem
    {
        [EntitySystem]
        private static void Awake(this OperaComponent self)
        {
            self.mapMask = CollisionFilterGroupFlags.StaticFilter;
        }

        [EntitySystem]
        private static void Update(this OperaComponent self)
        {
            InputManager input = GlobelEngineScript.Default.GetInputManager();
            if (input.IsMouseButtonReleased(MouseButton.Left))
            {
                Line line = GlobelEngineScript.Default.Camera.ScreenPointToLine(input.MousePosition);
                Simulation simulation = GlobelEngineScript.Default.GetSimulation();
                var result = simulation.Raycast(line.Start, line.End, filterFlags: self.mapMask, hitTriggers: true);
                if (result.Succeeded)
                {
                    C2M_PathfindingResult c2MPathfindingResult = new C2M_PathfindingResult();
                    c2MPathfindingResult.Position = result.Point.ToFloat3();
                    GlobelEngineScript.Default.DebugPoint(result.Point);
                    self.Root().GetComponent<ClientSenderCompnent>().Send(c2MPathfindingResult);
                }
            }

            if (input.IsKeyReleased(Keys.R))
            {
                CodeLoader.Instance.Reload().Coroutine();
                return;
            }

            if (input.IsKeyReleased(Keys.T))
            {
                C2M_TransferMap c2MTransferMap = new();
                self.Root().GetComponent<ClientSenderCompnent>().Call(c2MTransferMap).Coroutine();
            }
        }
    }
}