using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D rig;
        private SpriteRenderer sr;

        private void Start()
        {
            rig = transform.root.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()//скрипт постобработки. включается после всех движений в кадре
        {
            transform.up = Vector2.up;

            var xMotion = rig.velocity.x;//xMotion - переменная движения по X

            if(xMotion > 0.01f)//поворот рыцаря при движении
            {
                sr.flipX = false; //flipX - галочка, в настройках, отвечающая за поворот спрайта на 180
            }
            else
                if (xMotion < 0.01f)//поворот рыцаря при движении
                {
                    sr.flipX = true; //flipX - галочка, в настройках, отвечающая за поворот спрайта на 180
                }
        }


    }
}
