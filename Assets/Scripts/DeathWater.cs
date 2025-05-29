using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene(3);
        }
    }
}
