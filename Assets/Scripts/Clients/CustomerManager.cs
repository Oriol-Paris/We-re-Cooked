using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;

    public GameObject customerPrefab;
    public Transform spawnPoint;
    public int maxCustomers = 3;
    public float spawnInterval = 10f;
    public float customerWaitTime = 30f;

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
            SpawnCustomer();
            spawnTimer = 0;
        }

        foreach (var customer in customers)
        {
            customer.UpdateWait(Time.deltaTime);
            if (customer.isAngry)
            {
                //Hacer que se enfaden
            }
        }
    }

    void SpawnCustomer()
    {

        if (customerPrefab != null && spawnPoint != null)
        {
            Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
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
}
