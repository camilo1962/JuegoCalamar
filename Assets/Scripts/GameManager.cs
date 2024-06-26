using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public List<GameObject> npcs;

    Vector3 firstPosition;
    Vector3 lastPosition;

    public GameObject greenRedLight;
    public PlayerController player;
    public GameObject tabToRun;
    public GameObject panelyouWinText;

    public Gun gun;


    public float greenTime;
    public float redTime;

    public bool checkRedLight;

    private AudioSource audioSource;
    public AudioClip music;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        panelyouWinText.SetActive(false);
        GreenRedLight();
       StartCoroutine(CloseTapToRun());
    }

 

    private void FixedUpdate()
    {
       
            StartCoroutine( CheckMovePlayer());
      
    }

    IEnumerator CheckMovePlayer()
    {
       
        if (player!=null && checkRedLight)
        {

            Vector3 startPos =player.transform.position;

            yield return new WaitForSeconds(0.5f);

            Vector3 finalPos = player.transform.position;

            if ((startPos.x - finalPos.x)>0.1f )
            {
                if (!player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                {
                    gun.Shoot(player.transform);
                    player.KillTween();
                }
             
                      
            }

        }
     
    }

    private void GreenRedLight()
    {
        StartCoroutine(StartGreenRedLight());
    }

    IEnumerator StartGreenRedLight()
    {
     
        greenRedLight.GetComponent<Image>().color = Color.red;

        checkRedLight = true;
        StartCoroutine(CheckMoveChar());

        yield return new WaitForSeconds(3f);
        greenRedLight.GetComponent<Image>().color = Color.green;
        PlaySoundFX(music, 1f);
        yield return new WaitForSeconds(3f);
        greenRedLight.GetComponent<Image>().color = Color.red;
        checkRedLight = true;
        StartCoroutine(CheckMoveChar());

        GreenRedLight();
    }
    IEnumerator CheckMoveChar()
    {
        if (npcs.Count > 0 )
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                if (npcs[i] !=null)
                {
                    firstPosition = npcs[i].transform.position;

                    yield return new WaitForSeconds(0.5f);

                    lastPosition = npcs[i].transform.position;
           
                    if ((firstPosition.x - lastPosition.x) > 0.15f &&npcs[i] != null)
                    {
                        if (npcs.Count > 0)
                        {
                            GameObject destroyNpc = npcs[i];
                     

                            gun.Shoot(destroyNpc.transform);
                            if (destroyNpc != null)
                            {
                                destroyNpc.GetComponent<NpcController>().KillTween();
                                Destroy(destroyNpc.GetComponent<NpcController>());
                            }
                                
          
                        }

                    }
                }                  
            }
            checkRedLight = false;       
        }         
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
        DOTween.KillAll();

    }

    IEnumerator CloseTapToRun()
    {
        yield return new WaitForSeconds(3f);
        tabToRun.SetActive(false);
    }


    public void FinishGame()
    {
        Time.timeScale = 0.0f;
        panelyouWinText.SetActive(true);
    }

    public void PlaySoundFX(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);

    }
}
