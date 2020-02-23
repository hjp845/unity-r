using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 변수의 저장기능만 하는 클래스는 VO value object
public class BulletStat {

    public float speed { get; set; }
    public int damage { get; set; }

    public BulletStat(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
}
