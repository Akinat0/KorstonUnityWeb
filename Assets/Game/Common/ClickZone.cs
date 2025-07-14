using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ClickZone : MonoBehaviour
{
    [SerializeField] UnityEvent onMouseDownEvent;

    public UnityEvent OnMouseDownEvent => onMouseDownEvent;
    
    void OnMouseDown()
    {
        onMouseDownEvent?.Invoke();
    }
}
