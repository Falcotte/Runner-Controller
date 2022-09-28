using UnityEngine;
using DG.Tweening;
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

        [SerializeField] private float obstaclePushBackDistance;
        [SerializeField] private float obstaclePushBackDuration;

        private float currentTraveledDistance;

        private float targetXPos;

        private bool isMoving;
        public bool IsMoving => isMoving;

        private bool runnerStarted;

        private int collectedCollectables;
        public int CollectedCollectables => collectedCollectables;

        private void OnEnable()
        {
            InputManager.Instance.InputAreas[0].OnTouchDown += StartRunner;
            InputManager.Instance.InputAreas[0].OnDragDeltaScreenPercent += MoveHorizontal;
            InputManager.Instance.InputAreas[0].OnTouchUp += ResetTargetXPos;
        }

        private void OnDisable()
        {
            InputManager.Instance.InputAreas[0].OnTouchDown -= StartRunner;
            InputManager.Instance.InputAreas[0].OnDragDeltaScreenPercent -= MoveHorizontal;
            InputManager.Instance.InputAreas[0].OnTouchUp -= ResetTargetXPos;
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
                visual.localRotation = Quaternion.Euler(new Vector3(0f, Mathf.Clamp((visual.localPosition.x - targetXPos) * -maxPlayerRotation / ((Mathf.Abs(horizontalPositionLimits.x) + Mathf.Abs(horizontalPositionLimits.y) / 2f)), -maxPlayerRotation, maxPlayerRotation), 0f));

                playerAnimator.SetBool("IsMoving", isMoving);
            }
        }

        #region Movement

        private void StartRunner()
        {
            if(!runnerStarted)
            {
                runnerStarted = true;
                StartMovement();
            }
        }

        private void StartMovement()
        {
            isMoving = true;
        }

        private void StopMovement()
        {
            ResetTargetXPos();

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

        private void ResetTargetXPos()
        {
            if(isMoving)
            {
                targetXPos = Mathf.Lerp(targetXPos, visual.localPosition.x, .8f);
            }
        }

        #endregion

        public void AdjustCollectedAmount(int amount)
        {
            collectedCollectables = amount;
            collectedCollectables = Mathf.Max(0, amount);

            UIManager.Instance.UpdateCollectableText(collectedCollectables);
        }

        public void HitObstacle()
        {
            StopMovement();

            Sequence hitObstacleSequence = DOTween.Sequence();
            hitObstacleSequence.AppendCallback(() =>
            {
                playerAnimator.SetTrigger("HitObstacle");
                visual.DOLocalRotate(Vector3.zero, .25f);
            });
            hitObstacleSequence.Append(DOTween.To(() => currentTraveledDistance, x => currentTraveledDistance = x, currentTraveledDistance - obstaclePushBackDistance, obstaclePushBackDuration).OnUpdate(() =>
            {
                transform.position = Vector3.Lerp(RunnerLevel.Instance.RunnerStartTransform.position, RunnerLevel.Instance.RunnerEndTransform.position, currentTraveledDistance / 100f);
            }));
            hitObstacleSequence.AppendCallback(() =>
            {
                ResetTargetXPos();
                StartMovement();
            });
        }
    }
}