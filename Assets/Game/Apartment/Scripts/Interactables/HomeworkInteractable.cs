
using System;
using Abu.Tools;
using UnityEngine;

public class HomeworkInteractable : InteractableObject
{
    const string Id = "Homework";
    
    public override bool Interact()
    {
        DateTime dateTime = DateTime.Now;

        if (dateTime.DayOfWeek 
            is DayOfWeek.Saturday
            or DayOfWeek.Sunday)
        {
            GameInstance.Messenger.Show("Homework only on weekdays", Color.blue, 5);
            return false;
        }

        if (dateTime.Date == GameInstance.GameState.apartmentLastInteractionTimes.GetOrCreate(Id).Date)
        {
            GameInstance.Messenger.Show("We already had homework today", Color.blue, 5);
            return false;
        }

        GameInstance.GameState.apartmentLastInteractionTimes[Id] = DateTime.Now;
        
        return base.Interact();
    }

    protected override void InteractionCompleted()
    {
        GameInstance.GameState.stars += 1;
        
        base.InteractionCompleted();
    }
}
