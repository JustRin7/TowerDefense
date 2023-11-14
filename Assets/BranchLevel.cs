using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Text m_pointText;
        [SerializeField] private MapLevel m_RootLevel;
        [SerializeField] private int m_NeedPoints = 3;
        

        /// <summary>
        /// ѕопытка активации ответвленного уровн€
        /// јктиваци€ требует наличи€ очков и выполнени€ прошлого уровн€
        /// </summary>
        public void TryActive()
        {
            gameObject.SetActive(m_RootLevel.IsComplete);

            if (m_NeedPoints > MapCompletion.Instance.TotalScore)
            {
                m_pointText.text = m_NeedPoints.ToString();                
            }
            else
            {
                m_pointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialize();
            }
        }


    }
}

