using UnityEngine;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected Collider interactableCollider;

        protected virtual void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent(out RunnerCollisionHandler runnerCollisionHandler);

            if(runnerCollisionHandler)
            {
                Interact(runnerCollisionHandler);
            }
        }

        protected virtual void Interact(RunnerCollisionHandler runnerCollisionHandler)
        {
            interactableCollider.enabled = false;
        }
    }
}