using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour {


    // 이렇게 선언하고 유니티에서 사이즈 설정..
    public List<GameObject> respawnSpotList;

    public GameObject monster1Prefab;
    public GameObject monster2Prefab;
    private GameObject monsterPrefab;

    private int spawnCount = 0;
    private IEnumerator coroutine;

    void Start () {
        // gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // gameManager = new GameManager();
        monsterPrefab = monster1Prefab;
        coroutine = process();
        StartCoroutine(coroutine);
	}

    void Create()
    {
        int index = Random.Range(0, 4);
        GameObject respawnSpot = respawnSpotList[index];
        Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
        GameManager.instance.monsterAddCount++;
        spawnCount++;
    }

    IEnumerator process()
    {
        while (true)
        {
            if (GameManager.instance.round > GameManager.instance.totalRound) StopCoroutine(coroutine);
            if (spawnCount < GameManager.instance.spawnNumber)
            {
                Create();
                Debug.Log("몬스터생성완료");
            }
            Debug.Log(GameObject.FindGameObjectsWithTag("Monster"));
            if(spawnCount == GameManager.instance.spawnNumber &&
                GameObject.FindGameObjectWithTag("Monster") == null)
            {
                Debug.Log("라운드클리어!");
                if(GameManager.instance.totalRound == GameManager.instance.round)
                {
                    GameManager.instance.gameClear();
                    // 게임클리어하고 += 1 추가해주는 이유는 저기 위에서 StopCoroutine하려고
                    GameManager.instance.round += 1;
                }
                else
                {
                    GameManager.instance.clearRound();
                    spawnCount = 0;
                    if(GameManager.instance.round == 4)
                    {
                        monsterPrefab = monster2Prefab;
                        GameManager.instance.spawnTime = 2.0f;
                        GameManager.instance.spawnNumber = 10;
                    }
                }
            }
            // 몬스터나오기전에 종 땡땡땡 할때 그 기다리는 시간
            if (spawnCount == 0) yield return new WaitForSeconds(GameManager.instance.roundReadyTime);
            // 스폰 시간이 정해져있음
            else yield return new WaitForSeconds(GameManager.instance.spawnTime);
        }
    }
	
	//void Update () {
	//	if(GameManager.instance.round <= GameManager.instance.totalRound)
 //       {
 //           float timeGap = Time.time - lastSpawnTime;
 //           if(((spawnCount == 0 && timeGap > GameManager.instance.roundReadyTime) // 새 라운드가 시작
 //               || timeGap > GameManager.instance.spawnTime)
 //               && spawnCount < GameManager.instance.spawnNumber)
 //           {
 //               lastSpawnTime = Time.time;
 //               int index = Random.Range(0, 4);
 //               GameObject respawnSpot = respawnSpotList[index];
 //               Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
 //               GameManager.instance.monsterAddCount++;
 //               spawnCount += 1;
 //           }
 //           if(spawnCount == GameManager.instance.spawnNumber &&
 //              GameObject.FindGameObjectWithTag("Monster") == null)
 //           {
 //               if(GameManager.instance.totalRound == GameManager.instance.round)
 //               {
 //                   GameManager.instance.gameClear();
 //                   GameManager.instance.round += 1;
 //                   return;
 //               }
 //               GameManager.instance.clearRound();
 //               spawnCount = 0;
 //               lastSpawnTime = Time.time;

 //               if(GameManager.instance.round == 4)
 //               {
 //                   monsterPrefab = monster2Prefab;
 //                   GameManager.instance.spawnTime = 2.0f;
 //                   GameManager.instance.spawnNumber = 10;
 //               }
 //           }
 //       }
	//}
}
