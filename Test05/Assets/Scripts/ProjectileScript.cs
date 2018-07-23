using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    Transform target;

    int projectileDamage;

    [SerializeField]
    float projectileSpeed = 2f;

    LayerMask layerMask;

    ContactFilter2D contactFilter2D;

    Collider2D[] colliders = new Collider2D[8];

    string opponent;

    bool targetAquired = false;

    // Use this for initialization
    void Start () {
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
        DamageTarget();
        TargetDestroyed();
    }

    public void StartProjectile(Transform tar, int damage)
    {
        target = tar;
        targetAquired = true;
        projectileDamage = damage;
        float x = target.transform.position.x - gameObject.transform.position.x;
        float y = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(x, y).normalized) * projectileSpeed;
        Destroy(gameObject, 5.0f);
    }

    void DamageTarget()
    {
        int temp = gameObject.GetComponent<Rigidbody2D>().OverlapCollider(contactFilter2D, colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i] != null)
            {
                Debug.Log("col " + colliders[i].name);
                if (colliders[i].CompareTag(opponent))
                {
                    colliders[i].GetComponent<UnitScript>().RecieveDamage(projectileDamage);
                    Destroy(gameObject);
                    break;
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
}
