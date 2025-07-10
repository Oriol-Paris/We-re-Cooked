using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public static PlateManager instance;

    [Header("Variables")]
    [SerializeField] private int maxPlates;
    [SerializeField] private float plateSpawnSeparation = 0.3f;

    [Header("Plate Prefab")]
    [SerializeField] private GameObject platePrefab;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); return; }
    }

    private void Start()
    {
        for (int i = 0; i < maxPlates; i++)
        {
            Instantiate(platePrefab, new Vector3(transform.position.x, transform.position.y + i * plateSpawnSeparation, transform.position.z), Quaternion.identity);
        }
    }

    public void SpawnPlate()
    {
        Instantiate(platePrefab, new Vector3(transform.position.x, transform.position.y + (maxPlates - 1) * plateSpawnSeparation, transform.position.z), Quaternion.identity);
    }
}
