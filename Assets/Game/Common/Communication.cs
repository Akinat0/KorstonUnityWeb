using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;

public static class Communication 
{
    [DllImport("__Internal")]
    static extern void SendToFlutter(string message);

    public static void SendMessageToFlutter(string content) {
        Debug.Log("Send Message To Flutter: " + content);
        
#if UNITY_WEBGL && !UNITY_EDITOR
        SendToFlutter(content);
#else
        
        
        Dictionary<string, object> decoded = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
        
        string messageId = decoded["id"] as string;
        
        if (messageId == "unity_started") {
            //send game state to unity

            var stateMessage = new Dictionary<string, object>();
            
            string stateJson = PlayerPrefs.GetString("state", "");
            var state = JsonConvert.DeserializeObject<Dictionary<string, object>>(stateJson);

            stateMessage["id"] = "state";
            stateMessage["state"] = state;
            
            GameInstance.Instance.HandleWebMessage(JsonConvert.SerializeObject(stateMessage));
        }
        else if (messageId == "save_state")
        {
            object state = decoded["state"];
            var stateJson = JsonConvert.SerializeObject(state);
            
            PlayerPrefs.SetString("state", stateJson);
            PlayerPrefs.Save();
        }
        
#endif
        
    }
}
