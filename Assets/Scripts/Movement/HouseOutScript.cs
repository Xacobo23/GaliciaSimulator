using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseOutScript : MonoBehaviour
{

    bool isOnExit = true;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnExit)
        {
            Debug.Log("E");
            SceneManager.LoadScene("PrincipalScene");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isOnExit = true;
        Debug.Log("Enter");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isOnExit = false;
        Debug.Log("Exit");
    }
}
