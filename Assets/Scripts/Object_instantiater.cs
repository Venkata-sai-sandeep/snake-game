using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_instantiater : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject good_egg, poison_egg;
    [SerializeField]
    private int good_egg_count, poison_egg_count;
    public SnakeController snake_controller;
    private GameObject prefabToInstantiate;
    public Vector3 minBounds;
    public Vector3 maxBounds;
    //private GameObject snake;
    public void Start()
    {
        good_egg_count = 0;
        poison_egg_count = 0;
        //snake = transform.GetChild(0).transform.gameObject;
        instantiateObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void instantiateObject()
    //{
    //    if(good_egg_count <= 10 && poison_egg_count <= 5)
    //    {
    //        int randomIndex = Random.Range(0, 3);
    //        if (randomIndex == 0)
    //        {
    //            prefabToInstantiate = poison_egg;
    //            poison_egg_count++;
    //        }
    //        else
    //        {
    //            prefabToInstantiate = good_egg;
    //            good_egg_count++;
    //        }
    //    }
    //    else if(poison_egg_count > 5)
    //    {
    //        prefabToInstantiate = good_egg;
    //        good_egg_count++;
    //    }
    //    else if(good_egg_count>10)
    //    {
                
    //    }


    //    Vector3 randomPosition = new Vector3(
    //            Random.Range(minBounds.x, maxBounds.x),
    //            Random.Range(minBounds.y, maxBounds.y),
    //            Random.Range(minBounds.z, maxBounds.z)
    //        );

    //    Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);
    //    if(prefabToInstantiate.tag == "Poison_egg")
    //    {
    //        StartCoroutine(destroyGreenEgg(5f));
    //    }
    //    //prefabToInstantiate.transform.SetParent();
    //}

    public void instantiateObject()
    {
        if(poison_egg_count >= 4 && good_egg_count >= 9)
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
