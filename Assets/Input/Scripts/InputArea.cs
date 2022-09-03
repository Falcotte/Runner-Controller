using UnityEngine;
using UnityEngine.Events;

namespace AngryKoala.Inputs
{
    /// <summary>
    /// Values here are assigned by the InputController, can be accessed when needed through InputManager.Instance.InputAreas[index]
    /// Events are also triggered by the InputController
    /// </summary>
    public class InputArea : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        public InputController InputController => inputController;

        private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;
        private Vector2 inputAreaBoundaryMin;
        public Vector2 InputAreaBoundaryMin => inputAreaBoundaryMin;
        private Vector2 inputAreaBoundaryMax;
        public Vector2 InputAreaBoundaryMax => inputAreaBoundaryMax;
        private float inputAreaWidth;
        public float InputAreaWidth => inputAreaWidth;
        private float inputAreaHeight;
        public float InputAreaHeight => inputAreaHeight;

        #region Attributes

        public Vector2 TouchPos { get; private set; }
        public void SetTouchPos(Vector2 value) { TouchPos = value; }

        public float Horizontal { get; private set; }
        public void SetHorizontal(float value) { Horizontal = value; }
        public float HorizontalRaw { get { return Horizontal > 0 ? 1 : Horizontal < 0 ? -1 : 0; } }
        public float Vertical { get; private set; }
        public void SetVertical(float value) { Vertical = value; }
        public float VerticalRaw { get { return Vertical > 0 ? 1 : Vertical < 0 ? -1 : 0; } }
        public Vector2 Direction { get; private set; }
        public void SetDirection(Vector2 value) { Direction = value; }
        public Vector2 DirectionRaw { get { return Direction.normalized; } }

        public Vector2 Drag { get; private set; }
        public void SetDrag(Vector2 value) { Drag = value; }

        public Vector2 DragScreenPercent { get { return new Vector2(Drag.x / Screen.width, Drag.y / Screen.height); } }

        public Vector2 DragDelta { get; private set; }
        public void SetDragDelta(Vector2 value) { DragDelta = value; }

        public Vector2 DragDeltaScreenPercent { get { return new Vector2(DragDelta.x / Screen.width, DragDelta.y / Screen.height); } }

        public bool IsTouching { get; private set; }
        public void SetIsTouching(bool value) { IsTouching = value; }
        public bool IsDragging { get; private set; }
        public void SetIsDragging(bool value) { IsDragging = value; }

        #endregion

        #region Events

        public UnityAction OnTouchDown;
        public UnityAction OnTouchUp;
        public UnityAction OnTouch;

        public UnityAction<Vector2> OnDragDeltaScreenPercent;

        #endregion

        private void Start()
        {
            SetInputArea();
        }

        // InputArea's dimensions are adjusted to the screen size here
        private void SetInputArea()
        {
            rectTransform = GetComponent<RectTransform>();

            inputAreaBoundaryMin = new Vector2(rectTransform.anchorMin.x * Screen.width, rectTransform.anchorMin.y * Screen.height);
            inputAreaBoundaryMax = new Vector2(rectTransform.anchorMax.x * Screen.width, rectTransform.anchorMax.y * Screen.height);

            inputAreaWidth = (rectTransform.anchorMax.x - rectTransform.anchorMin.x) * Screen.width;
            inputAreaHeight = (rectTransform.anchorMax.y - rectTransform.anchorMin.y) * Screen.height;
        }

        public void ResetAttributes()
        {
            TouchPos = Vector2.zero;
            Horizontal = 0f;
            Vertical = 0f;
            Direction = Vector2.zero;
            Drag = Vector2.zero;
            DragDelta = Vector2.zero;
            IsTouching = false;
            IsDragging = false;
        }
    }
}