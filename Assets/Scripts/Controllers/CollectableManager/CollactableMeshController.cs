using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollactableMeshController : MonoBehaviour
    {
        #region Self Variables
        #region Serializefield Variables
        [SerializeField] private MeshFilter collectableMeshFilter;
        #endregion
        #region Private Variables

        [Header("Data")]private MeshData _collectableMeshData;
        #endregion
        #endregion
        
        public void MeshDataInitialize(MeshData dataMeshData)
        {
            _collectableMeshData = dataMeshData;
        }

        public void SetMeshData(MeshType type)
        {
            collectableMeshFilter.mesh = _collectableMeshData.meshdatas[(int)type];
        }
        
    }
    
}