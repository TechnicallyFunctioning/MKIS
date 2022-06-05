using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Starting Movement Variables
    [SerializeField] private float maxThrust;
    [Tooltip("0-1 1 = Current Velocity")]
    [SerializeField] private float brakeForce;
    [SerializeField] private float pitchForce;
    [SerializeField] private float yawForce;
    [SerializeField] private float rollForce;
    // Components
    private Rigidbody shipRb;
    private InputActionAsset inputAsset;
    private InputActionMap ship;
    // Input Variables for use in code
    private float thrust = 0;
    private float pitching = 0;
    private float yawing = 0;
    private float rolling = 0;

    // Gather Components
    private void Awake()
    {
        // Grab and Assign Rigidbody and all Input Maps and Actions
        shipRb = GetComponent<Rigidbody>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        ship = inputAsset.FindActionMap("Ship");
        ship.Enable();
        ship.FindAction("Thrust").performed += DoThrust;
        ship.FindAction("Thrust").canceled += StopThrust;
        ship.FindAction("Pitch").performed += DoPitch;
        ship.FindAction("Pitch").canceled += StopPitch;
        ship.FindAction("Yaw").performed += DoYaw;
        ship.FindAction("Yaw").canceled += StopYaw;
        ship.FindAction("Roll").performed += DoRoll;
        ship.FindAction("Roll").canceled += StopRoll;
    }

    // EDITOR DEBUG
    private void Update()
    {
        Debug.Log(shipRb.velocity);
    }

    // Physics Based Movement
    private void FixedUpdate()
    {
        // Brake if no Input
        if(thrust == 0 && pitching == 0 && yawing == 0 && rolling == 0)
        {
            if (shipRb.velocity.magnitude > 0) { shipRb.velocity -= shipRb.velocity * brakeForce; }
            if (shipRb.angularVelocity.magnitude > 0) { shipRb.angularVelocity -= shipRb.angularVelocity * brakeForce; }
        }
        // Handle Thrust
        shipRb.AddForce(transform.forward * thrust * Time.deltaTime);
        // Handle Pitch/Yaw/Roll Torque
        shipRb.AddTorque(transform.right * pitching * pitchForce);
        shipRb.AddTorque(transform.up * yawing * yawForce);
        shipRb.AddTorque(transform.forward * rolling * rollForce);

    }

    // Get all Input Data (Button press/release, stick position, etc)
    // Assign Input Data to Variables
    private void DoThrust(InputAction.CallbackContext context)
    {
        thrust = maxThrust * context.ReadValue<float>();
    }
    private void StopThrust(InputAction.CallbackContext context)
    {
        thrust = 0;
    }
    private void DoPitch(InputAction.CallbackContext context)
    {
        pitching = context.ReadValue<float>();
    }
    private void StopPitch(InputAction.CallbackContext context)
    {
        pitching = 0;
    }
    private void DoYaw(InputAction.CallbackContext context)
    {
        yawing = context.ReadValue<float>();
    }
    private void StopYaw(InputAction.CallbackContext context)
    {
        yawing = 0;
    }
    private void DoRoll(InputAction.CallbackContext context)
    {
        rolling = context.ReadValue<float>();
    }
    private void StopRoll(InputAction.CallbackContext context)
    {
        rolling = 0;
    }
}
