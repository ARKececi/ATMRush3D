using System;
using Keys;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPyhsicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                StackSignals.Instance.onStackAdd?.Invoke(new StackObjectParams()
                {
                    other = other.gameObject.transform.parent.gameObject
                });
            }
        }
    }
}