using UnityEngine;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public class GateController : Interactable
    {
        [SerializeField] private Gate[] gates;

        protected override void Interact(RunnerCollisionHandler runnerCollisionHandler)
        {
            base.Interact(runnerCollisionHandler);

            if(runnerCollisionHandler.transform.position.x < transform.position.x)
            {
                gates[0].Action(runnerCollisionHandler.RunnerController);
            }
            else
            {
                gates[1].Action(runnerCollisionHandler.RunnerController);
            }
        }
    }
}
