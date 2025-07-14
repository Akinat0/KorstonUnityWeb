
using System;
using Abu.Tools;
using UnityEngine;

public class BookInteractable : InteractableObject
{
    const string Id = "Book";
    
    public override bool Interact()
    {
        DateTime dateTime = DateTime.Now;
        
        if (dateTime.TimeOfDay > new TimeSpan(16, 0, 0) && dateTime.TimeOfDay < new TimeSpan(23, 0, 0))
        {
            GameInstance.Messenger.Show("We can read book from 16 to 23", Color.blue, 5);
            return false;
        }
        
        if (dateTime.Date == GameInstance.GameState.apartmentLastInteractionTimes.GetOrCreate(Id).Date)
        {
            GameInstance.Messenger.Show("We've already read book today", Color.blue, 5);
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
