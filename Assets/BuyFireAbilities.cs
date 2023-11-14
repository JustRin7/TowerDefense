using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyFireAbilities : MonoBehaviour
    {
        [SerializeField] int Coast;
        [SerializeField] TDPlayer tDPlayer;
        [SerializeField] GameObject FireAbilitiesButton;

        public void ChangeOil()
        {
            if(tDPlayer.Oil >= Coast)
            {
                if (tDPlayer.Oil == Coast)
                {
                    GetComponent<Button>().interactable = false;                    
                }
                tDPlayer.ChangeOil(-Coast);
                FireAbilitiesButton.SetActive(true);
            }
        }



    }
}
