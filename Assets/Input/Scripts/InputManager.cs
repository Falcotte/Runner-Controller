using System.Collections.Generic;
using UnityEngine;

namespace AngryKoala.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        [SerializeField] private List<InputArea> inputAreas;
        public List<InputArea> InputAreas => inputAreas;

        #region Debug
#if UNITY_EDITOR
        // These values are only added to make debugging inputs easier

        // Different values can be inspected by changing the inputAreaIndex
        [SerializeField] private int inputAreaIndex;

        [SerializeField] private Vector2 touchPos;
        [SerializeField] private float horizontal;
        [SerializeField] private float horizontalRaw;
        [SerializeField] private float vertical;
        [SerializeField] private float verticalRaw;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Vector2 directionRaw;
        [SerializeField] private Vector2 drag;
        [SerializeField] private Vector2 dragDelta;
        [SerializeField] private Vector2 dragDeltaScreenPercent;
        [SerializeField] private bool isTouching;
        [SerializeField] private bool isDragging;

        private void Start()
        {
            ControllerCheck();
        }

        private void Update()
        {
            if(inputAreaIndex >= 0 && inputAreaIndex < inputAreas.Count && inputAreas[inputAreaIndex] != null)
            {
                touchPos = inputAreas[inputAreaIndex].TouchPos;
                horizontal = inputAreas[inputAreaIndex].Horizontal;
                horizontalRaw = inputAreas[inputAreaIndex].HorizontalRaw;
                vertical = inputAreas[inputAreaIndex].Vertical;
                verticalRaw = inputAreas[inputAreaIndex].VerticalRaw;
                direction = inputAreas[inputAreaIndex].Direction;
                directionRaw = inputAreas[inputAreaIndex].DirectionRaw;
                drag = inputAreas[inputAreaIndex].Drag;
                dragDelta = inputAreas[inputAreaIndex].DragDelta;
                dragDeltaScreenPercent = inputAreas[inputAreaIndex].DragDeltaScreenPercent;
                isTouching = inputAreas[inputAreaIndex].IsTouching;
                isDragging = inputAreas[inputAreaIndex].IsDragging;
            }
            else
            {
                Debug.LogError("inputAreaIndex out of bounds");
            }
        }

        private void ControllerCheck()
        {
            for(int i = 0; i < inputAreas.Count; i++)
            {
                if(inputAreas[i] == null)
                {
                    Debug.LogWarning("InputArea[" + i + "] null, are you forgetting something :)");
                }
                else if(inputAreas[i].InputController == null)
                {
                    Debug.LogWarning("TouchpadController not available for InputArea[" + i + "], are you forgetting something :)");
                }
            }
        }
#endif
        #endregion
    }
}