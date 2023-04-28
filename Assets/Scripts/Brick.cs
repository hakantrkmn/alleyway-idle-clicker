using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public float health=100;
    public MeshRenderer mesh;

    public void TakeDamage(float damage)
    {
        health -= damage;
        mesh.material.color = Color.Lerp(Color.red, Color.white, health / 100);
        if (health<0)
        {
            EventManager.BrickDestroyed(this);
            Destroy(gameObject);
        }
    }
}
