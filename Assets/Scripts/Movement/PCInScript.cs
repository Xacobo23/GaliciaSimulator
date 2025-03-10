using UnityEngine;
using UnityEngine.SceneManagement;

public class PCInScript : MonoBehaviour
{
    bool isOnPC = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnPC)
        {
            Debug.Log("E");
            SceneManager.LoadScene("ComputerScene");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isOnPC = true;
        Debug.Log("PC in");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isOnPC = false;
        Debug.Log("PC out");
    }
}
