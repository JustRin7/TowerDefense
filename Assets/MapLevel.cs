using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
using System;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private RectTransform resoultPanel;
        [SerializeField] private Image[] resoultImages;

        public bool IsComplete { get { return
                    gameObject.activeSelf &&
                    resoultPanel.gameObject.activeSelf;
            } }

        public void LoadLevel()
        {
            //if(episode)
            //{
                LevelSequenceController.Instance.StartEpisode(m_episode);
            //}            
        }         


        public int Initialize()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_episode);
            resoultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                resoultImages[i].color = Color.white;
            }
            return score;
        }


    }
}