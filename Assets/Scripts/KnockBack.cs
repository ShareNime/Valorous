using UnityEngine;
using System.Collections;
class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            if(hit != null)
            {
                if (gameObject.CompareTag("Enemy"))
                {
                    if (other.gameObject.CompareTag("Player"))
                    {
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.Stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, gameObject.GetComponent<Melee>().baseDamage);

                    }
                }
                if (gameObject.CompareTag("Player"))
                {
                    if (other.gameObject.CompareTag("Enemy"))
                    {
                        
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        FindObjectOfType<Enemy>().GetComponent<Animator>().SetFloat("Enemy_Knock_Right", difference.x);
                        hit.AddForce(difference, ForceMode2D.Impulse);
                        hit.GetComponent<Enemy>().currentState = enemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime);
                        //FindObjectOfType<Enemy>().GetComponent<Animator>().SetBool("Attacked", false);
                    }
                }
               
            }
            
        }
    }
    private IEnumerator KnockCo(Rigidbody2D enemyRB)
    {
        if(enemyRB != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemyRB.velocity = Vector2.zero;
            Debug.Log("KenaKnock");
        }
        
    }
}
