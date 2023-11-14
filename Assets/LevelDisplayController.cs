using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{   
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;
        [SerializeField] private BranchLevel[] branchLevels;

        void Start()
        {
            var drawLewel = 0;
            var score = 1;

            while( score != 0 && drawLewel < levels.Length )
            {
                score = levels[drawLewel].Initialize();
                drawLewel += 1;
            }

            for(int i = drawLewel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }

            for(int i = 0; i < branchLevels.Length; i++)
            {
                branchLevels[i].TryActive();
            }            
        }


    }
}