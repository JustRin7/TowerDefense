using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path m_Path;//установка пути
        [SerializeField] private EnemyAsset[] m_EnemyAssets;//настройки врага


        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EnemyPrefab);
            e.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);

            //if (e.TryGetComponent<TDPatrolController>(out var ai))//пробуем из объекта достать компонентTDPatrolController и записываем объект как ai 
            e.GetComponent<TDPatrolController>().SetPath(m_Path);//задаем путь

            return e.gameObject;
        }

    }
}