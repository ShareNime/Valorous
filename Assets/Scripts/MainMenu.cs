using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
class MainMenu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuBGM");
    }
    public void PlayGame()
    {
        FindObjectOfType<LevelLoader>().LoadNextLevel1();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void HoverButton()
    {
        FindObjectOfType<AudioManager>().Play("hover");
    }
    
    //IEnumerator LoadLevel()
    //{
    //    TransitionAnimation.SetTrigger("Start");

    //    yield return new WaitForSeconds(1f);

    //    SceneManager.LoadScene(1);
    //}
}
