using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FPS : MonoBehaviour
{
    [SerializeField] private Camera player_camera; // player camera var
    [SerializeField] float rate_of_fire, rate_of_fire_counter; // rete of fire var + Counter
    [SerializeField] int hit_damage; 
    [SerializeField] bool is_firing; // bool to check if user is firing
    private string current_hit_name; // current target hit name
    [SerializeField] private int player_hit_points; 

    DoubleStringIntUnityEvent HitEvent; // unity event for hitting target
    SingleIntUnityEvent TakeDamagerEvent; // unity event for taking damage
    // Start is called before the first frame update
    void Start()
    {
        player_camera = gameObject.GetComponentInParent<Camera>(); // reset camera var
        HitEvent = new DoubleStringIntUnityEvent(); // create new unity event
        TakeDamagerEvent = new SingleIntUnityEvent(); // create new unity event

        TakeDamagerEvent.AddListener(GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>().SetHealthSlider); // add listener to set health bar
        HitEvent.AddListener(GameObject.Find("Enemy_Manager").GetComponent<EnemyManager>().EnemyHit); // add listener to hitting target
        TakeDamagerEvent.Invoke(player_hit_points); // reset player health bar to max health
    }

    // Update is called once per frame
    void Update()
    {

        if (rate_of_fire_counter > 0) // if counter isnt up
        {
            rate_of_fire_counter -= Time.deltaTime * 100; // deduct time from counter
        }
        if (Input.GetMouseButtonDown(0)) // if player pressing left click
        {
            is_firing = true; // set is firing to true
        }
        if (Input.GetMouseButtonUp(0)) // if player stops pressing left click
        {
            is_firing = false; // set is firing to false
        }
        if (is_firing && rate_of_fire_counter <= 0) // if is firing set to true and fire rate counter is over
        {
            Fire(); // call fire function
            rate_of_fire_counter = rate_of_fire; // reset rate of fire counter
        }

    }

    void Fire()
    {
        RaycastHit hit; // create new hit var
        if (Physics.Raycast(player_camera.transform.position, player_camera.transform.forward, out hit)) // send ray cast from camera position, to camera forward direction, send hit info to hit var
        {
            if (hit.collider.CompareTag("Target")) // if the ray hit an object with target tag
            {
                current_hit_name = hit.collider.name; // save current hit name
                HitEvent.Invoke(current_hit_name, hit_damage); // invoke hit event, using current target name and hit damage
            }
        }
    }
    public void PlayerHit(int damage)
    {
        player_hit_points -= damage; // deduct damage from player HP
        TakeDamagerEvent.Invoke(player_hit_points); // invoke take damage event
    }

}

public class DoubleStringIntUnityEvent : UnityEvent<string, int> { }
public class SingleStringUnityEvent : UnityEvent<string> { }
public class SingleIntUnityEvent : UnityEvent<int> { }
