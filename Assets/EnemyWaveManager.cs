using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemySpawn;
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;

        public event Action OnAllWavesDesd;


        private int activeEnemyCount = 0;
        private void RecordEnemyDead() 
        {
            if(--activeEnemyCount == 0)
            {
                    ForceNextWawe();
                
            } 
        }


        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
        }


        public void ForceNextWawe()
        {
            if(currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());

                SpawnEnemies();
            }
            else
            {
                if(activeEnemyCount == 0)
                {
                    OnAllWavesDesd?.Invoke();
                }                
            }
        }


        private void SpawnEnemies()
        {
            //создать врагов
            foreach( (EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefab, paths[pathIndex].StartArea.RandomInsideZone, Quaternion.identity);
                        e.OnEnd += RecordEnemyDead;
                        e.Use(asset);

                        //if (e.TryGetComponent<TDPatrolController>(out var ai))//пробуем из объекта достать компонентTDPatrolController и записываем объект как ai 
                        e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);//задаем путь

                        activeEnemyCount += 1;
                        OnEnemySpawn?.Invoke(e);
                    }
                    
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }


    }
}

