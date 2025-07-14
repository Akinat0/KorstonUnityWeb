
using UnityEngine;
using UnityEngine.EventSystems;

public class ApartmentsScroller : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    public Vector2 PrevTouchPosition { get; set; }
    public Vector2 CurrentTouchPosition { get; set; }
    public bool IsDragging { get; set; }



    public void OnPointerDown(PointerEventData eventData)
    {
        PrevTouchPosition = eventData.position;
        CurrentTouchPosition = PrevTouchPosition;
        IsDragging = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!IsDragging) return;
        
        CurrentTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
    }
}
