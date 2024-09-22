using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private CharacterController Controller; // charcter controller var fot the player movement
    [SerializeField] private float player_Speed; // float var for player movement speed
    float x_input; // flaot var for the input from the A & D keys axis. 
    float z_input; // float var for the input from the W & S keys axis.
    private Vector3 move_direction; // Vector3 var for the movement direction calculation after input

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>(); // set active player character controller to var
    }

    // Update is called once per frame
    void Update()
    {
        x_input = Input.GetAxis("Horizontal") * player_Speed * Time.deltaTime; // get input from A & D keys, multiply by player speed and TDT
        z_input = Input.GetAxis("Vertical") * player_Speed * Time.deltaTime;// get input from W & S keys, multiply by player speed and TDT

        move_direction = transform.right * x_input + transform.forward * z_input; // set the move direction, transform X axis * player input on the A & D keys, and Z axis * player input on the W & S keys
        Controller.Move(move_direction); // call the move function of the controller and give the new direction created

    }
}
