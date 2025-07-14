using System;
using System.Collections.Generic;

[Serializable]
public class KorstonGameState
{
    public int version = 1;
    public DateTime lastPlayTime;
    public int stars;
    public Dictionary<string, DateTime> apartmentLastInteractionTimes = new Dictionary<string, DateTime>();
}
