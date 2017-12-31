﻿using UnityEngine;

public class Container : MonoBehaviour
{
    public int visualIndex;

    private BlockType[] blockTypes = new BlockType[5];

    private void Awake()
    {
        blockTypes = GetComponentsInChildren<BlockType>();
    }

    public void SpawnType(BlType type)
    {
        for (int i = 0; i < blockTypes.Length; i++)
        {
            if (blockTypes[i].Bltype == type && type != BlType.space)
            {
                blockTypes[i].Option(true);
                GetComponentInParent<Row>().nrBlock++;
            }
            else
                blockTypes[i].Option(false);
        }
        DeSpawnBlock();
    }

    public void DeSpawnBlock()
    {
        blockTypes = GetComponentsInChildren<BlockType>();
        if (blockTypes.Length <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void EndLevel(GameObject obj)
    {
        if (obj.tag == Tags.Square)
        {
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (obj.tag == Tags.Square_01)
        {
            Destroy(obj);
        }
    }
}
