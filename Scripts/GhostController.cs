
using UnityEngine;
using UnityEngine.AI;


public class GhostController : MonoBehaviour
{

    //Decalration
    private Vector3 _position;
    private bool _followplayer, _animaitonbool;
    [SerializeField] protected GameObject _player;
    [SerializeField] protected float _ghostspeed;
    public NavMeshAgent agent;
    private Animator Animator;

    private AudioSource _ghostaudiocomponent;
    private Vector3 _playerposition;



    void Start()
    {
        
        //Initializing
        _ghostspeed = 1;
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        _ghostaudiocomponent = GetComponent<AudioSource>();
        _position = transform.position = new Vector3(10, 13, -2);
        _followplayer = false;
      



    }

    private void Update()
    {
        _position = transform.position;
       
        
        //Set Aniamtion for ghost if it's moving 
        if(Vector3.Distance(transform.position, agent.destination) >= 2) { Animator.SetFloat("FollowPlayer", 1); }
        else { Animator.SetFloat("FollowPlayer", 0); }
        
        //If  ghost is allowed to follow player then attack player
        //This is set in the game manager script where the trigger boxes trigger the follow player bool
        if (_followplayer)
        {
            Move();
            _ghostaudiocomponent.enabled = true;
           
        }
        else if(!_followplayer)
        {
            _ghostaudiocomponent.enabled = false;
            agent.SetDestination(_position);
            transform.position = _position; 

            
        }



    }


    //Accesors
    public Vector3 GetPosition() { return transform.position; }
    public AudioSource GetGhostAudioComponent() { return _ghostaudiocomponent; }

    public float GetGhostSpeed() {  return _ghostspeed; }

   

    //Mutators
   public void SetFollowPlayer(bool followcondition) { _followplayer = followcondition; }
   public void Setposition(Vector3 newposition) { gameObject.transform.position = newposition; }
    public void SetGhostSpeed(float speed) { _ghostspeed = speed; } 


    //Ghost moves with the navmesg agent which finds the path to the player 
    public void Move()
    {
        agent.speed = _ghostspeed;
        Vector3 distance = _player.transform.position - transform.position;
        agent.SetDestination(_player.transform.position);

    }
}
