﻿using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// Ссылки на то что спавнить
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[ Random.Range(0, m_EntityPrefabs.Length) ] );//возвращает копию случайного эл-та из этого префаба
        }

    }
}