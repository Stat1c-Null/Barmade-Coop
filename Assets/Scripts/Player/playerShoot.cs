using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{

    public GameObject bulletImpact;
    public float impactDestroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //* Player Shoot
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        ray.origin = Camera.main.transform.position;

        if(Physics.Raycast(ray, out RaycastHit hitInfo)) //* Detect hit
        {
            GameObject bulletImpactObject = Instantiate(bulletImpact, hitInfo.point + (hitInfo.normal * .002f), Quaternion.LookRotation(hitInfo.normal, Vector3.up));

            Destroy(bulletImpactObject, impactDestroyTimer);
        }

    }
}
