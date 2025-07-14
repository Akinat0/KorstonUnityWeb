using System.Collections.Generic;
using UnityEngine;

namespace Abu.Tools
{
    public static class Utility
    {
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue fallback = default)
        {
            if(!dict.ContainsKey(key))
                dict.Add(key, fallback);
            
            return dict[key];
        }
        
        public static Vector3 SetX(this Vector3 position, float value)
        {
            position.x = value;
            return position;
        }
    
        public static Vector3 SetY(this Vector3 position, float value)
        {
            position.y = value;
            return position;
        }
    
        public static Vector3 AddZ(this Vector3 position, float value)
        {
            position.z += value;
            return position;
        }
    
        public static Vector3 SetZ(this Vector3 position, float value)
        {
            position.z = value;
            return position;
        }
    
        public static void SetX(this Transform transform, float value)
        {
            Vector3 position = transform.position;
            position.x = value;
            transform.position = position;
        }
    
        public static void SetY(this Transform transform, float value)
        {
            Vector3 position = transform.position;
            position.y = value;
            transform.position = position;
        }
    
        public static void SetZ(this Transform transform, float value)
        {
            Vector3 position = transform.position;
            position.z = value;
            transform.position = position;
        }
    }
}