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
    private bool onlyOnce;

    bool timerElapsed = false;

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
        onlyOnce = true;
    }

    private void FixedUpdate()
    {
        timerElapsed = FindObjectOfType<GameManager>().timerZero;
        if (timerElapsed)
        {
            trapCollider.enabled = true;
        }
        else
        {
            trapCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && onlyOnce)
        {
            onlyOnce = false;
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
            //gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";

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

        if (name == "FireTrap" && spent == false)
        {
            spent = true;
            float x = pos.x;
            float y = pos.y;
            float z = pos.z;

            int num = UnityEngine.Random.Range(0, 3);
            int num2 = UnityEngine.Random.Range(4, 7);

            GameObject wildFire = (GameObject)Resources.Load("Fire/WildFire", typeof(GameObject));

            if (num == 0)
            {
                GameObject actualFire = Instantiate(wildFire, new Vector3(x - 1, y + 1, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x - 2, y + 1, z), Quaternion.identity);
            }
            else if (num == 1)
            {
                GameObject actualFire = Instantiate(wildFire, new Vector3(x, y + 1, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x, y + 2, z), Quaternion.identity);
            }
            else if (num == 2)
            {
                GameObject actualFire = Instantiate(wildFire, new Vector3(x + 1, y + 1, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x + 1, y + 2, z), Quaternion.identity);
            }
            else if (num == 3)
            {
                GameObject actualFire = Instantiate(wildFire, new Vector3(x - 1, y, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x - 2, y, z), Quaternion.identity);
            }

            if (num2 == 4)
            {
                GameObject actualFire2 = Instantiate(wildFire, new Vector3(x + 1, y, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x + 2, y, z), Quaternion.identity);
            }
            else if (num2 == 5)
            {
                GameObject actualFire2 = Instantiate(wildFire, new Vector3(x - 1, y - 1, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x - 1, y -2, z), Quaternion.identity);
            }
            else if (num2 == 6)
            {
                GameObject actualFire2 = Instantiate(wildFire, new Vector3(x, y - 1, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x, y -2, z), Quaternion.identity);
            }
            else if (num2 == 7)
            {
                GameObject actualFire2 = Instantiate(wildFire, new Vector3(x + 1, y - 1, z), Quaternion.identity);
                GameObject _actualFire = Instantiate(wildFire, new Vector3(x + 2, y - 1, z), Quaternion.identity);
            }

        }

        // Change the animation state, so that the trap animation plays.
        anim.SetInteger(animationState, 1);
        if (name == "ZeusMainTrap")
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";
        }
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
