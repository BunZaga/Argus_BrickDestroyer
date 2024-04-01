using UnityEngine;
using UnityEngine.EventSystems;

public class AimShooterDragZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    public void OnPointerDown(PointerEventData pointerEventData)
    {
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (!GameGrid.GameInPlay)
            return;
        
        AimInputControl.evtOnMouseUp.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
    }
}
