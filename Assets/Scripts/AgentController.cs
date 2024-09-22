using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent; // nav mesh agent var for controlling the agent
    [SerializeField] private GameObject Player; // game object var for the player 
    [SerializeField] private float hit_Distance; // distance needed to hit the player
    [SerializeField] private float Hit_Speed, Hit_Speed_Counter; // hit speed and counter
    [SerializeField] private int Hit_Damage;
    [SerializeField] private int agent_health_points;
    private SingleStringUnityEvent AgentDeadEvent; // single string unity event
    private SingleIntUnityEvent PlayerHitEvent; // single int unity event

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>(); // reset agent var to active nav mesh agent
        Player = GameObject.Find("Player"); // reset player to player from the scene
        Hit_Speed_Counter = Hit_Speed; // reset hit speed counter to hit speed
        AgentDeadEvent = new SingleStringUnityEvent();//create new unity event
        PlayerHitEvent = new SingleIntUnityEvent(); // create new unity event
        PlayerHitEvent.AddListener(Player.GetComponentInChildren<FPS>().PlayerHit); // add listener to the single int unity event to hit the player
        AgentDeadEvent.AddListener(GameObject.Find("Enemy_Manager").GetComponent<EnemyManager>().EnemyDead); // add listener to the single string unity event
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(Player.transform.position); // set agent destination to the player position
        Hit_Speed_Counter -= Time.deltaTime; // deduct time from hit speed counter
        if (Agent.remainingDistance <= hit_Distance && Hit_Speed_Counter <= 0) // if the agent is close to the player and hit speed counter below 0
        {
            PlayerHitEvent.Invoke(Hit_Damage);// invoke player hit using hit damage 
            Hit_Speed_Counter = Hit_Speed; // reset hit speed counter
        }

    }

    public void takeDamage(int damage)
    {
        agent_health_points -= damage; // deduct taken damage from HP
        if (agent_health_points < 0) // if HP below 0
        {
            AgentDeadEvent.Invoke(name); // invoke agent dead event using agent name
        }
    }
}
