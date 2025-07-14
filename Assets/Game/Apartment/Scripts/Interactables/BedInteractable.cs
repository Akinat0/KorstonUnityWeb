
using System;
using Abu.Tools;
using UnityEngine;

public class BedInteractable : InteractableObject
{
    const string Id = "Bed";
    
    public override bool Interact()
    {
        DateTime dateTime = DateTime.Now;
        
        if (dateTime.TimeOfDay > new TimeSpan(4, 0, 0) && dateTime.TimeOfDay < new TimeSpan(12, 0, 0))
        {
            GameInstance.Messenger.Show("Bed can be done from 4 to 12", Color.blue, 5);
            return false;
        }
        
        if (dateTime.Date == GameInstance.GameState.apartmentLastInteractionTimes.GetOrCreate(Id).Date)
        {
            GameInstance.Messenger.Show("Today we already did stuff with bed", Color.blue, 5);
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
