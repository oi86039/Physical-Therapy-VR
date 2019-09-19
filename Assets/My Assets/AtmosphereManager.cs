using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereManager : MonoBehaviour
{

    public int num; //Number to decide which atmosphere to take

    Material skybox;
    public Quaternion sunRotation;
    public Transform sun;
    AudioClip music;


    void Awake()
    {
        //Generate random number and pick properties based on it
        if (!gameManager.Instance.debugMode) 
        num = Random.Range(1, 5);

        switch (num)
        {
            case 1: //Daybreak
                skybox = Resources.Load<Material>("CloudyCrown_Daybreak");
                break;
            case 2: //Midday
                skybox = Resources.Load<Material>("CloudyCrown_Midday");
                break;
            case 3: //Sunset
                skybox = Resources.Load<Material>("CloudyCrown_Sunset");
                break;
            case 4: //Evening
                skybox = Resources.Load<Material>("CloudyCrown_Evening");
                break;
            case 5: //Midnight
                skybox = Resources.Load<Material>("CloudyCrown_Midnight");
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        //Change skybox material
        RenderSettings.skybox = skybox;
        DynamicGI.UpdateEnvironment();

    }

    // Update is called once per frame
    void Update()
    {
        sun.rotation = sunRotation;
    }
}
