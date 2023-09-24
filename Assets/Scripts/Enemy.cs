using UnityEngine;
using System.Collections;
public enum enemyState
{
    idle, walk, attack, stagger
}
class Enemy : MonoBehaviour
{
    public enemyState currentState;
    public int health;
    public string enemyName;
    public int baseDamage;
    public float moveSpeed;
    public void Knock(Rigidbody2D PlayerRB, float knockTime)
    {
        StartCoroutine(KnockCo(PlayerRB, knockTime));
    }
    private IEnumerator KnockCo(Rigidbody2D PlayerRB, float knockTime)
    {
        if (PlayerRB != null)
        {
            yield return new WaitForSeconds(knockTime);
            PlayerRB.velocity = Vector2.zero;
            currentState = enemyState.idle;
            PlayerRB.velocity = Vector2.zero;
            Debug.Log("Musuh KenaKnock");
        }

    }
    

}
