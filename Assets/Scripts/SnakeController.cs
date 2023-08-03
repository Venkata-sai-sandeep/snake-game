using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SnakeController : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private int player_score = 0;
    public float snake_speed = 2;
    public int Gap = 40;
    private bool canStart = false;
    [SerializeField]
    private bool gameOver = false;
    private Vector3 moveDirection = Vector3.forward;
    public Object_instantiater object_instantiater;
    public GameObject endCanavas;

    public GameObject bodyParts;

    
    private List<GameObject> snake_body = new List<GameObject>();
    private List<Vector3> bodyPartsHistory = new List<Vector3>();

    
    void Start() {
        score.text = "Score : " + player_score.ToString();
        gameObject.transform.position = new Vector3(0, 0.31f, 0);
        gameOver = false;
        player_score = 0;
    }

    void Update() 
    {
        if (player_score < 0)
        {
            gameCompleted();
        }
        if (!gameOver)
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

            bodyPartsHistory.Insert(0, transform.position);


            int index = 0;
            foreach (var body in snake_body)
            {
                Vector3 point = bodyPartsHistory[Mathf.Clamp(index * Gap, 0, bodyPartsHistory.Count - 1)];
                Vector3 moveDirection = point - body.transform.position;
                body.transform.position += moveDirection * snake_speed * Time.deltaTime;
                body.transform.LookAt(point);
                index++;
            }


        }
    }

    private void GrowSnake() {
       
        GameObject body = Instantiate(bodyParts);
        snake_body.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Good_egg")
        {
            player_score += 10;
            score.text = "Score : " + player_score.ToString();
            Destroy(other.gameObject);
            GrowSnake();
            object_instantiater.instantiateObject();
        }
        else if (other.tag.Equals("Poison_egg"))
        {
            player_score -= 5;
            score.text = "Score : " + player_score.ToString();
            Destroy(other.gameObject);
            if (snake_body.Count >= 2)
            {
                Destroy(snake_body[snake_body.Count - 1]);
                snake_body.RemoveAt(snake_body.Count - 1);
                Destroy(snake_body[snake_body.Count - 1]);
                snake_body.RemoveAt(snake_body.Count - 1);
                Debug.Log(snake_body.Count);
            }
            else gameCompleted();
                
            //object_instantiater.instantiateObject();
        }
        else if (other.tag.Equals("Border"))
        {
            gameCompleted();
        }
    }
    public void onClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Transform parentTransform = transform.parent;
        GameObject parentGameobject = parentTransform.gameObject;
        parentGameobject.GetComponent<Object_instantiater>().enabled = false;
        parentGameobject.GetComponent<Object_instantiater>().enabled = true;
        GameObject[] good_eggs = GameObject.FindGameObjectsWithTag("Good_egg");
        GameObject[] poison_eggs = GameObject.FindGameObjectsWithTag("Poison_egg");
        GameObject[] snake = GameObject.FindGameObjectsWithTag("Snake");
        foreach (GameObject go in good_eggs)
            Destroy(go);
        foreach (GameObject go in poison_eggs)
            Destroy(go);
        foreach (GameObject go in snake)
            Destroy(go);
        Start();
        object_instantiater.Start();
    }

    public void gameCompleted()
    {
        gameOver = true;
        endCanavas.SetActive(true);
        endCanavas.transform.GetChild(1).gameObject.SetActive(true);
        endCanavas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Game over. You have scored " + player_score.ToString() + " Click on the restart button to restart the game.";
    }
}