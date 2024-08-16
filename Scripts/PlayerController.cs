
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Declaring components
    private Rigidbody _rigidbody;
    private Collider _collider;

    //GameObjects
    [SerializeField] protected Camera _playercamera;
    private GameManager _gamemanager;

    //Variables
    [SerializeField] protected float _walkspeed, _lookSpeed, _breathingspeed;
    private float _horizontalinput, _verticalinput, _mousex, _mousey;
    private bool _playeralive, _canmove;
    

  

    // Start is called before the first frame update
    void Start()
    {

        //Initializing components
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        //Gett
        _gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        //Initializing variables
        _walkspeed = 1.6f;
        _breathingspeed = _lookSpeed = 1;
        _playeralive = true;
       

    }

    // Update is called once per frame
    void Update()
    {
        //if player is able to move then move 
        if (_canmove) { Move(); }
              
       
        
    }

    //Accessors
    public Vector3 GetPosition() { return transform.position; }
    public Camera GetPlayerCamera() { return _playercamera; }
    public float GetPlayerWalkSpeed() { return _walkspeed; }
   
    public bool Getplayeralive() { return _playeralive; }
    public Collider GetCollider() { return _collider; }

    //Mutators 
    public void SetPosition( Vector3 newposition ) { transform.position = newposition; }
    public void SetCanMove(bool value) { _canmove = value; }
    public void SetPlayerAlive(bool value) { _playeralive = value; }
    public void SetPlayerWalkSpeed(float value) { _walkspeed = value; }




    //Locomotion and Lookout mechanics
    public void Move()
    {
        //locomotion with vector movement
        _horizontalinput = Input.GetAxis("Horizontal");
        _verticalinput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * _verticalinput + transform.right * _horizontalinput;
        movement = movement.normalized * _walkspeed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);

        //Breathing Effect and y camera movement
        //rotation for camera in x axis        
        _mousex += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _mousex = Mathf.Clamp(_mousex, -20, 20);
       _playercamera.transform.localRotation = Quaternion.Euler(_mousex + Mathf.Sin(Time.time * _breathingspeed), 0, 0);

        //This Code is for player turing left and right
        //rotation for the player in y axis
        _mousey = Input.GetAxis("Mouse X") * _lookSpeed;
        transform.rotation *= Quaternion.Euler(0, _mousey, 0);



    }

   



    



}
