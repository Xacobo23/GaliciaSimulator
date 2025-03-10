using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PCShopInteraction : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI maizeText;
    public TextMeshProUGUI seedText;
    public int maizeSellPrice = 5;   
    public int seedBuyPrice = 2;

    // Variables internas
    private int playerMoney = 0;
    private int playerMaize = 0;
    private int playerSeeds = 0;

    void Start()
    {
        // Inicializa las variables
        playerMoney = GameManager.Instance.money;
        playerMaize = GameManager.Instance.maize;
        playerSeeds = GameManager.Instance.seeds;

        UpdateUI();
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

                if (clickedObject.CompareTag("SellMaiz"))
                {
                    SellMaize();
                }
                else if (clickedObject.CompareTag("BuySeeds"))
                {
                    BuySeeds();
                }
                else if (clickedObject.CompareTag("Exit"))
                {
                    GoToPC();
                }
            }
        }
    }

    // Función para vender maíz
    private void SellMaize()
    {
        if (playerMaize > 0)
        {
            playerMaize--;
            playerMoney += maizeSellPrice;
            GameManager.Instance.maize = playerMaize;
            GameManager.Instance.money = playerMoney;

            UpdateUI();
        }
        else
        {
            Debug.Log("non hai maiz");
        }
    }

    // Función para comprar semillas
    private void BuySeeds()
    {
        if (playerMoney >= seedBuyPrice)
        {
            playerMoney -= seedBuyPrice;
            playerSeeds++;
            GameManager.Instance.money = playerMoney;
            GameManager.Instance.seeds = playerSeeds;

            UpdateUI();
        }
        else
        {
            Debug.Log("faltan semillas");
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = playerMoney.ToString();

        if (maizeText != null)
            maizeText.text = playerMaize.ToString();

        if (seedText != null)
            seedText.text = playerSeeds.ToString();

        if (playerMoney >= 100)
        {
            LoadNewScene();
        }
    }

    private void GoToPC()
    {
        SceneManager.LoadScene("HouseScene");
        Debug.Log("Entraste al PC.");
    }


    private void LoadNewScene()
    {
        SceneManager.LoadScene("FinalScene");
        Debug.Log("Has alcanzado 100 monedas. Cambiando a la nueva escena.");
    }
}
