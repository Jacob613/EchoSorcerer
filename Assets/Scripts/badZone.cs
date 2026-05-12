using UnityEngine;
using UnityEngine.SceneManagement;

public class badZone : MonoBehaviour
{

    public void ResetScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetScene();
            print("Collision");
        }
    }
    
}
