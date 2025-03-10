using System.Collections;
using UnityEngine;

public class MaizInicioController : MonoBehaviour
{
    public GameObject prefabToInstantiate1; 

    [SerializeField] public GameObject currentPrefabInstance; 

    private float wait = 5f;

    void Start()
    {
        if (prefabToInstantiate1 == null)
        {
            Debug.LogError("No se han asignado prefabs para instanciar");
            return;
        }

        StartCoroutine(InstantiatePrefab1());
    }

    private IEnumerator InstantiatePrefab1()
    {
        Debug.Log("Esperando 5 segundos.");

        yield return new WaitForSeconds(wait);

        if (prefabToInstantiate1 != null)
        {
            Debug.Log("Instanciando el primer prefab");

            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance);
                Debug.Log("Prefab anterior destruido");
            }

            currentPrefabInstance = Instantiate(
                prefabToInstantiate1,
                new Vector3(transform.position.x, transform.position.y, -4f),
                Quaternion.identity
            );
        }
        else
        {
            Debug.LogError("No se ha asignado el primer prefab para instanciar");
        }

    }
}
