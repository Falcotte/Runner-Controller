using UnityEngine;
using AngryKoala.Collection;

namespace AngryKoala.RunnerControls
{
    public class RunnerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private RunnerController runnerController;

        public void CollectCollectable(Collectable collectable)
        {
            runnerController.CollectCollectable(collectable.Amount);
        }
    }
}