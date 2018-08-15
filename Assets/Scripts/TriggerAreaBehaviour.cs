﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaBehaviour : MonoBehaviour
{
    int goalCubes;
    int actualCubes = 0;
    bool particlesEnabled = false;
    float delay = 0;

    // Use this for initialization
    void Start()
    {
        goalCubes = GameObject.Find("Shapes").GetComponent<ShapeGenerator>().NumberOfCubes;
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        Vector3 Scale = GameObject.Find("Shapes").GetComponent<ShapeGenerator>().Dimensions;
        transform.localScale = Scale;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x + Scale.y / 2, transform.localPosition.z);
    }

    void ChangeAreaColor()  //from red to green
    {
        float value = (float)actualCubes / goalCubes;
        Color targetColor = new Color(Mathf.Clamp01(2f - value * 2), Mathf.Clamp01(value * 2), 0, 0.25f);
        GetComponent<Renderer>().material.color = targetColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Cube")
            return;
        
        actualCubes++;
        ChangeAreaColor();

        if (actualCubes == goalCubes)
        {
            var particles = GameObject.Find("Particles").GetComponentsInChildren<ParticleSystem>();
            foreach (var particle in particles)
                particle.Play();

            particlesEnabled = true;

            StartCoroutine("WinGame");
        }

        //Debug.Log(actualCubes + "/" + goalCubes);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "Cube")
            return;

        actualCubes--;
        ChangeAreaColor();
        Debug.Log((float)actualCubes / goalCubes);

        //Debug.Log(actualCubes + "/" + goalCubes);
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(Game.WinScreenDelay);
        Game.ShowWinScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (particlesEnabled)
        {
            delay -= Time.deltaTime;
            //Debug.Log(delay);

            if (delay <= 0)
            {
                var particles = GameObject.Find("Particles").GetComponentsInChildren<ParticleSystem>();
                foreach (var particle in particles)
                    particle.startColor = Random.ColorHSV(0, 1);
                delay = 2;
            }
        }
    }
}
