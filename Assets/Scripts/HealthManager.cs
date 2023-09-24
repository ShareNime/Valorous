using UnityEngine;
using UnityEngine.UI;
class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullheart;
    public FloatValue healthContainers;
    public FloatValue playerCurrentHealth;
    private void Start()
    {
        initHearts();
    }

    private void initHearts()
    {
        for(int i = 0; i < healthContainers.initialValue;i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullheart;
        }   
    }
    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue;
            if(tempHealth == 2f)
            {
                hearts[0].gameObject.SetActive(false);
            }else if(tempHealth == 1f)
            {
                hearts[1].gameObject.SetActive(false);
            }else if (tempHealth == 0f)
            {
                hearts[2].gameObject.SetActive(false);
            }
        
    }
}
