using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int seeds = 99;  
    public int maize = 0;  
    public int money = 0;

    void Awake()
    {
        SaveManager.ResetSaveData();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        LoadGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("seeds", seeds);
        PlayerPrefs.SetInt("maize", maize);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save(); 
    }

    public void LoadGame()
    {
        seeds = PlayerPrefs.GetInt("seeds", 10); 
        maize = PlayerPrefs.GetInt("maize", 0);
        money = PlayerPrefs.GetInt("money", 90);
    }

    public void ResetGameValues()
    {
        seeds = 10;
        maize = 0;
        money = 90;
        SaveGame();
    }
}
