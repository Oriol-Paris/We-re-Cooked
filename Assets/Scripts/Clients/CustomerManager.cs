using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomerRegister
{
    public Transform pos;
    public GameObject customerO;
    public ReceipeContainer recipes;
    public CalculateScore score;
}


public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;

    public GameObject customerPrefab;
    public List<CustomerRegister> spawnPoints = new List<CustomerRegister>();

    public int maxCustomers = 3;
    public float spawnInterval = 10f;
    public float customerWaitTime = 30f;

    //[SerializeField]
    //private List<Customer> customers = new List<Customer>();
    private float spawnTimer;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnCustomer();
            spawnTimer = 0;
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if (spawnPoints[i].customerO == null)
                {
                    var c = Instantiate(customerPrefab, transform.position, spawnPoints[i].pos.rotation);
                    spawnPoints[i].customerO = c;
                    c.transform.position = spawnPoints[i].pos.position;
                    spawnPoints[i].recipes.ResetReceipe();
                    spawnPoints[i].score.customerIndex = i;
                    spawnPoints[i].customerO.GetComponent<Customer>().scoreTime = spawnPoints[i].score.timeBeforeResetScore;
                    break;
                }
            }
        }
    }

    //public void RegisterCustomer(Customer customer)
    //{
    //    if (!customers.Contains(customer))
    //    {
    //        recipes.ResetReceipe();
    //        customer.SetRecipie(recipes.currentR);
    //    }
    //}

    //public List<Customer> GetAllCustomers()
    //{
    //    return customers;
    //}

    //public void RemoveCustomer(Customer c)
    //{
    //    if (customers.Contains(c))
    //    {
    //        customers.Remove(c);
    //    }
    //}

    //public Customer GetFirstWaitingCustomer()
    //{
    //    return customers.Count > 0 ? customers[0] : null;
    //}
}