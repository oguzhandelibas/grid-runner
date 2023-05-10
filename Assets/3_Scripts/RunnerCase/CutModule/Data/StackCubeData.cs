using System.Collections.Generic;
using UnityEngine;

namespace GridRunner.Runner.CutModule.Data
{
    [System.Serializable]
    public struct StackCubeData
    {
        public Vector2 MinMaxPushValueX;
        public float StackCubeSpeed;
        public Vector2 SpawnDotsX;
        public List<Material> CubeColors;
    }
}
