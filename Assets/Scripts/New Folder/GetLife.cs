using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;

namespace TowerDefense
{
    public class GetLife : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] int life;
        [SerializeField] GameObject heart;
        [SerializeField] private Text m_text;



        public void LifeUpdate()
        {
            player.GetLife(life);
            m_text.text = life.ToString();
            Destroy(heart);

        }

    }
}


