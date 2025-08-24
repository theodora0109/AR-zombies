using UnityEngine;
using UnityEngine.SceneManagement; // シーンをリロードする時に使う
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
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

        if (currentHealth <= 0)
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
	 void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
     void StopAllZombiesGroan()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Whatnot");
        foreach (var z in zombies)
        {
            AudioSource a = z.GetComponent<AudioSource>();
            if (a != null && a.isPlaying)
            {
                a.Stop();
            }
        }
    }
}
