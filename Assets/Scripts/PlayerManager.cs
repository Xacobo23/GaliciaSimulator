using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;



public class PlayerManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileToColor;
    public TextMeshProUGUI seedTextUI;
    public TextMeshProUGUI maizeTextUI;
    public TextMeshProUGUI moneyTextUI;
    public GameObject borderObject;
    public Color borderInsideColor = Color.blue;
    public Color borderOutsideColor = Color.red;
    public Vector2Int interactionRange = new Vector2Int(3, 3);
    public GameObject prefabToPlace;
    public GameObject maizePrefab;

    private SpriteRenderer borderRenderer;
    private Dictionary<SerializableVector3Int, bool> plantedCells = new Dictionary<SerializableVector3Int, bool>();

    public AudioClip plantSound;
    public AudioClip collectSound;
    private AudioSource audioSource;

    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();

        borderRenderer = borderObject.GetComponent<SpriteRenderer>();
        if (borderRenderer == null)
        {
            Debug.LogError("faltalle o spriterenderer");
            return;
        }

        borderRenderer.enabled = false;

        plantedCells = SaveManager.LoadPlantedCells();
        foreach (var cell in plantedCells)
        {
            if (cell.Value)
            {
                PlantPrefabInCell(cell.Key.ToVector3Int());
            }
        }
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        Vector3Int playerCellPos = tilemap.WorldToCell(transform.position);
        Vector3Int cellDiff = gridPos - playerCellPos;

        if (Mathf.Abs(cellDiff.x) <= interactionRange.x && Mathf.Abs(cellDiff.y) <= interactionRange.y)
        {
            TileBase currentTile = tilemap.GetTile(gridPos);

            if (currentTile != null)
            {
                bool canCollectMaize = CheckForMaizeInCell(gridPos);

                if (canCollectMaize)
                {
                    SetBorderColor(borderInsideColor);
                }
                else if (GameManager.Instance.seeds > 0 && !plantedCells.ContainsKey(new SerializableVector3Int(gridPos)))
                {
                    SetBorderColor(borderInsideColor);
                }
                else if (plantedCells.ContainsKey(new SerializableVector3Int(gridPos)) && plantedCells[new SerializableVector3Int(gridPos)] == true)
                {
                    SetBorderColor(Color.red);
                }
                else
                {
                    SetBorderColor(borderOutsideColor);
                }

                ShowCellBorder(gridPos);
            }
            else
            {
                SetBorderColor(Color.red);
                ShowCellBorder(gridPos);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (CheckForMaizeInCell(gridPos))
                {
                    CollectMaize(gridPos);
                }
                else if (GameManager.Instance.seeds > 0 && !plantedCells.ContainsKey(new SerializableVector3Int(gridPos)) && currentTile != null)
                {
                    PlantPrefabInCell(gridPos);
                    RemoveSeeds(1);
                    plantedCells[new SerializableVector3Int(gridPos)] = true;
                }
            }
        }
        else
        {
            SetBorderColor(Color.red);
            ShowCellBorder(gridPos);
        }

        UpdateSeedUI();
        UpdateMaizeUI();
        UpdateMoneyUI();
    }

    public void AddSeeds(int amount)
    {
        GameManager.Instance.seeds += amount;
        GameManager.Instance.seeds = Mathf.Max(GameManager.Instance.seeds, 0);
    }

    public void RemoveSeeds(int amount)
    {
        GameManager.Instance.seeds -= amount;
        GameManager.Instance.seeds = Mathf.Max(GameManager.Instance.seeds, 0);
    }

    private void UpdateSeedUI()
    {
        if (seedTextUI != null)
        {
            seedTextUI.text = GameManager.Instance.seeds.ToString();
        }
        else
        {
            Debug.LogError("error");
        }
    }


    private void UpdateMaizeUI()
    {
        if (maizeTextUI != null)
        {
            maizeTextUI.text = GameManager.Instance.maize.ToString();
        }
    }

    private void UpdateMoneyUI()
    {
        if (moneyTextUI != null)
        {
            moneyTextUI.text = GameManager.Instance.money.ToString();
        }
    }

    private void ShowCellBorder(Vector3Int gridPos)
    {
        Vector3 cellCenter = tilemap.GetCellCenterWorld(gridPos);
        Vector3 cellSize = tilemap.cellSize;

        borderObject.transform.position = new Vector3(cellCenter.x, cellCenter.y, -4);
        borderRenderer.enabled = true;

        Vector3 borderScale = new Vector3(cellSize.x * 0.8f, cellSize.y * 0.8f, 1);
        borderObject.transform.localScale = borderScale;
    }

    private void SetBorderColor(Color color)
    {
        borderRenderer.color = color;
    }

    private void PlantPrefabInCell(Vector3Int gridPos)
    {
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(gridPos);

        if (prefabToPlace != null)
        {
            GameObject maiz = Instantiate(prefabToPlace, cellWorldPos, Quaternion.identity);
            PlaySound(plantSound);
        }
        else
        {
            Debug.LogError("error no PlantPrefabInCell");
        }
    }

    private bool CheckForMaizeInCell(Vector3Int gridPos)
    {
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(gridPos);
        Collider2D[] colliders = Physics2D.OverlapPointAll(cellWorldPos);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("MaizFinal"))
            {
                return true;
            }
        }

        return false;
    }

    private void CollectMaize(Vector3Int gridPos)
    {
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(gridPos);
        Collider2D[] colliders = Physics2D.OverlapPointAll(cellWorldPos);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("MaizFinal"))
            {
                Destroy(collider.gameObject);
                GameManager.Instance.maize++;
                UpdateMaizeUI();

                // Marcar celda
                SerializableVector3Int key = new SerializableVector3Int(gridPos);
                if (plantedCells.ContainsKey(key))
                {
                    plantedCells[key] = false;
                    plantedCells.Remove(key);
                }

                SaveManager.SavePlantedCells(plantedCells);
                PlaySound(collectSound);
                break;
            }
        }
    }


    void OnDisable()
    {
        SaveManager.SavePlantedCells(plantedCells);
    }

    void OnApplicationQuit()
    {
        SaveManager.SavePlantedCells(plantedCells);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

}
