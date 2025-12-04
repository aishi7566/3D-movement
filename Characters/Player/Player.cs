using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data{get; private set;}

    [field: Header("Collision")]
    [field: SerializeField] public CapsuleColliderUtility ColliderUtility{get; private set;}
    [field: SerializeField] public PlayerLayerData LayerData {get; private set;}
    public Rigidbody Rigidbody{ get; private set; }
    public Transform MainCameraTransform{get;private set;}
    public PlayerInput Input{ get; private set;}
    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Input = GetComponent<PlayerInput>();

        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimentsions();

        MainCameraTransform = Camera.main.transform;
    
        movementStateMachine = new PlayerMovementStateMachine(this);
    }
    void OnValidate()
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimentsions();
    }
    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }
    private void OTriggerEnter(Collider collider)
    {
       movementStateMachine.OnTriggerEnter(collider); 
    }
    private void Update()
    {
        movementStateMachine.HandleInput();

        movementStateMachine.Update();   
    }
    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }

}
