using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    public GameObject CollisionPanel; // Reference to the collision panel UI GameObject
    public Button RestartButton; // Reference to the restart button in the collision panel

    private int collisionCount = 0; // Tracks collisions

    void OnCollisionEnter(Collision collision)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Survival")
        {
            // Increment collision count and show panel after 4 collisions
            collisionCount++;
            if (collisionCount >= 4)
            {
                Time.timeScale = 0f;
                CollisionPanel.SetActive(true);
                RestartButton.Select(); // Automatically select the restart button when the panel is shown
            }
        }
        else
        {
            if (collision.collider is BoxCollider)
            {
                Time.timeScale = 0f;
            }
            CollisionPanel.SetActive(true); // Show the collision panel
            RestartButton.Select(); // Automatically select the restart button when the panel is shown  
        }
    }
    
    public void Restart()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
