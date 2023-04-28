using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public float throwForce;
    private Vector3 lastFrameVelocity;
    public float minSpeed;
    private Vector3 direction;
    
    public void ThrowBall()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 force = Random.onUnitSphere*throwForce;
        force.z = 0;
        rb.AddForce(force,ForceMode.VelocityChange);
        minSpeed = force.magnitude;
    }
    
    private void Update()
    {
        lastFrameVelocity = rb.velocity;

        if (Input.GetMouseButtonDown(0))
        {
            DOTween.Complete(this);
            minSpeed += 3;
            DOVirtual.Float(minSpeed, minSpeed - 3, .3f, (x =>
            {
                minSpeed = x;
                rb.velocity = direction * minSpeed;
            })).SetId(this).OnComplete(() =>
            {
                rb.velocity = direction * minSpeed;

            });
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Brick>())
        {
            Bounce(collision.contacts[0].normal);
            collision.transform.GetComponent<Brick>().TakeDamage(10);
        }
    }
    
    private void Bounce(Vector3 collisionNormal)
    {
        direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direction * minSpeed;
    }

    
}
