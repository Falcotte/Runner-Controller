using UnityEngine;
using DG.Tweening;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public class Obstacle : Interactable
    {
        [SerializeField] protected Transform visual;

        protected override void Interact(RunnerCollisionHandler runnerCollisionHandler)
        {
            base.Interact(runnerCollisionHandler);

            runnerCollisionHandler.RunnerController.HitObstacle();

            HideObstacle();
        }

        protected void HideObstacle()
        {
            visual.DOScale(0f, .3f);
        }
    }
}
