using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    Animator animator;

    Transform target;

    int projectileDamage;

    [SerializeField]
    float projectileSpeed = 2f;

    [SerializeField]
    bool isParabellum = false;

    LayerMask layerMask;

    ContactFilter2D contactFilter2D;

    Collider2D[] colliders = new Collider2D[8];

    string opponent;

    bool targetAquired = false;

    bool isHit = false;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        if (gameObject.transform.parent.CompareTag("Player"))
        {
            opponent = "Enemy";
            layerMask = LayerMask.GetMask("Enemy");
            contactFilter2D.SetLayerMask(layerMask);
        }
        else if (gameObject.transform.parent.CompareTag("Enemy"))
        {
            opponent = "Player";
            layerMask = LayerMask.GetMask("Player");
            contactFilter2D.SetLayerMask(layerMask);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isHit)
        {
            DamageTarget();
        }
        TargetDestroyed();
    }

    public void StartProjectile(Transform tar, int damage)
    {
        target = tar;
        targetAquired = true;
        projectileDamage = damage;
        if (isParabellum)
        {
            Parabellum();
        }
        else
        {
            float x = target.transform.position.x - gameObject.transform.position.x;
            float y = (target.transform.position.y - gameObject.transform.position.y) / 3;
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(x, y).normalized) * projectileSpeed;
        }
        Destroy(gameObject, 6.0f);
    }

    void Parabellum()
    {
        float x = target.transform.position.x - gameObject.transform.position.x;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed * 0.6f;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 0.7f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        /*
        if (target.GetComponent<Animator>().GetBool("isAttacking"))
        {
            float x = target.transform.position.x - gameObject.transform.position.x;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed * 0.6f;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 0.7f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            float x = target.transform.position.x - gameObject.transform.position.x + target.GetComponent<UnitScript>().TotalSpeed * 1.3f;
            if (x > 0)
            {
                x = -x;
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed * 0.5f;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 0.7f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        */
    }

    void DamageTarget()
    {
        int temp = gameObject.GetComponent<Rigidbody2D>().OverlapCollider(contactFilter2D, colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i] != null)
            {
                if (colliders[i].tag != null)
                {
                    if (colliders[i].CompareTag(opponent))
                    {
                        isHit = true;
                        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        animator.SetTrigger("hit");
                        colliders[i].GetComponent<UnitScript>().RecieveDamage(projectileDamage);
                        StartCoroutine("DeleteObject");
                        break;
                    }
                }
            }
        }
    }

    void TargetDestroyed()
    {
        if (target == null && targetAquired)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
