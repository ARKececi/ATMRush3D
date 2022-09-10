using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_MeshData", menuName = "Data/SO_MeshData", order = 0)]
    public class SO_MeshData : ScriptableObject
    {
        public MeshData MeshData;
    }
}