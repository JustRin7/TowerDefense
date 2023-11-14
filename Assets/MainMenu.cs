using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private GameObject newGamePanel;


        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);//выключаем кнопку
        }


        public void CheckNewGamePanelOn()
        {
            newGamePanel.SetActive(true);
        }

        public void CheckNewGamePanelOff()
        {
            newGamePanel.SetActive(false);
        }


        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.filename);
            FileHandler.Reset(Upgrades.filename);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }


    }
}