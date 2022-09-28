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

            float pathDistance = (runnerEndTransform.position.z + 20f - (runnerStartTransform.position.z - 20f)) / pathResolution;

            for(int i = 0; i < pathResolution; i++)
            {
                GameObject pathSection = Instantiate(pathSectionPrefab, pathPlacementPosition, Quaternion.identity, pathSectionHolder);
                pathSection.transform.localScale = new Vector3(1f, 1f, pathDistance);

                pathSections.Add(pathSection);

                pathPlacementPosition += Vector3.forward * pathDistance;
            }

            finishLine.transform.position = runnerEndTransform.position + Vector3.up * .02f;

            finishPlatform.position = runnerEndTransform.position + new Vector3(0f, -1.02f, 19.9f);
            finishPlatform.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}