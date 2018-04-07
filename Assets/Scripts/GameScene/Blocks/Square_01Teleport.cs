﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square_01Teleport : MonoBehaviour
{
    //public Image imgSign;
    public Material imgSign;

    private GameController gameContr;

    private void Start()
    {
        gameContr = GameController.Instance;
        ChangeColor();
        LevelManager.Instance.listTelep.Add(this.gameObject);
    }
    
    public void ChangeColor()
    {
        var colorScore = gameContr.score_Rows;
        imgSign.color = gameContr.ChangeColor(colorScore);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag(Tags.Player) || coll.gameObject.CompareTag(Tags.ballCopy))
        {
            if (coll.gameObject.GetComponent<Ball>().enterTeleport)
                LevelManager.Instance.Teleports(this.gameObject, coll.gameObject);
        }
    }
}
