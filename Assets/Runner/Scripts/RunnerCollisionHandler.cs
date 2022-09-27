using UnityEngine;

namespace AngryKoala.RunnerControls
{
    public class RunnerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private RunnerController runnerController;
        public RunnerController RunnerController => runnerController;
    }
}