using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace AngryKoala.Inputs
{
    public class InputController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private InputArea inputArea;

        private Vector2 inputVector = Vector2.zero;

        private float horizontal { get { return inputVector.x; } }
        private float vertical { get { return inputVector.y; } }
        private Vector2 direction { get { return new Vector2(horizontal, vertical); } }

        private Vector2 drag;
        private Vector2 dragDelta;

        private bool isTouching;
        private bool isDragging;

        private Vector2 initialTouchPosition;
        private Vector2 touchPosition;

        private void Update()
        {
            if(isTouching)
            {
                dragDelta = CurrentTouchPosition() - touchPosition;
                inputArea.SetDragDelta(dragDelta);

                touchPosition = CurrentTouchPosition();
                inputArea.SetTouchPos(touchPosition);

                inputArea.OnTouch?.Invoke();
            }
        }

        #region Touch

        public void OnPointerDown(PointerEventData eventData)
        {
            if(inputArea != null)
            {
                isTouching = true;
                inputArea.SetIsTouching(true);

                initialTouchPosition = eventData.position;
                touchPosition = initialTouchPosition;

                inputArea.OnTouchDown?.Invoke();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(inputArea != null)
            {
                isTouching = false;
                inputArea.SetIsTouching(false);

                inputArea.OnTouchUp?.Invoke();

                initialTouchPosition = Vector2.zero;
                touchPosition = Vector2.zero;
                inputArea.SetTouchPos(touchPosition);

                inputVector = Vector2.zero;

                HandleAxes();
            }
        }

#if UNITY_EDITOR
        private Vector2 CurrentTouchPosition()
        {
            if(inputArea != null)
            {
                if(isTouching)
                {
                    return Input.mousePosition;
                }
                else
                {
                    return Vector2.zero;
                }
            }
            return Vector2.zero;
        }
#else
        private Vector2 CurrentTouchPosition()
        {
            if(inputArea != null)
            {
                if(Input.touchCount > 0)
                {
                    return Input.touches[0].position;
                }
            }
            return Vector2.zero;
        }
#endif

        #endregion

        #region Drag

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if(inputArea != null)
            {
                isDragging = true;
                inputArea.SetIsDragging(true);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(inputArea != null)
            {
                drag = (eventData.position - initialTouchPosition);
                dragDelta = (eventData.position - touchPosition);

                inputArea.SetDrag(drag);
                inputArea.SetDragDelta(dragDelta);

                inputArea.OnDragDeltaScreenPercent?.Invoke(inputArea.DragDeltaScreenPercent);

                Vector2 direction = drag;

                inputVector = direction;

                HandleAxes();
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if(inputArea != null)
            {
                isDragging = false;
                inputArea.SetIsDragging(false);

                drag = Vector2.zero;
                dragDelta = Vector2.zero;

                inputArea.SetDrag(drag);
                inputArea.SetDragDelta(dragDelta);
            }
        }

        #endregion

        private void HandleAxes()
        {
            inputArea.SetHorizontal(inputVector.x);
            inputArea.SetVertical(inputVector.y);
            inputArea.SetDirection(inputVector);
        }
    }
}