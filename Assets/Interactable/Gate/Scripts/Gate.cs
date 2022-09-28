using UnityEngine;
using TMPro;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private TextMeshPro gateText;
        public TextMeshPro GateText => gateText;

        public enum ActionTypes { Add, Multiply };
        [SerializeField] private ActionTypes actionType;
        public ActionTypes ActionType => actionType;

        [SerializeField] private int amount;
        public int Amount => amount;

        public void SetGate(ActionTypes actionType, int amount)
        {
            this.actionType = actionType;
            this.amount = amount;
        }

        public void Action(RunnerController runnerController)
        {
            switch(actionType)
            {
                case ActionTypes.Add:
                    runnerController.AdjustCollectedAmount(runnerController.CollectedCollectables + amount);
                    break;
                case ActionTypes.Multiply:
                    runnerController.AdjustCollectedAmount(runnerController.CollectedCollectables * amount);
                    break;
            }
        }
    }
}