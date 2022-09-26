using UnityEngine;

namespace Controllers.LevelManager
{
    public class ClearlevelController : MonoBehaviour
    {
        public void ClearLevel(Transform levelHolder)
        {
            
            Destroy(levelHolder.GetChild(0).gameObject);
        }
    }
}