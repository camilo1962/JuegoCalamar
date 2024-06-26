using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    private bool isPlayerRun;

    private bool isStartGame;

    public GameObject arrow;
    public GameObject panelSiquitraque;

    public GameManager gameManager;

    private void Start()
    {
        transform.DOKill();
        StartCoroutine(StartedGame());
        panelSiquitraque.SetActive(false);
    }


    IEnumerator StartedGame()
    {
        yield return new WaitForSeconds(3f);
        arrow.SetActive(false);
        isStartGame = true;
    }

    void Update()
    {
        if(isStartGame)
            PlayerInputControl();
      
            
      
    }

    private void PlayerInputControl()
    {
        if (Input.GetMouseButtonDown(0) && isPlayerRun == false)
        {
            transform.DOMoveX(0f, 20f);
            
            gameObject.GetComponent<Animator>().SetTrigger("Run");
            isPlayerRun = true;
        }

        else if (Input.GetMouseButtonDown(0) && isPlayerRun == true)
        {
            transform.DOPause();
            
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            isPlayerRun = false;
        }
    }

    public void KillTween()
    {
        isStartGame = false;
        transform.DOKill();
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
        Invoke("Siquitraque", 2f);
        

    }

    public void Siquitraque()
    {
        panelSiquitraque.SetActive(true);
        DOTween.KillAll();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
           
            gameManager.FinishGame();
        }


    }
}
