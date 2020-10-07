using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    private NavMeshAgent navMeshAgent;
    public GameController gameController;
    private Score scoreScript;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        scoreScript = FindObjectOfType<Score>();
        FindObjectOfType<LineDrawer>().OnNewPathCreated += SetPoints;
    }

    private void SetPoints(IEnumerable<Vector3> fingerpositions)
    {
        pathPoints = new Queue<Vector3>(fingerpositions);
    }
    void Start()
    {

    }

    private void UpdatePathing()
    {
        if (ShouldSetDestinatiion())
        {
            navMeshAgent.SetDestination(pathPoints.Dequeue());
        }
    }

    public void freezePlayer()
    {
        pathPoints.Clear();
    }

    private bool ShouldSetDestinatiion()
    {
        if (pathPoints.Count == 0)
        {
            return false;
        }
        if (navMeshAgent.hasPath == false || navMeshAgent.remainingDistance < 0.5f)
        {
            return true;
        }
        return false;
    }

    public void setColliderStatus(bool status)
    {
        GetComponent<Collider>().enabled = status;
    }

    public Transform Spawn;
    public FeggFollow FEggPrefab;
    public GameObject nextPlayer;// Drag & Drop the prefab with the Ghost script attached to it
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Egg")
        {
            scoreScript.updateScore();

            Destroy(other.gameObject); // Or whatever way you want to remove the coin.
            if (scoreScript.score == 1)
            {
                (Instantiate(FEggPrefab, Spawn.position, Spawn.rotation)).SetTarget(gameObject, 8);
            }
            else if (scoreScript.score == 2)
            {
                nextPlayer = GameObject.FindGameObjectsWithTag("Player")[1];
                (Instantiate(FEggPrefab, Spawn.position, Spawn.rotation)).SetTarget(nextPlayer, 8);
            }
            else
            {
                nextPlayer = GameObject.FindGameObjectsWithTag("Player")[2];
                (Instantiate(FEggPrefab, Spawn.position, Spawn.rotation)).SetTarget(nextPlayer, 8);
            }

        }
        else if (other.tag == "EndPoint")
        {
            PlayerPrefs.SetInt("TotalEggs", scoreScript.score + PlayerPrefs.GetInt("TotalEggs"));
            gameController.CompleteLevel();
        }
        else
        {

        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdatePathing();
        }
    }
}
