using UnityEngine;

namespace Code.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector3)
        {
            return new Vector3Data(vector3.x, vector3.y, vector3.z);
        }
        
        public static Vector3 AsUnityVector3(this Vector3Data vector3data)
        {
            return new Vector3(vector3data.X, vector3data.Y, vector3data.Z);
        }

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }
        public static T ToDeserialized<T>(this string json)
        {
           return JsonUtility.FromJson<T>(json);
        }
        
        
    }
}