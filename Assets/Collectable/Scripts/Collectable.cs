using UnityEngine;
using DG.Tweening;
using AngryKoala.RunnerControls;

namespace AngryKoala.Collection
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] protected int amount;
        public int Amount => amount;

        [SerializeField] protected Transform visual;
        [SerializeField] protected Collider collectableCollider;

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

            visual.DOScale(1.2f, .2f).OnComplete(() =>
            {
                visual.DOScale(0f, .2f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
            collectableCollider.enabled = false;
        }
    }
}