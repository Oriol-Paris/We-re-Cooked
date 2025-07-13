using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {
    public List<GameObject> objL;

    public List<GameObject> currObjL;

    public int maxObj = 2;

    public float spawnTime = 2;


    void OnEnable() {
        InitSpawn();

        StartCoroutine(Spawn());
    }
    void OnDisable() {
        StopAllCoroutines();
    }

    public void InitSpawn() {
        while (currObjL.Count < maxObj) {
            int rand = Random.Range(0, objL.Count);
            var tmp = Instantiate(objL[rand], transform.position, transform.rotation);
            currObjL.Add(tmp);
        }
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(Spawn());

        for (int i = currObjL.Count - 1; i >= 0; i--) {
            if (currObjL[i] == null)
                currObjL.RemoveAt(i);
        }

        if (currObjL.Count < maxObj) {
            int rand = Random.Range(0, objL.Count);
            var tmp = Instantiate(objL[rand], transform.position, transform.rotation);
            currObjL.Add(tmp);
        }

    }

}
