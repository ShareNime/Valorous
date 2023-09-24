using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
class LevelLoader : MonoBehaviour
{
    public Animator transitionlevel;
    private void Start()
    {
        
    }

    
    public void LoadNextLevel1()
    {
        StartCoroutine(LevelTrasition(1));
    }
    public void LoadNextLevel0()
    {
        StartCoroutine(LevelTrasition(0));
    }
    private IEnumerator LevelTrasition(int level)
    {
        transitionlevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }
}
