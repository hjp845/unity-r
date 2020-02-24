using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public BulletStat bulletStat { get; set; }
    public GameObject character;

    public float activeTime = 3.0f;

    public BulletBehavior()
    {
        bulletStat = new BulletStat(0, 0);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    // 이 객체가 활성화 되었을 때, 자동으로 호출되는 유니티 내장함수
    // 현재 총알은 오브젝트풀링이 적용된 상태. 그래서 Start에 넣는것보다 OnEable에 넣는것이 옳음
    private void OnEnable()
    {
        StartCoroutine(BulletInactive(activeTime));
    }

    IEnumerator BulletInactive(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
	
	void Update () {
        transform.Translate(Vector2.right * bulletStat.speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            other.GetComponent<MonsterStat>().attacked(bulletStat.damage);
        }   
    }
}
