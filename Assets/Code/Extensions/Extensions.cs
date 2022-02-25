using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Extension
{
  
    public static class Extensions
    {
        public static T Closest<T>(this IEnumerable<T> source, T to) where T : Component
        {
            T closest = null;
            float closestToSqr = float.MaxValue;
            foreach (var item in source)
            {
                var itemDistance = (to.transform.position - item.transform.position).sqrMagnitude;
                if (closestToSqr >= itemDistance)
                {
                    closest = item;
                    closestToSqr = itemDistance;
                }
            }
            return closest;
        }
        public static T Closest<T>(this IEnumerable<T> source, Vector3 to) where T : Component
        {
            T closest = null;
            float closestToSqr = float.MaxValue;

            foreach (var item in source)
            {
                var itemDistance = (to - item.transform.position).sqrMagnitude;
                if (closestToSqr >= itemDistance)
                {
                    closest = item;
                    closestToSqr = itemDistance;

                    
                }
            }
            return closest;
        }
    }
    
}