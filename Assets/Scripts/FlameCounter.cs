using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class FlameCounter : MonoBehaviour
{
    public float fireDuration; //in seconds. 
    private float counter;
    private Vector3 startingFireValues;
    private float startingLightRange;
    private Light light;
    private bool deacreaseFire;
    public float EXPLOSION_FACTOR = 2;
    public float EXPLOSION_ANIMATION_TIME = 3;
    public Color EXPLOSION_COLOR;
    private Color DEFAULT_COLOR = Color.white;

    void Start()
    {
        deacreaseFire = true;
        light = transform.GetChild(1).gameObject.GetComponent<Light>(); // 1 is the index of light child.
        counter = 0;
        startingFireValues = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        startingLightRange = light.range;
        ChangeColor(DEFAULT_COLOR);
    }

    void Update()
    {
        if (deacreaseFire)
        {
            counter += Time.deltaTime;

            Vector3 shortenFireFactor = transform.localScale - startingFireValues * (1f / fireDuration) * Time.deltaTime;
            light.range -= startingLightRange * (1f / fireDuration) * Time.deltaTime;
            transform.localScale = new Vector3(Math.Max(shortenFireFactor.x, 0), Math.Max(shortenFireFactor.y, 0), Math.Max(shortenFireFactor.z, 0));
            if (counter >= fireDuration)
            {
                print("GAME OVER!");
            }
        }
    }
    
    private IEnumerator ExplosionEnumarator()
    {
        deacreaseFire = false;
        float startRange = light.range;
        Vector3 startScale = transform.localScale;
        Color startColor = light.color;

        float t = 0;
        while (t <= EXPLOSION_ANIMATION_TIME)
        {
            light.range = Mathf.Lerp(startRange, startingLightRange * EXPLOSION_FACTOR, t / EXPLOSION_ANIMATION_TIME);
            transform.localScale = Vector3.Lerp(startScale, startingFireValues * EXPLOSION_FACTOR, t / EXPLOSION_ANIMATION_TIME);

            ChangeColor(Color.Lerp(startColor, EXPLOSION_COLOR, t / EXPLOSION_ANIMATION_TIME));

            t += Time.deltaTime;
            yield return null;
        }

    }

    public void Explosion()
    {
        StartCoroutine(ExplosionEnumarator());
    }

    private void ChangeColor(Color c)
    {
        light.color = c;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
        GetComponent<Renderer>().material.SetColor("_Color", c);
    }
}
