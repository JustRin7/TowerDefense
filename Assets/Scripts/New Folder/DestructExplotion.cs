using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class DestructExplotion : MonoBehaviour
    {
        [SerializeField] private int m_Damage;
        [SerializeField] private float m_Velocity;


        private void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {
                var destructible = hit.collider.transform.root.GetComponent<Destructible>();

                if (destructible != null)
                {
                    destructible.ApplyDamage(m_Damage);
                }
            }
        }


    }
}
