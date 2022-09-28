using UnityEngine;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public class GateController : Interactable
    {
        [SerializeField] private Gate[] gates;
        public Gate[] Gates => gates;

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

        public void AdjustGateTexts()
        {
            for(int i = 0; i < 2; i++)
            {
                switch(gates[i].ActionType)
                {
                    case Gate.ActionTypes.Add:
                        gates[i].GateText.text = gates[i].Amount > 0 ? $"+{gates[i].Amount}" : $"{gates[i].Amount}";
                        break;
                    case Gate.ActionTypes.Multiply:
                        gates[i].GateText.text = $"x{gates[i].Amount}";
                        break;
                }
            }
        }

        public void SwapGates()
        {
            Gate.ActionTypes tempActionType = gates[0].ActionType;
            int tempAmount = gates[0].Amount;

            gates[0].SetGate(gates[1].ActionType, gates[1].Amount);
            gates[1].SetGate(tempActionType, tempAmount);

            AdjustGateTexts();
        }
    }
}
