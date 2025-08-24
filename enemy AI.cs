using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class enemyAI : MonoBehaviour
{
   [SerializeField] GameObject zombiePrefab = null;
   Vector3 defaultPosition = new Vector3();
   public Transform player;
  
   public float spawnInterval = 10f;   //ゾンビが出てくるスパン
   public float spawnRadiusMin = 5f;    // 最小スポーン距離
   public float spawnRadiusMax = 10f;　　//最大スポーン距離
   public Slider hpSlider;　　// HPのゲージ
   private const int maxHP = 100;　//100のHP
   private int currentHP;  //現在のHP
   public float walkspeed = 1.2f; //ゾンビが歩くスピード
   public float startspeed = 3.0f; //ゾンビが増殖する最初のタイミング
   private float currentInterval;　
   public float spawnDecrease = 0.05f;　//10秒ごとにスポーンの時間を短くする秒数
   public float delta = 0; //　　
   public AudioSource audioSource;　//ゾンビの声

   void Start()
   {
       defaultPosition = this.transform.localPosition;
       hpSlider.maxValue = maxHP;
       currentHP = maxHP;
       currentInterval = spawnInterval;
       hpSlider.value = currentHP;
    　 InvokeRepeating(nameof(SpawnZombie), startspeed, currentInterval);
      
   }
   

   void Update()
   { transform.rotation = Camera.main.transform.rotation;

    Vector3 direction = player.position - transform.position; //ゾンビがplayerに近づいてくる
    direction.Normalize();
    transform.position += direction * walkspeed * Time.deltaTime;　//だんだん早くなってくる

    transform.LookAt(player.transform.position);

    this.delta += Time.deltaTime;

     audioSource = GetComponent<AudioSource>();
   }

   void SpawnZombie()
    {
        float angle = Random.Range(-180f, 180f);　　　　　　　　　　//360度からゾンビが迫ってくる

        Quaternion rotation = Quaternion.Euler(0, angle, 0) * player.rotation;

        // 出現位置 = プレイヤー位置 + 回転方向 * 半径
        Vector3 spawnDirection = rotation * Vector3.forward;
        Vector3 spawnPosition = player.position + spawnDirection * spawnRadiusMax;

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
   
        currentInterval = Mathf.Max(currentInterval - spawnDecrease);
   
      //  Invoke("zombiePrefab", currentInterval);
   }

     void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
           //Destroy(collision.gameObject); //銃弾消滅
            currentHP -= 25;
            hpSlider.value = currentHP; 
            Debug.Log("プレイヤーの攻撃25のダメージ");
            }
            
        if(hpSlider.value <= 0)//100のHPがなくなったら
        {
            Destroy(this.gameObject);
            StopSounds();
        }
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        void StopSounds()
        {
            if(audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

    }
}

