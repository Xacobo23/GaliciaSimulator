using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioScript : MonoBehaviour
{
    void Start()
    {

    }

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
                else if (clickedObject.CompareTag("Inicio"))
                {
                    LoadNewScene();
                }
            }
        }
    }

    private void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }


    private void LoadNewScene()
    {
        Debug.Log("Cargando nueva escena...");
        SceneManager.LoadScene("ComputerScene");
    }
}
