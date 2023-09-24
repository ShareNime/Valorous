using UnityEngine;
using System.Collections;
public enum PlayerState
{
    Idle,Walk,Attack,Stagger
}
class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float moveSpeed = 0.001f;
    private Rigidbody2D RB;
    public Animator animator;
    private Vector3 movement;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    private void Start()
    {
        currentState = PlayerState.Walk;
        RB = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        movement = Vector3.zero;
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.Attack && currentState != PlayerState.Stagger)
        {
            int randomattack = Random.Range(1,10);
            if(randomattack >= 5)
            {
                FindObjectOfType<AudioManager>().Play("playerattack");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("playerattack2");
            }
            
            StartCoroutine(Attackco());
        }
    }
    private void FixedUpdate()
    {
        if (currentState == PlayerState.Walk || currentState == PlayerState.Idle)
        {
            UpdateAnimationAndMove();
        }
    }
    private IEnumerator Attackco()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.Attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.Idle;
    }
    private void UpdateAnimationAndMove()
    {
        if (movement != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
    //private void FixedUpdate()
    // {
    //     RB.MovePosition(RB.position + movement * moveSpeed * Time.deltaTime);
    //}
    void MoveCharacter()
    {
        movement.Normalize();
        RB.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }
    public void Knock(float knockTime, int damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(RB, knockTime));
            int takingdamage = Random.Range(1, 10);
              
            if(takingdamage >= 5)
            {
                FindObjectOfType<AudioManager>().Play("takingdamage1");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("takingdamage2");
            }
        }
        else
        {
            PlayerPrefs.SetInt("Reach", 1);
            RB.velocity = Vector3.zero;
            if(currentState == PlayerState.Stagger)
            {
                FindObjectOfType<AudioManager>().Play("playerded");
            }
            //GameOver.SetActive(true);
            currentHealth.RuntimeValue = 3f; //KALAU MAU RESTART
            FindObjectOfType<GameManager>().GameOverActive();
            //FindObjectOfType<GameManager>().RestartDelay(2f); //KALAU MAU RESTART
            
            //FindObjectOfType<GameManager>().EndGame(); //KALAU MAU ENDGAME
        }
        
    }
    private IEnumerator KnockCo(Rigidbody2D PlayerRB, float knockTime)
    {
        if (PlayerRB != null)
        {
            yield return new WaitForSeconds(knockTime);
            PlayerRB.velocity = Vector2.zero;
            currentState = PlayerState.Idle;
            PlayerRB.velocity = Vector2.zero;
            Debug.Log("Player KenaKnock");

        }

    }
}

