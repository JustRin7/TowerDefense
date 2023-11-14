using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;

        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>//лямбда выражениею Подписка на эвент
            //будет безымянная ф-я без параметров, кот будет делать то, что в скобках далее
           {
               StopLevelActivity();
               LevelResultController.Instance.Show(false);
           };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener( () =>
            {
                StopLevelActivity();
                //PlayerPrefs.SetInt(LevelSequenceController.Instance.CurrentEpisode.EpisodeName, levelScore);//под названием эпизода сохраняем левелскорс. PlayerPrefs - сохранение общих насроек, к примеру, громкость звука и сложность
                if (m_ReferenceTime <= Time.time)
                {
                    levelScore -= 1;
                }
                MapCompletion.SaveEpisodeResoult(levelScore);
            });

            void LifeScoreChange(int _)
            {
                levelScore -= 1;
                TDPlayer.Instance.OnLifeUpdate -= LifeScoreChange;
            }

            TDPlayer.Instance.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            foreach(var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T: MonoBehaviour//UnityEngine.Object 
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<EnemyWave>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
        }


    }


}
