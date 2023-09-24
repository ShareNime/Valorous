using UnityEngine;
using System.Collections;
class Melee : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public int currentHealth;
    public Animator anim;
    private Rigidbody2D RB;
    
    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        currentState = enemyState.idle;
        
        
        currentHealth = health;
        RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        checkDistance();

    }
    void checkDistance()
    {
        if (Vector3.Distance(target.position, RB.transform.position) <= chaseRadius && Vector3.Distance(target.position, RB.transform.position) > attackRadius)
        {
            if(currentState == enemyState.idle || currentState == enemyState.walk && currentState != enemyState.stagger)
           {
                Vector3 temp = Vector3.MoveTowards(RB.transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                RB.MovePosition(temp);
                currentState = enemyState.walk;
                anim.SetFloat("Speed", 1f);
                RB.velocity = Vector3.zero;
            }

        }
        else if(Vector3.Distance(target.position,RB.transform.position) > chaseRadius)
        {
            currentState = enemyState.idle;
            anim.SetFloat("Speed", 0f);
        }
    }
    private void changeAnim(Vector2 direction)
    {
        anim.SetBool("Attacked", false);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
        if (direction.x > 0)
        {
            anim.SetFloat("Enemy_MoveX", 1f);
        }else if(direction.x < 0)
        {
            anim.SetFloat("Enemy_MoveX", -1f);
        } 
    }
    public void takeDamage(int damage)
    {
        anim.SetBool("Attacked", true);
        currentHealth -= damage;
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
        if(currentHealth <= 0){
        Die();
        }
    }
    void Die()
    {
        Debug.Log("MATI!");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
