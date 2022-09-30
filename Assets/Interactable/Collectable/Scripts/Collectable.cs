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
            base.Interact(runnerCollisionHandler);

            runnerCollisionHandler.RunnerController.AdjustCollectedAmount(runnerCollisionHandler.RunnerController.CollectedCollectables + amount);

            animationController.StopIdleAnimation();
            animationController.PlayCollectionAnimation(runnerCollisionHandler.transform);
        }
    }
}