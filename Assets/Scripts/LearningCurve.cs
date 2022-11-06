using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    private Transform camTransform;
    public GameObject directionalLight;
    private Transform lightTransform;

    // Start is called before the first frame update
    void Start()
    {

        //directionalLight = GameObject.Find("Directional Light");
        lightTransform = directionalLight.GetComponent<Transform>();
        //Debug.Log(lightTransform.localPosition);


        camTransform = this.GetComponent<Transform>();
        //Debug.Log(camTransform.localPosition);

        Character hero = new Character();
        Character hero2 = hero;
        hero2.name = "Sir Von Mandellopino";
        //hero.printStatsInfo();
        //hero2.printStatsInfo();
        Character heroine = new Character("Agatha");
        //heroine.printStatsInfo();
        Weapon huntingBow = new Weapon("Hunting Bow", 105);
        Weapon warBow = huntingBow;
        warBow.name = "War Bow";
        warBow.damage = 130;
        //huntingBow.printWeaponStats();
        //warBow.printWeaponStats();
        Paladin knight = new Paladin("Sir Arthur", huntingBow);
        //knight.printStatsInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
