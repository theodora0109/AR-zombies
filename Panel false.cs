using UnityEngine;

public class HidePanel : MonoBehaviour
{
    public GameObject panel; // Inspectorで対象のPanelを指定

    void Start()
    {
        StartCoroutine(HideAfterSeconds(3f)); // 5秒後に非表示
    }

    private System.Collections.IEnumerator HideAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(false);
    }
}
