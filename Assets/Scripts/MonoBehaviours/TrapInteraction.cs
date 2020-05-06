using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TrapInteraction : MonoBehaviour 
{
    public Trap trap;
    private Animator anim;
    string animationState = "AnimationState";
    private Vector3 pos;
    bool spent = false;
    public bool poseidonDirectionReady = false;
    public int poseidonDirection;
    //audio source
    public AudioSource trapSound;
    public CircleCollider2D trapCollider;
    private SpriteRenderer sr;
    public string killer;

    // Bool for the timer elapsed until we implement a global message
    bool timerElapsed = false;
    bool roundRestart = false;

    void Start()
    {
        // When a trap is initialised, it is set to z positon -100, so that it is invisible.
        anim = GetComponent<Animator>();
        //pos = gameObject.transform.position;
        //pos.z = -100;
        //gameObject.transform.position = pos;
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
        trapCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if(timerElapsed)
        {
            timerElapsed = false;
            trapCollider.enabled = true;
        }
        if(roundRestart)
        {
            roundRestart = false;
            trapCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {  
        // A trap can only be interacted with once, so we disable its collider.
        trapCollider.enabled = false;

        // Make the trap visible by changin its z value.
        pos = gameObject.transform.position;
        //pos.z = 0;
        //gameObject.transform.position = pos;
        sr.enabled = true;

        // Start a coroutine which deals with each trap animation.
        StartCoroutine(MyCoroutine(pos));
    }

    IEnumerator MyCoroutine(Vector3 pos)
    {
        // Wait for 2 seconds before changing the animation state.
        yield return new WaitForSeconds(2f);

        string name = trap.trapName;

        // If the trap is the zeus trap, then we must initiate four diagonal lightning bolts.
        if(name == "ZeusMainTrap")
        {
            // We change the sorting layer so that it will render above the player, not below.
            gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";

            float x = pos.x;
            float y = pos.y;
            float z = pos.z;
            GameObject zeusSouthEast = (GameObject)Resources.Load("Zeus/ZeusSouthEast", typeof(GameObject));
            GameObject actualSouthEast = Instantiate(zeusSouthEast, new Vector3(x + 1, y - 1.5f, z), Quaternion.identity);
            GameObject actualSouthWest = Instantiate(zeusSouthEast, new Vector3(x - 1, y - 1.5f, z), Quaternion.Euler(new Vector3(0,0,-90)));
            GameObject actualNorthEast = Instantiate(zeusSouthEast, new Vector3(x + 1, y + 0.5f, z), Quaternion.Euler(new Vector3(0,0,90)));
            GameObject actualNorthWest = Instantiate(zeusSouthEast, new Vector3(x - 1, y + 0.5f, z), Quaternion.Euler(new Vector3(0,0,180)));

            //delay for sync sound with thunder
            //yield return new WaitForSeconds(2f);
        }

        // Change the animation state, so that the trap animation plays.
        anim.SetInteger(animationState, 1);
        StartCoroutine(playSound(name));
        //trapSound.Play();

        // If the trap is the Poseidon trap, and the trap has not been used yet.
        if(name == "PoseidonTrap" && spent == false)
        {
            // Make the trap invisible, so we can rotate it.
            //pos.z = -100;
            sr.enabled = false;
            // Indicate that we have used the trap up, so it cannot be used twice.
            spent = true;
            // Generate a random number between 0 and 3, for how much we will rotate the trap by.
            int num = UnityEngine.Random.Range(0,3);
            gameObject.transform.Rotate(0.0f, 0.0f, num * 90.0f, Space.Self);
            // Store this direction as a variable, which can be accessed by the player so they know which way to move.
            poseidonDirection = num;
            // Indicate that the player is ready to be moved.
            poseidonDirectionReady = true;
            // Wait a short amount as a failsafe.
            yield return new WaitForSeconds(0.3f);
        }

        // Make the trap visible again (this only really relates to the Poseidon trap)
        //pos.z = 0;
        sr.enabled = true;

        // Wait two seconds before deactivating the trap.
        yield return new WaitForSeconds(2f);
        //pos.z = -100;
        sr.enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
       
        yield return 0;
    }

    IEnumerator playSound(string Name)
    {

        if (Name == "Zeus") { 
        yield return new WaitForSeconds(2f);
    }
        trapSound.Play();  
    }
}
