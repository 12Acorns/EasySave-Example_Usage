using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEG.Plugins.EasySave
{
    public readonly struct SVector2
    {
        public SVector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public readonly float x;
        public readonly float y;

        public Vector2 GetVector()
        {
            return new Vector2(x, y);
        }
    }
}
