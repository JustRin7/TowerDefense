using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float timeLimit = 4f;//сколько времени должно пройти для победы

        void Start()
        {
            timeLimit += Time.time;
        }

        public bool IsCompleted => Time.time > timeLimit;//Time.time - время с начала работы приложения
    }

}
