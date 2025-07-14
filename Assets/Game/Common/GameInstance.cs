
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Scripting;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; set; }
    
    
    Messenger messenger;
    KorstonGameState korstonGameState;
    
    public static KorstonGameState GameState => Instance != null ? Instance.korstonGameState : null;
    public static Messenger Messenger => Instance != null ? Instance.messenger : null;

    public static void SaveState()
    {
        if (!Instance || GameState == null) return;
        
        string message = JsonConvert.SerializeObject(new Dictionary<string, object> { { "id", "save_state" }, { "state", GameState}});
        
        Communication.SendMessageToFlutter(message);
    }
    
    
    void Awake()
    {
        Instance = this;
        
        messenger = gameObject.AddComponent<Messenger>();

        string message = JsonConvert.SerializeObject(new Dictionary<string, string> { { "id", "unity_started" } });
        
        Communication.SendMessageToFlutter(message);
    }
    
    [Preserve]
    public void HandleWebMessage(string message)
    {
        Debug.Log($"Web: {message}");

        Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);
        
        if (!dictionary.TryGetValue("id", out object messageObject) || messageObject is not string messageId)
        {
            Debug.LogError($"Message {message} doesn't contain id");
            return;
        }
        
        switch (messageId)
        {
            case "state":
            {
                if (!dictionary.TryGetValue("state", out object stateObject))
                {
                    Debug.LogError($"State message doesn't contain state");
                    return;
                }

                if (stateObject is JObject jobject)
                    korstonGameState = jobject.ToObject<KorstonGameState>();
                
                korstonGameState ??= new KorstonGameState();
                
                Debug.Log("state parsed");
                
                korstonGameState.lastPlayTime = DateTime.UtcNow;
                SaveState();
                return;
            }
        }
    }
}
