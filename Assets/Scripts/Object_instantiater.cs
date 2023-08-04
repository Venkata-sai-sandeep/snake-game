using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_instantiater : MonoBehaviour
{
   
    
    [SerializeField]
    private GameObject good_egg, poison_egg;
    [SerializeField]
    private int good_egg_count, poison_egg_count;
    public SnakeController snake_controller;
    private GameObject prefabToInstantiate;
    public Vector3 minBounds;
    public Vector3 maxBounds;
    public void Start()
    {
        good_egg_count = 0;
        poison_egg_count = 0;
        //instantiateObject();
    }

    public void instantiateObject()
    {
        if(poison_egg_count > 4 && good_egg_count > 9)
        { 
            snake_controller.gameCompleted();
            return;
        }
        int randomIndex = Random.Range(0, 2);
        Vector3 randomPosition = new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );
        if (randomIndex == 0 && poison_egg_count < 5)
        {


            prefabToInstantiate = Instantiate(poison_egg, randomPosition, Quaternion.identity);
            poison_egg_count++;
            if (prefabToInstantiate.tag == "Poison_egg")
            {
                StartCoroutine(destroyEgg(5f));
            }

        }
        else
        {
            prefabToInstantiate = good_egg;
            if(good_egg_count<10)
            {
                Instantiate(good_egg, randomPosition, Quaternion.identity);
                good_egg_count++;
            }
            else
            {
                instantiateObject();
            }
        }
        
    }
    IEnumerator destroyEgg(float x)
    {
        yield return new WaitForSeconds(x);
        DestroyImmediate(prefabToInstantiate,true);
        instantiateObject();
        
    }
}
