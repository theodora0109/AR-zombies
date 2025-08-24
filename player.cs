using UnityEngine;
using UnityEngine.SceneManagement; // シーンをリロードする時に使う
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;　//プレイヤーの体力
    private int currentHealth;　//現在の体力
	public GameObject gameoverCanvas;  
	public Image[] hearts;
  

    void Start()
    {
        currentHealth = maxHealth;
		gameoverCanvas.SetActive(false); 
		UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player hit! Remaining HP: " + currentHealth);

        if (currentHealth <= 0) //ハートが0になったらゲームオーバー
        {
            Die();
           
        }
		UpdateHearts();
    }

    void Die()
    {
        Debug.Log("Player died!");
        // シーンをリロード（リスタート）する場合:
       	gameoverCanvas.SetActive(true);
			//Time.timeScale = 0f;
        // またはゲームオーバー画面に移動するなど
		Time.timeScale = 0f;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Whatnot"){　
			TakeDamage(1);
		}
    }
	 void UpdateHearts() // ハートの残機
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
}

