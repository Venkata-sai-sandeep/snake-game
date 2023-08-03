using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Snake_controller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private int player_score = 0;
    private bool canStart = false;
    private float snake_speed = 2.5f;
    private Vector3 moveDirection = Vector3.forward;
    public Object_instantiater object_instantiater;
    private void Start()
    {
        score.text = "Score : " + player_score.ToString();
        object_instantiater = transform.parent.GetComponent<Object_instantiater>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector3.back)
        {
            moveDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector3.forward)
        {
            moveDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector3.right)
        {
            moveDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector3.left)
        {
            moveDirection = Vector3.right;
        }
        transform.position += moveDirection * snake_speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Good_egg"))
        {
            player_score += 10;
            score.text = "Score : " + player_score.ToString();
            Destroy(other.gameObject);
            object_instantiater.instantiateObject();
        }
        else if(other.tag.Equals("Poison_egg"))
        {
            player_score -= 5;
            score.text = "Score : " + player_score.ToString();
            Destroy(other.gameObject);
            object_instantiater.instantiateObject();
        }
    }
}
