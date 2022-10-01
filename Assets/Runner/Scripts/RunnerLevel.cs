using System.Collections.Generic;
using UnityEngine;

namespace AngryKoala.RunnerControls
{
    public class RunnerLevel : MonoSingleton<RunnerLevel>
    {
        [SerializeField] private Transform runnerStartTransform;
        public Transform RunnerStartTransform => runnerStartTransform;

        [SerializeField] private Transform runnerEndTransform;
        public Transform RunnerEndTransform => runnerEndTransform;

        [SerializeField] private Transform finishPlatformCenter;
        public Transform FinishPlatformCenter => finishPlatformCenter;

        [SerializeField] private Transform pathSectionHolder;

        [SerializeField] private GameObject pathSectionPrefab;
        [SerializeField] private int pathResolution;

        [SerializeField] private Transform finishPlatform;
        [SerializeField] private GameObject finishLine;

        [SerializeField] private List<GameObject> pathSections = new List<GameObject>();

        public void PlacePathSections()
        {
            foreach(var pathSection in pathSections)
            {
                DestroyImmediate(pathSection.gameObject);
            }

            pathSections.Clear();

            Vector3 pathPlacementPosition = runnerStartTransform.position - Vector3.forward * 20f;

            float pathDistance = (runnerEndTransform.position.z + 10f - (runnerStartTransform.position.z - 20f)) / pathResolution;

            for(int i = 0; i < pathResolution; i++)
            {

                pathPlacementPosition += i == 0 ? Vector3.forward * pathDistance / 2f : Vector3.forward * pathDistance;

                GameObject pathSection = Instantiate(pathSectionPrefab, pathPlacementPosition, Quaternion.identity, pathSectionHolder);
                pathSection.transform.localScale = new Vector3(1f, 1f, pathDistance);

                pathSections.Add(pathSection);
            }

            finishLine.transform.position = runnerEndTransform.position + Vector3.up * .02f;

            finishPlatform.position = runnerEndTransform.position + Vector3.forward * 10f;
        }
    }
}