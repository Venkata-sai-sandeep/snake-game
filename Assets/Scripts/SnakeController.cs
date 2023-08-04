using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SnakeController : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI score;
    public TextMeshProUGUI levelSelection;
    [SerializeField]
    private int player_score;
    public float snake_speed;
    public int Gap = 15;
    private bool canStart = false;
    [SerializeField]
    private bool gameOver = false;
    private Vector3 moveDirection = Vector3.forward;
    public Object_instantiater object_instantiater;
    public GameObject endCanavas;
    public GameObject levelSelectionCanvas;
    public GameObject bodyParts;
    public GameObject parentObject;

    
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
       
        if(!levelSelection.text.Equals("Select Level"))
        {
            if(!canStart)
            {
                levelSelectionCanvas.SetActive(false);
                endCanavas.SetActive(true);
                endCanavas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press the space button to start the game.";
                endCanavas.transform.GetChild(1).gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    endCanavas.transform.GetChild(1).gameObject.SetActive(true);
                    endCanavas.SetActive(false);
                    canStart = true;
                    object_instantiater.instantiateObject();
                }
            }
            
            else
            {
                parentObject.GetComponent<Object_instantiater>().enabled = true;
                if (levelSelection.text.Equals("Easy"))
                {
                    snake_speed = 2f;
                    Debug.Log(Gap.ToString());
                }
                else if (levelSelection.text.Equals("Medium"))
                {
                    snake_speed = 4f;
                    Gap = 10;
                    Debug.Log(Gap.ToString());
                }
                else if (levelSelection.text.Equals("Hard"))
                {
                    snake_speed = 6f;
                    Gap = 7;
                    Debug.Log(Gap.ToString());
                }

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
                        Vector3 points = bodyPartsHistory[Mathf.Clamp(index * Gap, 0, bodyPartsHistory.Count - 1)];
                        Vector3 moveDirection = points - body.transform.position;
                        body.transform.position += moveDirection * snake_speed * Time.deltaTime;
                        body.transform.LookAt(points);
                        index++;
                    }


                }
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
                
        }
        else if (other.tag.Equals("Border"))
        {
            gameCompleted();
        }
    }
    public void onClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameCompleted()
    {
        gameOver = true;
        endCanavas.SetActive(true);
        endCanavas.transform.GetChild(1).gameObject.SetActive(true);
        endCanavas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Game over. You have scored " + player_score.ToString() + " Click on the restart button to restart the game.";
    }
}