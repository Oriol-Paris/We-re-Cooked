using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomerSpawnPoint
{
    public Transform pos;
    public GameObject customerO;
}


public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;

    public GameObject customerPrefab;
    public Transform spawnPoint, freeSpawn;
    public List<Transform> spawnPoints = new List<Transform>();

    public int maxCustomers = 3;
    public float spawnInterval = 10f;
    public float customerWaitTime = 30f;

    public ReceipeContainer recipes;

    [SerializeField]
    private List<Customer> customers = new List<Customer>();
    private float spawnTimer;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && customers.Count < maxCustomers)
        {
            SpawnCustomer(this.spawnPoint);
            spawnTimer = 0;
        }
    }

    void SpawnCustomer(Transform spawnPoint)
    {
        if (customerPrefab != null && spawnPoint != null)
        {
            Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void RegisterCustomer(Customer customer)
    {
        if (!customers.Contains(customer))
        {
            recipes.ResetReceipe();
            //recipes.RefreshHudList();
            customer.SetRecipie(recipes.currentR);
            customers.Add(customer);
        }
    }

    public List<Customer> GetAllCustomers()
    {
        return customers;
    }

    public void RemoveCustomer(Customer c)
    {
        if (customers.Contains(c))
        {
            customers.Remove(c);
        }
    }

    public Customer GetFirstWaitingCustomer()
    {
        return customers.Count > 0 ? customers[0] : null;
    }

    public void RenewCustomer(Customer c)
    {
        freeSpawn = c.GetPos();
        RemoveCustomer(c);
        SpawnCustomer(freeSpawn);
        freeSpawn = null;
    }
}
