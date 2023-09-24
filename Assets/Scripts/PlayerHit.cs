using UnityEngine;
using System.Collections;
class PlayerHit : MonoBehaviour
{
    public int damage = 100;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Melee>().takeDamage(damage);
        }
    }
    
}
