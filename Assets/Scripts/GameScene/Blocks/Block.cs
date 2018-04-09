﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Block : MonoSingleton<Block>
{
    public GameObject goHpText;
    public GameObject DeathEFX;
    public GameObject imageBonus = null;
    public int hp , hpx2 = 1;
    public bool isBonus = false;

    private RectTransform containerPos;
    private GameController gameContr;
    private TextMeshProUGUI hpText;
    private Animator anim;
    private bool isDestroy, isApplBonus;
    private int visualIndexCont, rowID;

    private void Awake()
    {
        containerPos = GetComponentInParent<Container>().gameObject.GetComponent<RectTransform>();
        gameContr = GameController.Instance;
        GetComponent<RectTransform>().localScale = new Vector2(75, 75);
        anim = GetComponent<Animator>();
        visualIndexCont = GetComponentInParent<Container>().visualIndex;
        rowID = GetComponentInParent<Row>().rowID - 1;
    }

    private void Start()
    {
        //float rtime = Random.Range(2f, 5f);
        hpText = goHpText.GetComponent<TextMeshProUGUI>();
        hp = gameContr.score_Rows * hpx2;
        hpText.text = hp.ToString();
        isDestroy = isApplBonus = true;
        if (isBonus)
        {
            GetComponent<Image>().color = gameContr.ChangeColor(hp);
            anim.SetBool("IsBonus",true);
        }
        else
            GetComponent<SpriteRenderer>().color = gameContr.ChangeColor(hp);

        //InvokeRepeating("AnimState", 0, rtime);
    }

    public void ReceiveHit(bool hitBlBon)
    {
        if (hitBlBon)
        {
            hp = 0;
            //Debug.Log(hp);
        }
        else
            hp--;
        if (hp > 0)
        {
            if (isBonus)
                anim.SetBool("IsBonus", true);
            anim.SetTrigger("Hit");
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            if (isBonus && isApplBonus)
            {
                isApplBonus = false;
                GetComponentInParent<Container>().ApplySquare_Bonus();
            }
            hpText.text = "1";
            GameObject go = Instantiate(DeathEFX, containerPos) as GameObject;

            var main = go.GetComponent<ParticleSystem>().main;
            if (isBonus)
                main.startColor = GetComponent<Image>().color;
            else
                main.startColor = GetComponent<SpriteRenderer>().color;

            GetComponentInParent<Row>().nrSpace++;
            LevelManager.Instance.listFreeConts.Add(GetComponentInParent<Container>().gameObject);
            Destroy(go, 1f);
            Destroy(gameObject, 1f);
            if (isDestroy)
            {
                ScoreLEVEL.Instance.AddScoreLevel();
                ScoreLEVEL.Instance.ShowNrBlock(containerPos);
                isDestroy = false;
            }
            gameObject.SetActive(false);
            return;
        }
        hpText.text = hp.ToString();
        if (!isBonus)
            GetComponent<SpriteRenderer>().color = gameContr.ChangeColor(hp);
    }

    public void ReciveHitByBonus()
    {
        //hp = 1;
        LevelManager.Instance.ApplyBallBonus(visualIndexCont, rowID - 1);
        LevelManager.Instance.ApplyBallBonus(visualIndexCont, rowID);
        LevelManager.Instance.ApplyBallBonus(visualIndexCont, rowID + 1);
        //ReceiveHit(false);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag(Tags.Player) || coll.gameObject.CompareTag(Tags.ballCopy) || coll.gameObject.CompareTag(Tags.ballSQLine) || coll.gameObject.CompareTag(Tags.LaserSq))
            ReceiveHit(false);
        if (coll.gameObject.CompareTag(Tags.Bonus_02))
        {
            ReciveHitByBonus();
            //GetComponentInParent<Row>().RunBonus_02();
        }
    }

}
