using UnityEngine;

namespace Controllers.LevelManager
{
    public class LevelLoaderController : MonoBehaviour
    {
        public void LoaderLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level{_levelID}"), levelHolder);
        }
    }
}