using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{
    public GameObject target;

    public float speed = 15f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    public void Setting(Vector3 pos, GameObject target)
    {
        transform.position = pos;
        this.target = target;
    }
}
