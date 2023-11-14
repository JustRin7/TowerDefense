using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        private int costNumber = 0;
        [SerializeField] public Text level, costText;
        [SerializeField] private Button buyButton;

        public void Initialize()
        {
            upgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);
            
            if(savedLevel >= asset.costByLevel.Length)
            {
                level.text = $"Lvl: {savedLevel} (Max)";
                buyButton.interactable = false;
                buyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyButton.transform.Find("Text").gameObject.SetActive(false);
                costText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                level.text = $"Lvl: {savedLevel + 1}";
                costNumber = asset.costByLevel[savedLevel];
                costText.text = costNumber.ToString();
            }            
        }


        public void CheckCost(int money)
        {
            buyButton.interactable = money >= costNumber;
        }


        public void Buy( )
        {
            Upgrades.BuyUpdgade(asset);
            Initialize();
        }


    }
}