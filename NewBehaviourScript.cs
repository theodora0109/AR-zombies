using UnityEngine;
using UnityEngine.UI;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint; // 発射位置（カメラの前でもOK）
    AudioSource audioSource;

    void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        transform.position += Camera.main.transform.forward * bulletSpeed;  //カメラを向けた方向に弾が跳ぶ
    }


    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);　//カメラの場所、軸に弾を生成する
        Rigidbody rb = bullet.GetComponent<Rigidbody>();　//弾に重さを持たせる
        rb.AddForce(Camera.main.transform.forward * bulletSpeed); //重力を持ちながら跳ぶ

         audioSource.PlayOneShot(audioSource.clip);//発砲音がする

        
        Vector3 direction = Camera.main.transform.forward; // カメラの向き（X・Y・Zすべて）に力を加える
        direction.y = 0f;
        direction.Normalize(); //ベクトルを正規化
       
        rb.velocity = direction * bulletSpeed; //毎秒カメラの向きに同じスピードで跳んでいく

        // 3秒後に自動削除
        Destroy(bullet, 3f);

    }
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Whatnot"))
        {
            Destroy(gameObject); // 弾を削除
        }
    }
}