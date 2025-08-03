using Quests;
using TMPro;
using UnityEngine;

namespace Controllers.ui
{
    public class WinDialogController : MonoBehaviour
    {
        public GameObject gameWinDialog;
        public TMP_Text timeText;
        public TMP_Text drinksText;
        public TMP_Text finalScoreText;
        public TMP_Text completionText;

        void Start()
        {
            gameWinDialog.SetActive(false);
        }

        private string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return $"{minutes:D2}:{seconds:D2}";
        }
        
        public void ShowGameWin()
        {
            int drinks = PlayerStateController.GetInstance().GetDrinks();
            float totalTime = Time.timeSinceLevelLoad;
            float timeDeduction = drinks * 5.0f;
            float finalTime = totalTime - timeDeduction;
            timeText.text = "Time: " + FormatTime(totalTime);
            drinksText.text = "Drinks: " + drinks;
            finalScoreText.text = "Final Time: " + FormatTime(finalTime);
            float completionPercentage = QuestCoordinator.GetInstance().GetCompletionPercentage();
            // Format the completion percentage to two decimal places
            completionText.text = $"{completionPercentage * 100f:F2}% Completion";
            gameWinDialog.SetActive(true);
        }
        
        // Function to restart the scene
        public void RestartScene()
        {
            // In case time was stopped, resume it
            Time.timeScale = 1f;
            // Reload the current scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        
        // Function to quit the game
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}