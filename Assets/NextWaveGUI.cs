using UnityEngine;
using UnityEngine.UI;


namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;
        private EnemyWaveManager manager;
        private float timeToNextWave;


        void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepeare += (float time) => 
            {
                timeToNextWave = time;
            };
        }


        public void CallWave()
        {
            manager.ForceNextWawe();
        }


        private void Update()
        { 
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;

            bonusAmount.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }


    }
}
