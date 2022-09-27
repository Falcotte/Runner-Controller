using UnityEngine;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public class Collectable : Interactable
    {
        [SerializeField] protected CollectableAnimationController animationController;

        [SerializeField] protected int amount;
        public int Amount => amount;

        protected override void Interact(RunnerCollisionHandler runnerCollisionHandler)
        {
            runnerCollisionHandler.CollectCollectable(this);

            animationController.StopIdleAnimation();
            animationController.PlayCollectionAnimation();
        }
    }
}