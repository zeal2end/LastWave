using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 7;
    public LayerMask collisionMask;
    float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance); 
        
    }
    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        Idamagible damageableObject = hit.collider.GetComponent<Idamagible>();
        if(damageableObject != null){
            damageableObject.TakeHit(damage,hit);
        }
        GameObject.Destroy(gameObject);
    }
}
