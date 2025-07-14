using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent onMouseDownEvent;
    [SerializeField] ApartmentsController controller;
    [SerializeField] float interactionTime = 3;
    [SerializeField] Transform interactionPoint;

    public Vector3 InteractionPosition => interactionPoint ? interactionPoint.position : transform.position;
    
    public UnityEvent OnMouseDownEvent => onMouseDownEvent;
    
    void OnMouseDown()
    {
        if (!controller.CurrentInteractable)
            controller.TargetInteractable = this;
    }

    public virtual bool Interact()
    {
        onMouseDownEvent?.Invoke();
        StartCoroutine(InteractionRoutine());
        return true;
    }

    protected virtual void InteractionCompleted()
    {
        GameInstance.SaveState();
        controller.CurrentInteractable = null;
    }

    IEnumerator InteractionRoutine()
    {
        float time = 0;
        
        while (time < interactionTime)
        {
            time += Time.deltaTime;
        
            float phase = time / interactionTime;

            float sin = Mathf.Sin(time * 5);
            
            transform.localScale = Vector3.one * (1 + sin * sin * 0.2f);
            yield return null;
        }

        transform.localScale = Vector3.one;
        controller.TargetInteractable = null;
        
        InteractionCompleted();
    }
}