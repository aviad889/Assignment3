using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Agents; // list of active agents in scene
    private GameObject[] Spawn_Points; // array of spawn points for the agents
    [SerializeField] private GameObject Agent_Prefab; // agent prefab to spawn
    [SerializeField] private int Agent_ID; // agent id tracker


    // Start is called before the first frame update
    void Start()
    {
        Agent_ID = 0; // reset agent id tracker
        Spawn_Points = GameObject.FindGameObjectsWithTag("SpawnPoint"); // fill spawn points array from active spawn points in scene
        foreach (GameObject sp in Spawn_Points) // go over every spawn point to spawn max amount of agants
        {

            GameObject tempAgent = Instantiate(Agent_Prefab, sp.transform.position, Quaternion.identity); // spawn new agent at each spawn point
            tempAgent.name = "Agent " + Agent_ID++; // change new agent name using agent id tracker
            Agents.Add(tempAgent); // add new agent to active agent list
        }
    }

    public void EnemyDead(string name)
    {
        foreach (GameObject agent in Agents) // go over active agent list
        {
            if (agent.name == name) // find agent with the same name
            {
                Agents.Remove(agent); // remove agent from list
                Destroy(agent); // destroy the agent 
                SpawnNewAgent(); // call spawn new agent function
                return; // close the function after removing the agent
            }
        }
    }

    private void SpawnNewAgent()
    {
        int random_spawn = Random.Range(0, Spawn_Points.Length); // choose random spawn point
        GameObject tempAgent = Instantiate(Agent_Prefab, Spawn_Points[random_spawn].transform.position, Quaternion.identity); // spawn new agent at random spawn point
        tempAgent.name = "Agent " + Agent_ID++; // change new agent name using the agent id tracker
        Agents.Add(tempAgent); // add new agent to actiev agent list

    }
    public void EnemyHit(string name, int damage)
    {
        foreach (GameObject agent in Agents) // go over active agent list
        { 
            if (agent.gameObject.name == name) // find agent with same name
            { 
                agent.GetComponent<AgentController>().takeDamage(damage); // call take damage function to the agent
                return; // close the function after deducting damage
            }
        }
    }
}
