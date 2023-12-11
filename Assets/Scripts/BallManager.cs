using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class BallManager : SerializedMonoBehaviour
{
    public GameObject ballPrefab;
    public List<List<Ball>> spawnedBalls;
    public List<GameObject> ballPrefabs;
    void SpawnBall(int index)
    {
        var ball = Instantiate(ballPrefabs[index], transform.position, quaternion.identity, transform);
        ball.GetComponent<Ball>().ThrowBall();
        spawnedBalls[index].Add(ball.GetComponent<Ball>());
        CheckIfCanMerge();
    }

    void CheckIfCanMerge()
    {
        foreach (var balls in spawnedBalls)
        {
            if (balls.Count >= 3)
            {
                EventManager.CanMerge(true);
                return;
            }
        }

        EventManager.CanMerge(false);
    }

    public void MergeBalls()
    {
        Ball firstBall = new Ball();
        Ball secondBall = new Ball();
        Ball thirdBall = new Ball();

        foreach (var balls in spawnedBalls)
        {
            if (balls.Count >= 3)
            {
                firstBall = balls[0];
                secondBall = balls[1];
                thirdBall = balls[2];
                balls.RemoveRange(0,3);
                break;
            }
        }

        Sequence merge = DOTween.Sequence();
        merge.Append(firstBall.transform.DOMove(transform.position, .2f));
        merge.Join(secondBall.transform.DOMove(transform.position, .2f));
        merge.Join(thirdBall.transform.DOMove(transform.position, .2f));
        merge.AppendCallback(() =>
        {
                SpawnBall(firstBall.level+1);
                Destroy(firstBall.gameObject);
                Destroy(secondBall.gameObject);
                Destroy(thirdBall.gameObject);
                CheckIfCanMerge();
        });

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
        if (type == IncrementalButtonTypes.AddBall)
            SpawnBall(0);
        else if(type==IncrementalButtonTypes.Merge)
            MergeBalls();
    }
}