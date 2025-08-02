using UnityEngine;

namespace Controllers.ui
{
    public class GameOverDialogController : MonoBehaviour
    {
        public GameObject gameOverDialog;

        void Start()
        {
            gameOverDialog.SetActive(false);
        }
        
        public void ShowGameOverDialog()
        {
            gameOverDialog.SetActive(true);
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