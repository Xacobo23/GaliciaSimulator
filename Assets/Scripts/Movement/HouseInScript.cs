using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseInScript : MonoBehaviour
{
    bool isOnEnter = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnEnter)
        {
            Debug.Log("E");
            SceneManager.LoadScene("HouseScene");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isOnEnter = true;
        Debug.Log("Enter");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isOnEnter = false;
        Debug.Log("Exit");
    }
}
