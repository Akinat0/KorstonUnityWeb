
using System;
using Abu.Tools;
using UnityEngine;

public class CleanInteractable : InteractableObject
{
    const string Id = "Clean";
    
    public override bool Interact()
    {
        DateTime dateTime = DateTime.Now;

        if (dateTime.DayOfWeek
            is DayOfWeek.Saturday
            or DayOfWeek.Sunday)
        {
            
            if (dateTime.Date == GameInstance.GameState.apartmentLastInteractionTimes.GetOrCreate(Id).Date)
            {
                GameInstance.Messenger.Show("We already cleaned up today", Color.blue, 5);
                return false;
            }

            GameInstance.GameState.apartmentLastInteractionTimes[Id] = DateTime.Now;

            return base.Interact();
        }
        
        GameInstance.Messenger.Show("Clean up  only on weekends", Color.blue, 5);
        return false;
    }

    protected override void InteractionCompleted()
    {
        GameInstance.GameState.stars += 1;
        
        base.InteractionCompleted();
    }
}
