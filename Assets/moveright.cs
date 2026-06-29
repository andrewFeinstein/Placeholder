using UnityEngine;

public class moveright : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(.01f,0,0);
    }
}
