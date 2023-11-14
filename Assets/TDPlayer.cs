using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.UI;


namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance 
        { get 
            { 
                return Player.Instance as TDPlayer; 
            }
        }
                
        
        private event Action<int> OnGoldUpdate;//событийное статичное событие
        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }


        public event Action<int> OnLifeUpdate;//классификация event позволяет читать переменные, добавлять и удирать слушателей, но не стереть, то что находится внутри
        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }


        [SerializeField] private int m_gold = 0;


        public void ChangeGold(int change)
        {            
            m_gold += change;
            OnGoldUpdate(m_gold);
        }








        private event Action<int> OnOildUpdate;//событийное статичное событие
        public void OilUpdateSubscribe(Action<int> act)
        {
            OnOildUpdate += act;
            act(Instance.m_oil);
        }


        [SerializeField] private int m_oil = 0;
        public int Oil => m_oil;

        public void ChangeOil(int change)
        {
            m_oil += change;
            OnOildUpdate(m_oil);
        }







        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        
        [SerializeField] private Tower m_towerPrefab;


        //TODO: верим в то, что золота на постройку достаточно
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            Destroy(buildSite.gameObject);
        }


        [SerializeField] private UpgradeAsset healthUpgrade;
        [SerializeField] private UpgradeAsset slowByeUpgrade;
        [SerializeField] private GameObject slowAbilityButton;
        [SerializeField] private Abilities ability;
        [SerializeField] private UpgradeAsset slowSoldersUpgrade;

        private void Start()
        {
            //Health upgrade
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
            

            var slowBye = Upgrades.GetUpgradeLevel(slowByeUpgrade);
            if (slowBye == 1)
            {
                slowAbilityButton.SetActive(true);
            }

            var slow = Upgrades.GetUpgradeLevel(slowSoldersUpgrade);
            if (slow == 1)
            {
                ability.PlusCountTimeAbility();
            }
            
        }


    }
}

