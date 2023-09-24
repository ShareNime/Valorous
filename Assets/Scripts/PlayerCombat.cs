using UnityEngine;
using System.Collections;
class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public Transform attackPoint3;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int damage = 25;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public Animator animator;
    public float thrust;
    public float knocktime;
    private void FixedUpdate()
    {
        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        //play an attack animation
        //detect enemy in attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange, enemyLayer);
        hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, enemyLayer);
        hitEnemies = Physics2D.OverlapCircleAll(attackPoint3.position, attackRange, enemyLayer);
        //damaging an enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Melee>().takeDamage(damage);           
        }
    }
    //private IEnumerator Knockco(Rigidbody2D enemy)
    //{
    //    if (GameObject.FindGameObjectWithTag("Enemy") != null)
    //    {
    //        yield return new WaitForSeconds(knocktime);
    //        enemy.velocity = Vector2.zero;
    //        enemy.isKinematic = true;
    //    }



    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
