using UnityEngine;
using UnityEngine.SceneManagement;

public class PCOutScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log("PCOutScript iniciado");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("Rato en " + mousePos);

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

            if (hitCollider != null)
            {
                Debug.Log("Collider " + hitCollider.gameObject.name);
                if (hitCollider.gameObject == gameObject)
                {
                    SceneManager.LoadScene("HouseScene");
                }
                else
                {
                    Debug.Log("Error");
                }
            }
            else
            {
                Debug.Log("Falta collider");
            }
        }
    }
}
