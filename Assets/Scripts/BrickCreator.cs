using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BrickCreator : MonoBehaviour
{
    public float height;
    public float width;
    public GameObject brickPrefab;
    public List<Brick> bricks;
    public float middleRadius;

    private void OnEnable()
    {
        EventManager.BrickDestroyed += BrickDestroyed;
    }

    private void BrickDestroyed(Brick brick)
    {
        bricks.Remove(brick);
    }

    private void Start()
    {
        CreateBricks();
        DestroyMiddleBricks();
    }

    void CreateBricks()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var x = (transform.position.x - (width / 2)) + j+.5f;
                var y = (transform.position.y - (height / 2)) + i+.5f;

                var pos = new Vector3(x, y, 0);
                var brick = Instantiate(brickPrefab, pos, quaternion.identity, transform);
                bricks.Add(brick.GetComponent<Brick>());
            }
        }
    }

    void DestroyMiddleBricks()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(middleRadius,middleRadius,middleRadius));
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.SetActive(false);
            bricks.Remove(hitCollider.GetComponent<Brick>());
        }
    }
}