using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollactableMeshController collactableMeshController;

        #endregion

        #region Public Variables

        [Header("Data")] public MeshData MeshData;
        
        public MeshType MeshTypeValue
        {
            get => _collectableType;
            private set
            {
            
                _collectableType = value;
                SendCollectableMeshDataToMeshController();
                //StackSignals.Instance.onUpdateType?.Invoke();
            }
        }

        #endregion

        #region Private Variables

        private MeshType _collectableType;

        #endregion

        #endregion
        
        private void Awake()
        {
            MeshData = GetMeshData();
            MeshDataInitializeToMeshController();
        }

        private MeshData GetMeshData() =>
            Resources.Load<SO_MeshData>("Data/SO_MeshData").MeshData;
        
        private void MeshDataInitializeToMeshController()
        {
            collactableMeshController.MeshDataInitialize(MeshData);
        }

        private void SendCollectableMeshDataToMeshController()
        {
            ScoreSignals.Instance.onPlayerScoreDistributing?.Invoke(transform.gameObject);
            collactableMeshController.SetMeshData(MeshTypeValue);
            ScoreSignals.Instance.onPlayerScoreCalculation?.Invoke(transform.gameObject);
        }
        
        public void MeshUpdater()
        {
            if ((int)MeshTypeValue < 2)
            {
                MeshTypeValue++;
            }
        }
    }
}