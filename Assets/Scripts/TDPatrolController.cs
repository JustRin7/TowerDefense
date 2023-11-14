using UnityEngine;
using SpaceShooter;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_path;
        private int pathIndex;//индекс точки пути
        [SerializeField] private UnityEvent OnEndPath;

        public void SetPath(Path newPath)
        {
            m_path = newPath;//устанавливаем путь
            pathIndex = 0;//назначаем индекс первой точке

            SetPatrolBehaviour(m_path[pathIndex]);//отправляем путь и индекс точки
        }


        protected override void GetNewPoint()
        {
            pathIndex += 1;//++pathIndex - это pathIndex += 1

            if (m_path.Lenght > pathIndex)
            {
                SetPatrolBehaviour(m_path[pathIndex]);
            }
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
        }


    }
}

