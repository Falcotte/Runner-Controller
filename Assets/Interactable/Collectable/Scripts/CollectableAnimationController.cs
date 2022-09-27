using UnityEngine;
using DG.Tweening;

namespace AngryKoala.Interaction
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] protected Transform visual;

        [Header("Idle")]
        [SerializeField] protected float verticalMovementAmount;
        [SerializeField] protected float verticalMovementDuration;
        [SerializeField] protected float rotationSpeed;
        [SerializeField] protected float scaleAmount;
        [SerializeField] protected float scaleDuration;

        [Header("Collection")]
        [SerializeField] protected float collectionScaleAmount;
        [SerializeField] protected float collectionDuration;

        private void Start()
        {
            PlayIdleAnimation();
        }

        protected virtual void PlayIdleAnimation()
        {
            visual.DOBlendableLocalMoveBy(Vector3.up * verticalMovementAmount, verticalMovementDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            visual.DOBlendableLocalRotateBy(Vector3.up, rotationSpeed, RotateMode.Fast).SetSpeedBased(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            visual.DOBlendableScaleBy(Vector3.one * scaleAmount, scaleDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        public virtual void StopIdleAnimation()
        {
            DOTween.Kill(visual);
        }

        public virtual void PlayCollectionAnimation()
        {
            visual.DOScale(collectionScaleAmount, collectionDuration / 2f).OnComplete(() =>
              {
                  visual.DOScale(0f, collectionDuration / 2f).OnComplete(() =>
                  {
                      gameObject.SetActive(false);
                  });
              });
        }
    }
}
