
using System.Collections.Generic;
using UnityEngine;

public class Messenger : MonoBehaviour
{
    public float displayDuration = 5f; 
    public int maxMessages = 30;       
    
    // Internal log storage
    struct Message
    {
        public string message;
        public Color color;
        public float expirationTime;
    }
    
    List<Message> messages = new List<Message>();

    [ContextMenu("Test")]
    public void Test() => Show("SOSI");
    
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    public void Show(string text, Color color = default, float time = 2)
    {
        if (color == default)
            color = Color.cyan;
        
        Message message = new Message()
        {
            message = text,
            color = color,
            expirationTime = Time.time + displayDuration
        };
        
        messages.Add(message);
        
        if (messages.Count > maxMessages)
            messages.RemoveAt(0);
        
    }
    
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        Color color = Color.white;

        switch (type)
        {
            case LogType.Log:
                color = Color.white;
                break;
            case LogType.Warning:
                color = Color.yellow;
                break;
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                logString += $"\n" + stackTrace;
                color = Color.red;
                break;
        }
        
        
        Show(logString, color, 3);
    }
    
    void Update()
    {
        // Remove expired messages
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            if (Time.time > messages[i].expirationTime)
            {
                messages.RemoveAt(i);
            }
        }
    }

    GUIStyle style;
    Vector2 shadowOffset;
    
    void OnGUI()
    {

        if (style == null)
        {
            const int referenceScreenWidth = 1920;
            const int referenceScreenHeight = 1080;

            float scale = Mathf.Min((float)Screen.width / referenceScreenWidth, (float)Screen.height / referenceScreenHeight);
            
            style = GUIStyle.none;
            style.fontSize = Mathf.RoundToInt(30 * scale);
            style.alignment = TextAnchor.LowerCenter;
            style.fontStyle = FontStyle.Bold;
        }

        
        Rect messageArea = new Rect(
            0, 0, Screen.width, (float)Screen.height / 2
        );
        
        foreach (Message entry in messages)
        {
            GUI.color = entry.color;
            
            style.normal.textColor = Color.black;
            style.contentOffset = Vector2.zero;
            GUI.Label(messageArea, entry.message, style);
            
            style.normal.textColor = entry.color;
            float offset = (float)style.fontSize / 10;
            style.contentOffset = new Vector2(-offset, -offset);
            GUI.Label(messageArea, entry.message, style);
            
            messageArea.y -= style.fontSize;
        }
        
        GUI.color = Color.white;
    }
}
