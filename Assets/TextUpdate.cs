using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSourse
        {
            Gold,
            Life,
            Oil
        }

        public UpdateSourse sourse = UpdateSourse.Gold;

        private Text m_text;


        void Start()
        {
            m_text = GetComponent<Text>();

            switch(sourse)
            {
                case UpdateSourse.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;

                case UpdateSourse.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
                case UpdateSourse.Oil:
                    TDPlayer.Instance.OilUpdateSubscribe(UpdateText);
                    break;
            }

        }


        private void UpdateText(int life)
        {
            m_text.text = life.ToString();
        }


    }
}