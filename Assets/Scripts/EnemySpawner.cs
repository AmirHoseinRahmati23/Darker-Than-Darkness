using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private LogicManager Logic;
    [SerializeField]
    private GameObject enemy;
    private bool reached = false;
    private float dayTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag(nameof(Logic)).GetComponent<LogicManager>();
        timer = 0;
        dayTime = Logic.DayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Logic.gameIsActive)
        {
            SpawnTimer();
        }
    }

    private void SpawnTimer()
    {
        if (!reached)
        {

            if (timer <= dayTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                reached = true;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
