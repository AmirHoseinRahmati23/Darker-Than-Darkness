using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObj;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        Instantiate(spawnObj);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
