using UnityEngine;
using TMPro;
using AngryKoala.RunnerControls;

namespace AngryKoala.Interaction
{
    public class Gate : MonoBehaviour
    {
        public enum ActionTypes { Add, Multiply };
        [SerializeField] private ActionTypes actionType;
        public ActionTypes ActionType => actionType;

        [SerializeField] private TextMeshPro gateText;

        [SerializeField] private int amount;
        public int Amount => amount;

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