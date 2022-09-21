using UnityEngine;

namespace AngryKoala.RunnerControls
{
    public class RunnerLevel : MonoSingleton<RunnerLevel>
    {
        [SerializeField] private Transform runnerStartTransform;
        public Transform RunnerStartTransform => runnerStartTransform;

        [SerializeField] private Transform runnerEndTransform;
        public Transform RunnerEndTransform => runnerEndTransform;
    }
}