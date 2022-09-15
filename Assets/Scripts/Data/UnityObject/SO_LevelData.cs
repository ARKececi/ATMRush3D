using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_LevelData", menuName = "Data/SO_LevelData", order = 0)]
    public class SO_LevelData : ScriptableObject
    {
        public List<int> Levels = new List<int>();
    }
}