using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

            if (hitCollider != null)
            {
                GameObject clickedObject = hitCollider.gameObject;

                if (clickedObject.CompareTag("Exit"))
                {
                    ExitGame();
                }
                else if (clickedObject.CompareTag("Reset"))
                {
                    ResetGame();
                }
            }
        }
    }

    private void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    private void ResetGame()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("Reiniciando el juego...");
            GameManager.Instance.ResetGameValues();
            SaveManager.ResetSaveData();
            SceneManager.LoadScene("ComputerScene");
        }
        else
        {
            Debug.LogError("GameManager no ha sido inicializado.");
        }
    }
}
