using UnityEngine;
using UnityEngine.SceneManagement;
class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject GameOver;
    public GameObject WinGame;
    private void Start()
    {
        GameOver.SetActive(false);
        WinGame.SetActive(false);
    }
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
        }
        
    }
    public void WinGameActive()
    {
        WinGame.SetActive(true);
    }
    public void GameOverActive()
    {
        GameOver.SetActive(true);
    }
    public void RestartDelay(float delay)
    {
        Invoke("Restart", delay);
    }
    private void Restart()
    {
        FindObjectOfType<LevelLoader>().LoadNextLevel1();
    }
}
