using UnityEngine;
using AngryKoala.Inputs;

namespace AngryKoala.RunnerControls
{
    public class RunnerController : MonoBehaviour
    {
        [SerializeField] private Transform visual;

        [SerializeField] private float horizontalSpeed;

        [SerializeField] private float horizontalInputSensitivity;
        [SerializeField] private Vector2 horizontalPositionLimits;

        [SerializeField] private float maxPlayerRotation;

        private float targetXPos;

        private bool isMoving;
        public bool IsMoving => isMoving;

        private void OnEnable()
        {
            InputManager.Instance.InputAreas[0].OnDragDeltaScreenPercent += MoveHorizontal;
        }

        private void OnDisable()
        {
            InputManager.Instance.InputAreas[0].OnDragDeltaScreenPercent -= MoveHorizontal;
        }

        private void Start()
        {
            isMoving = true;
        }

        private void Update()
        {
            if(isMoving)
            {
                visual.localPosition = new Vector3(Mathf.Lerp(visual.localPosition.x, targetXPos, horizontalSpeed * Time.deltaTime), visual.localPosition.y, visual.localPosition.z);
                visual.localRotation = Quaternion.Euler(new Vector3(0f, (visual.localPosition.x - targetXPos) * -maxPlayerRotation / (Mathf.Abs(horizontalPositionLimits.x) + Mathf.Abs(horizontalPositionLimits.y)), 0f));
            }
        }

        private void MoveHorizontal(Vector2 dragDeltaPercent)
        {
            if(isMoving)
            {
                targetXPos += dragDeltaPercent.x * (Mathf.Abs(horizontalPositionLimits.x) + Mathf.Abs(horizontalPositionLimits.y)) * horizontalInputSensitivity;
                targetXPos = Mathf.Clamp(targetXPos, horizontalPositionLimits.x, horizontalPositionLimits.y);
            }
        }
    }
}