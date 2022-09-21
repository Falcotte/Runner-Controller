using UnityEngine;
using AngryKoala.Inputs;

namespace AngryKoala.RunnerControls
{
    public class RunnerController : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        [SerializeField] private Animator playerAnimator;

        [SerializeField] private float forwardSpeed;    // Percentage of distance traveled per second
        [SerializeField] private float horizontalSpeed;

        [SerializeField] private float horizontalInputSensitivity;
        [SerializeField] private Vector2 horizontalPositionLimits;

        [SerializeField] private float maxPlayerRotation;

        private float currentTraveledDistance;

        private float targetXPos;

        private bool isMoving;
        public bool IsMoving => isMoving;

        private bool hasReachedEnd;

        private void OnEnable()
        {
            InputManager.Instance.InputAreas[0].OnTouch += StartMovement;
            InputManager.Instance.InputAreas[0].OnDragDeltaScreenPercent += MoveHorizontal;
        }

        private void OnDisable()
        {
            InputManager.Instance.InputAreas[0].OnTouch -= StartMovement;
            InputManager.Instance.InputAreas[0].OnDragDeltaScreenPercent -= MoveHorizontal;
        }

        private void Update()
        {
            if(isMoving)
            {
                currentTraveledDistance += forwardSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(RunnerLevel.Instance.RunnerStartTransform.position, RunnerLevel.Instance.RunnerEndTransform.position, currentTraveledDistance / 100f);

                if(currentTraveledDistance >= 100f)
                {
                    currentTraveledDistance = 100f;
                    StopMovement();
                }

                visual.localPosition = new Vector3(Mathf.Lerp(visual.localPosition.x, targetXPos, horizontalSpeed * Time.deltaTime), visual.localPosition.y, visual.localPosition.z);
                visual.localRotation = Quaternion.Euler(new Vector3(0f, (visual.localPosition.x - targetXPos) * -maxPlayerRotation / (Mathf.Abs(horizontalPositionLimits.x) + Mathf.Abs(horizontalPositionLimits.y)), 0f));

                playerAnimator.SetBool("IsMoving", isMoving);
            }
        }

        private void StartMovement()
        {
            if(!hasReachedEnd)
            {
                isMoving = true;
            }
        }

        private void StopMovement()
        {
            isMoving = false;
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