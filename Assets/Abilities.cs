using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Abilities : MonoSingleton<Abilities>
    {


        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost = 5;
            [SerializeField] private int m_Damage = 5;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private GameObject m_FireButton;
            

            public void Use()
            {
                ClickProtection.Instance.Activate( (Vector2 v)  =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach( var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if(collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                    m_FireButton.SetActive(false);
                });
            }
        }

       

        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private UpgradeAsset slowSoldersUpgrade;
            public int count = 2;

            public void Use()
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMAxVelocity(count);
                }


                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMAxVelocity(count);
                EnemyWaveManager.OnEnemySpawn += Slow;


                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMAxVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }


                Instance.StartCoroutine(Restore());


                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_TimeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAbilityButton());
            }

            public void PlusCount()
            {
                count += 2;
            }
        }



        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_TimeButton;
        

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();


        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();
        public void PlusCountTimeAbility() => m_TimeAbility.PlusCount();


    }
}
