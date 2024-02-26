using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float respawnSeconds;

    private Rigidbody enemy;

    private void Start()
    {
        enemy = GetComponent<Rigidbody>();
        StartCoroutine(EnemySpawner());
    }
    public IEnumerator EnemySpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnSeconds);
            enemy.transform.position = new Vector3(Random.Range(-30f, 51f), transform.position.y, Random.Range(-30f, 51f));
        }  
    }
}
