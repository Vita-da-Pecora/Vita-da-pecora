using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Attack : MonoBehaviour
{
    [Header("SetUp")]
    [SerializeField] private string loseSceneName = "SCN_Lose";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(loseSceneName);
        }
    }
}
