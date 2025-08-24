using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // -------------------------------------------------------
    /// <summary>
    /// ゲームステート.
    /// </summary>
    // -------------------------------------------------------
    public enum PlayState
    {
        None,
        Ready,
        Play,
        Finish,
    }

    
    public PlayState CurrentState = PlayState.None;// 現在のステート.

   
    [SerializeField] int countStartTime = 5; //! カウントダウンスタートタイム.

    
    [SerializeField] TMP_Text countdownText = null;//! カウントダウンテキスト.
    
    [SerializeField] TMP_Text timerText = null;//! タイマーテキスト.
   
    float currentCountDown = 0; // カウントダウンの現在値.
    
    float timer = 0;// ゲーム経過時間現在値.

    void Start()
    {
        CountDownStart();
    }

    void Update()
    {
　　　　timerText.text = "Time : 000.0 s";

        
        if (CurrentState == PlayState.Ready)// ステートがReadyのとき.
        {
          
            currentCountDown -= Time.deltaTime;  // 時間を引いていく.

            int intNum = 0;
          
            if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
            {
             
                intNum = (int)Mathf.Ceil(currentCountDown);
                countdownText.text = intNum.ToString();
            }
            else if (currentCountDown <= 0)
            {
                StartPlay();
                intNum = 0;
                countdownText.text = "Start!!";

              
                StartCoroutine(WaitErase());
            }
        }
        // ステートがPlayのとき.
        else if (CurrentState == PlayState.Play)  // Start表示を少しして消す.
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("000.0") + " s";
        }
        else
        {
            timer = 0;
            timerText.text = "Time : 000.0 s";
        }
    }


    void CountDownStart()
    {
        currentCountDown = (float)countStartTime;
        SetPlayState( PlayState.Ready );
        countdownText.gameObject.SetActive( true );
    }

    void StartPlay()
    {
        Debug.Log( "Start!!!" );
        SetPlayState( PlayState.Play );
    }

   
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds( 2f );
        countdownText.gameObject.SetActive( false );
    }

   
    void SetPlayState( PlayState state )
    {
        CurrentState = state;
    }
}