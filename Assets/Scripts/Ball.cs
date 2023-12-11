using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float damage;
    public float moveSpeed;
    private Vector3 direction;
    public float incomeAmount;
    public int level;
    public void ThrowBall()
    {
        direction = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0);
        
    }

    
    private void OnEnable()
    {
        EventManager.IdleButtonClicked += IdleButtonClicked;
    }

    private void OnDisable()
    {
        EventManager.IdleButtonClicked -= IdleButtonClicked;
    }

    private void IdleButtonClicked(IncrementalButtonTypes type, float amount)
    {
        if (type==IncrementalButtonTypes.Income)
        {
            incomeAmount *= 1.1f;
        }
        else if (type==IncrementalButtonTypes.Speed)
        {
            moveSpeed *= 1.1f;
        }
    }

    private void Update()
    {
        transform.position += direction * (moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            DOTween.Complete(this);
            moveSpeed += 3;
            DOVirtual.Float(moveSpeed, moveSpeed - 3, .3f, (x =>
            {
                moveSpeed = x;
            })).SetId(this);
        }
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Brick>())
        {
            Bounce(collision.contacts[0].normal);
            collision.transform.GetComponent<Brick>().TakeDamage(damage);
            EventManager.EarnMoney(incomeAmount);
        }
    }
    
    private void Bounce(Vector3 collisionNormal)
    {
        direction = Vector3.Reflect(direction, collisionNormal);
        direction = direction.normalized;
    }

  
}
