using System;
using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class CellSelectionHandler : MonoBehaviour
    {
        [SerializeField] OnSwipeInput onSwipeInput;
        [SerializeField] SwipedCellData swipedCellData;
        [SerializeField] Grid grid;
        Camera cam;

        void Awake() => cam = Camera.main;

        void OnEnable() => onSwipeInput.AddListener(HandleSwipe, 0);

        void OnDisable() => onSwipeInput.RemoveListener(HandleSwipe);

        void HandleSwipe(SwipeData swipeData)
        {
            var worldPos = cam.ScreenToWorldPoint(new Vector3(swipeData.touchStartPos.x, swipeData.touchStartPos.y, 0));
            var cellUnderCursor = grid.WorldToCell(worldPos);
            swipedCellData.SetCellPos(this, cellUnderCursor);
            Debug.Log("cell selction");
        }
    }
}