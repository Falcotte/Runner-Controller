using UnityEngine;
using DG.Tweening;
using AngryKoala.RunnerControls;

namespace AngryKoala.Collection
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] protected Transform visual;
        [SerializeField] protected CollectableAnimationController animationController;

        [SerializeField] protected Collider collectableCollider;

        [SerializeField] protected int amount;
        public int Amount => amount;

        protected virtual void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent(out RunnerCollisionHandler runnerCollisionHandler);

            if(runnerCollisionHandler)
            {
                Collect(runnerCollisionHandler);
            }
        }

        protected virtual void Collect(RunnerCollisionHandler runnerCollisionHandler)
        {
            runnerCollisionHandler.CollectCollectable(this);

            animationController.StopIdleAnimation();
            animationController.PlayCollectionAnimation();

            collectableCollider.enabled = false;
        }
    }
}