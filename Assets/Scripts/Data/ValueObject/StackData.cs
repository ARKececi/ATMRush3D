using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        [Range(0.1f, 0.8f)] public float LerpDelay = 0.15f;
    }
}