using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy; //crawler
    public GameObject pparent; //pages
    public GameObject houseTrigger;
    public int xPos;
    public int zPos;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemy.SetActive(false); //make orginal enemy invisible
        for (int i=1; i < pparent.transform.childCount; i++)
        {
            pparent.transform.GetChild(i).gameObject.SetActive(false);
        }
        houseTrigger.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            houseTrigger.SetActive(true);
            enemy.SetActive(true);
            for (int i = 0; i < pparent.transform.childCount; i++)
            {
                pparent.transform.GetChild(i).gameObject.SetActive(true);
            }
            StartCoroutine(EnemyDrop());
        }
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 15)
        {
            xPos = Random.Range(-75, -15);
            zPos = Random.Range(547, 700);
            Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return
            enemyCount += 1;
        }
        enemyCount = 0;
        while (enemyCount < 15)
        {
            xPos = Random.Range(64, 100);
            zPos = Random.Range(564, 683);
            Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return
            enemyCount += 1;
        }
        Destroy(transform.gameObject); //once collided box is destroyed
    }

}
