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
    private GameObject prefabToInstantiate;
    public Vector3 minBounds;
    public Vector3 maxBounds;

    void Start()
    {
        good_egg_count = 0;
        poison_egg_count = 0;
        instantiateObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instantiateObject()
    {
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0)
        {
            prefabToInstantiate = poison_egg;
            poison_egg_count++;
        }
        else
        {
            prefabToInstantiate = good_egg;
            good_egg_count++;
        }

        Vector3 randomPosition = new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );

        Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);
        if(prefabToInstantiate.tag == "Good_egg")
        {
            StartCoroutine(destroyGreenEgg(5f));
        }
        //prefabToInstantiate.transform.SetParent();
    }

    IEnumerator destroyGreenEgg(float x)
    {
        int temp_green_cnt = good_egg_count;
        int temp_red_cnt = poison_egg_count;
        yield return new WaitForSeconds(x);
        if(good_egg_count == temp_green_cnt && poison_egg_count == temp_red_cnt)
        {
            Destroy(prefabToInstantiate);
            instantiateObject();
        }
    }
}
