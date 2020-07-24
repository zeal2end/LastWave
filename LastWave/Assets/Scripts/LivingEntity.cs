using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, Idamagible{

    public float startingHealth;
    protected float health;
    protected bool dead =false;
    public event System.Action OnDeath;

    protected virtual void Start() {
        health  = startingHealth;
    }
    public void TakeHit(float damage, RaycastHit hit){
        health -= damage;
        if(health <= 0 && !dead){
            Die();
        }
    }
    protected void Die(){
        dead = true;
        if(OnDeath != null){
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}