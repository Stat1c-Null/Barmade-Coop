using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{

    public GameObject bulletImpact;

    public float impactDestroyTimer;
    public float timeBetweenShots;
    private float shotCounter;
    public float maxHeat, heatPerShot, coolRate, overheatCoolRate;
    private float heatCounter;
    private bool overHeated;

    public bool automaticFire;
    public bool AmmoWeapon;

    public Gun[] guns;
    private int selectedGun;

    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.weaponHeatSlider.maxValue = maxHeat;
    }

    // Update is called once per frame
    void Update()
    {
        //*Check if weapon havent overheated
        if(!overHeated){
            //* Player Shoot
            if(Input.GetMouseButtonDown(0) && !automaticFire)
            {
                Shoot();
            } else if(Input.GetMouseButtonDown(0) && automaticFire) {
                FullAuto();
            }

            //*Check if left click is been held down
            if(Input.GetMouseButton(0) && automaticFire) {
                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0) {
                    FullAuto();
                }
            }
            heatCounter -= coolRate * Time.deltaTime;
        } else {
            heatCounter -= overheatCoolRate * Time.deltaTime;
            if(heatCounter <= 0) {
                heatCounter = 0;

                overHeated = false;

                UIController.instance.overheatedText.gameObject.SetActive(false);
            }
        }

        if(heatCounter < 0)
        {
            heatCounter = 0f;
        }

        UIController.instance.weaponHeatSlider.value = heatCounter;

        //*Switch guns
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f) { //*Check if wheel is scrolled up to switch guns
            selectedGun++;

            if(selectedGun >= guns.Length) {
                selectedGun = 0;
            }
            SwitchGun();
        } else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f) {
            selectedGun--;

            if(selectedGun < 0) {
                selectedGun = guns.Length - 1;
            }
            SwitchGun();
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

        //*Check if weapon overheated
        heatCounter += heatPerShot;
        if(heatCounter >= maxHeat) {
            heatCounter = maxHeat;

            overHeated = true;

            UIController.instance.overheatedText.gameObject.SetActive(true);
        }
    }

    private void FullAuto()
    {
        Shoot();

        shotCounter = timeBetweenShots;
    }

    private void SwitchGun() 
    {
        foreach(Gun gun in guns) {
            gun.gameObject.SetActive(false);
        }

        guns[selectedGun].gameObject.SetActive(true);
    }
}
