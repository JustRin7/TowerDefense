using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {

        [SerializeField] private float m_Radius = 5f;
        private float m_Lead = 0.3f;
        private Turret[] turrets;
        private Rigidbody2D target = null;


        public void Use(TowerAsset asset)
        {

            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;
            turrets = GetComponentsInChildren<Turret>();

            foreach(var turret in turrets)
            {
                GetComponentInChildren<Turret>().m_TurretProperties = asset.turretProperties;
                turret.AssignLoadout(asset.turretProperties);//не устанавливает turretProperties в Turret
            }
            GetComponentInChildren<BuildSite>().SetBildableTowers(asset.m_UpgradesTo);
        }


        private void Update()
        {
            if(target)
            {

                /*if(Vector2.Distance(target.transform.position, transform.position) <= m_Radius)//если враг находится в рендже тавера*/
                if(Vector3.Distance(target.transform.position, transform.position) <= m_Radius)//то же, что и расчет Distance
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position
                            - turret.transform.position + (Vector3)target.velocity * m_Lead;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
                
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);//если враг попал в рендж тавера
                                                                                  //OverlapCircle возвращает в кач-ве результата коллайдер
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Rigidbody2D>();                    
                }
            }

           
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }

    }
}
