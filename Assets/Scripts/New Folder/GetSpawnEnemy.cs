using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpawnEnemy : MonoBehaviour
{
    private float time;
    [SerializeField] private float SetTime;
    [SerializeField] GameObject spawner;

    // Update is called once per frame
    void Update()
    {
        if (time < SetTime)
        {
            time += Time.deltaTime;
        }        
        if(time > SetTime)
        {
            if (spawner)
            {
                spawner.SetActive(true);
            }
            else return;
            
        }
    }
}
