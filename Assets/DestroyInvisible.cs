using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInvisible : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnBecameInvisible()
    {
        if(this.gameObject.tag != "Player")
        {
            LevelManagerScript.objects_left -= 1;
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
