﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartSpawner : MonoBehaviour
{
    //organ prefabs
    public GameObject heartPrefab;
    public GameObject lungPrefab;
    public GameObject brainPrefab;

    //limb prefabs
    public GameObject armPrefab;
    public GameObject legPrefab;
    public GameObject torsoPrefab;
    public GameObject headPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject SpawnHeart(string name)
    {
        GameObject bodyPart = Instantiate(heartPrefab);
        bodyPart.name = name;
        return bodyPart;
    }

    public GameObject SpawnLung(string name)
    {
        GameObject bodyPart = Instantiate(lungPrefab);
        bodyPart.name = name;
        return bodyPart;
    }

    public GameObject SpawnBrain(string name)
    {
        GameObject bodyPart = Instantiate(brainPrefab);
        bodyPart.name = name;
        return bodyPart;
    }
    public GameObject SpawnArm(string name)
    {
        GameObject bodyPart = Instantiate(armPrefab);
        bodyPart.name = name;
        return bodyPart;
    }

    public GameObject SpawnLeg(string name)
    {
        GameObject bodyPart = Instantiate(legPrefab);
        bodyPart.name = name;
        return bodyPart;
    }

    public GameObject SpawnTorso(string name)
    {
        GameObject bodyPart = Instantiate(torsoPrefab);
        bodyPart.name = name;
        return bodyPart;
    }

    public GameObject SpawnHead(string name)
    {
        GameObject bodyPart = Instantiate(headPrefab);
        bodyPart.name = name;
        return bodyPart;
    }

}
