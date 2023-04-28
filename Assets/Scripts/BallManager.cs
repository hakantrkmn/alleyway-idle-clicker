using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;

    [Button]
    void SpawnBall()
    {
        var ball = Instantiate(ballPrefab, transform.position, quaternion.identity, transform);
        ball.GetComponent<Ball>().ThrowBall();
    }
}
