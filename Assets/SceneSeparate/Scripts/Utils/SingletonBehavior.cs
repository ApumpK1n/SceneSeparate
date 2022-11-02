using System;
using UnityEngine;

namespace Pumpkin.Utility
{
    public class SingletonBehavior<T> : MonoBehaviour
    {
        public static T Instance;

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = Activator.CreateInstance<T>();
            }
        }
    }
}
