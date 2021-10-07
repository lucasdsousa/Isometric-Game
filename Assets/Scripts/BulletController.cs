using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float speed = 50f;
    private float timeToDestroy = 3f;

    public ParticleSystem muzzleFlash;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    /* private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target) < .01f) {
            Destrou(gameObject);
        }
    } */
}
