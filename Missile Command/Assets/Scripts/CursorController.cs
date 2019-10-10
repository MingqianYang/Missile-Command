using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component is attached to main camera
public class CursorController : MonoBehaviour
{
    // Player's missile prefab
    [SerializeField] private GameObject missilePrefab;

    // The missile launcher prefab on the ground in the middle
    [SerializeField] private GameObject missileLauncherPrefab;

    // The replaced cursor texture
    [SerializeField] private Texture2D cursorTexture;

    AudioSource myAudio;

    // The curors's central postion on the screen
    private Vector2 cursorHotspot;

    private GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();

        myAudio = GetComponent<AudioSource>();

        // Get the central position
        cursorHotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        // Left clike mouse
        if (Input.GetMouseButtonDown(0) && myGameController.currentMissilesLoadedLauncher > 0)
        {
            Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);

            myGameController.PlayerFiredMissile();
           
            myAudio.Play();
        }
    }


    // Use leapmotion gesture to trigger missile relase event and is used in RigidRoundHand_L
    public void LeapmotionTrigger()
    {
        if (myGameController.currentMissilesLoadedLauncher > 0)
        {
            Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);

            myGameController.PlayerFiredMissile();

            myAudio.Play();
        }
    }
}
