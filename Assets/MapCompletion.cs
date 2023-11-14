using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string filename = "completion.dat";        


        [Serializable]
        private class EpisodeScore//тип данных, который соединяет в себе эпизод и очки
        {
            public Episode episode;
            public int score;
        }


        [SerializeField] private EpisodeScore[] complitionData;
        public int TotalScore { private set; get; }


        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref complitionData);
            foreach (var episodeScore in complitionData)
            {
                TotalScore += episodeScore.score;
            }
        }


        public static void SaveEpisodeResoult(int levelScore)
        {
            if (Instance)
            { 
                foreach (var item in Instance.complitionData)
                {// Созранение новых очков прохождения
                    if (item.episode == LevelSequenceController.Instance.CurrentEpisode)
                    {
                        if (levelScore > item.score)
                        {
                            Instance.TotalScore += levelScore - item.score;
                            item.score = levelScore;
                            Saver<EpisodeScore[]>.Save(filename, Instance.complitionData);
                        }
                    }
                }
            }
            else
            {
                Debug.Log($"Episode complete with score {levelScore}");
            }
        }


        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in complitionData)
            {
                if(data.episode == m_episode)
                {
                    return data.score;
                }                
            }
            return 0;
        }


    }
}