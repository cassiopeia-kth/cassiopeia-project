using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFire : MonoBehaviour
{
    // Start is called before the first frame update
    public Trap trap;

    // Update is called once per frame
    void Update()
    {
        // As soon as it is ready, start a coroutine for its animation.
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        // Wait for two seconds, then delete the object.
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return 0;
    }
}
