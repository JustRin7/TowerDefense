using System;
using UnityEngine;
using SpaceShooter;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace TowerDefense
{




    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType
        {
            Base = 0,
            Mage = 1
        }


        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {// Func похож на Action, но последний параметр - возвращаемый тип значений
            (int power, TDProjectile.DamageType type, int armor) =>
            {// ArmorType.Base
                switch(type)
                {
                    case TDProjectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },

            (int power, TDProjectile.DamageType type, int armor) =>
            {// ArmorType.Magic
                if(TDProjectile.DamageType.Base == type)
                    armor = armor / 2;
                return Mathf.Max(power - armor, 1);
            },
        };


        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_gold = 1;
        [SerializeField] private int m_armor = 1;
        [SerializeField] private ArmorType m_ArmorType;


        private Destructible m_destructible;


        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
        }



        public event Action OnEnd;


        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }


        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
            //sr.transform.position = new Vector3(sr.transform.position.x + asset.colliderLocation.x, sr.transform.position.y + asset.colliderLocation.y , sr.transform.position.z);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;


            GetComponent<SpaceShip>().Use(asset);


             GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            GetComponentInChildren<CircleCollider2D>().transform.localPosition = new Vector3(asset.colliderLocation.x, asset.colliderLocation.y, 0);

            m_damage = asset.damage;
            m_armor = asset.armor;
            m_ArmorType = asset.armorType;
            m_gold = asset.gold;
        }


        public void DamagePlayer()
        {
            //(Player.Instance as TDPlayer).ReduceLife(m_damage);//достаем TDPlayer через родительский класс Player и делаем так, чтобы возвращаемое значение было типа TDPlayer
            TDPlayer.Instance.ReduceLife(m_damage);
        }


        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_gold);
        }


        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {


            m_destructible.ApplyDamage( ArmorDamageFunctions[(int)m_ArmorType](damage, damageType, m_armor) );
        }


    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector: Editor//таким способом можно достучаться до способа отрисовки вещей в самом редакторе
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //GUILayout.Label("heya");

            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false ) as EnemyAsset;
            //памаметры в ObjectField: 1 - значение объекта, который должен находиться в этом поле
            //2 - тип объекта, который он может сюда назначать
            //3 - можно или нельзя использовать объекты из сцены

            if (a)
            {
                //тот, объект, кот. является целью инспектора представь в виде типа Enemy
                (target as Enemy).Use(a);
            }
        }
    }
#endif


}
