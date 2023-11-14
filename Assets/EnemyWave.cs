using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    internal class EnemyWave: MonoBehaviour
    {
        public static Action<float> OnWavePrepeare;

        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }


        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private PathGroup[] groupse;
        [SerializeField] private float prepeareTime = 10f;


        public float GetRemainingTime() { return prepeareTime - Time.time; }


        void Awake()
        {
            enabled = false;
        }


        private event Action OnWaveReady;


        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepeare?.Invoke(prepeareTime);

            prepeareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }


        private void Update()
        {
            if(Time.time >= prepeareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }


        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < groupse.Length; i++)
            {
                foreach (var squad in groupse[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                    //yield return - возможность возвращать значения из ф-ии не сразу, а по одному, работает только если
                    //возвращаемый тип ф-ии IEnumerable
                }
            }

        }

        [SerializeField] private EnemyWave next;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if(next) next.Prepare(spawnEnemies);

            return next;
        }       


    }
}