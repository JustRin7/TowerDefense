using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea startArea;
        public CircleArea StartArea { get { return startArea;  } }

        [SerializeField] private AIPointPatrol[] points;

        public int Lenght { get =>  points.Length; }

        public AIPointPatrol this[int i] { get => points[i]; }//возвращает одну конкретную точку

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach(var point in points)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
                //Gizmos.DrawLine
            }
            
        }

    }
}
