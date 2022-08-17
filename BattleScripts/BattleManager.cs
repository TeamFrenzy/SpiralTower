using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public delegate void StartTouchPrimary(Vector2 position, float time);
    public event StartTouchPrimary OnStartTouchPrimary;
    public delegate void EndTouchPrimary(Vector2 position, float time);
    public event EndTouchPrimary OnEndTouchPrimary;
    public delegate void TapPrimary(Vector2 position, float time);
    public event TapPrimary OnTapPrimary;

    private Camera cam;

    public Phase phase;
    public GameObject enemy;
    public GameObject phantomT;
    public GameObject playerCharacter;

    float timeCounter = 0;
    public Vector3 startingPosition;
    public float speed;
    public float width;
    public float height;

    public Vector3 lastPosition;
    public Vector3 lastPositionT;
    public Vector3 direction;
    public Vector3 directionT;

    public float testAngle;
    public Quaternion testQuaternion;
    // public Vector3 localDirection;

    //Charge variables
    public float chargeTimerMax;
    public float chargeTimerCurrent;
    Vector3 chargeStartPosition;
    Vector3 chargeEndPosition;
    float chargeElapsedTime;
    public float chargeDesiredDuration;
    public bool goingIn = false;

    // Charge Lerp Data 
    public GameObject testLerpCube;
    public Vector3 endPosition = new Vector3(5, -2, 0);
    public Vector3 startPosition;
    public float desiredDuration = 3f;
    private float elapsedTime;

    //Jump data
    public bool goingDown = false;
    public bool jumping = false;
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpTime;
    public float jumpStartDistance;
    public float jumpTimeTracker;

    //StartupTimer
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public float startUpTimer;

    bool rolling = false;

    private void Start()
    {
        // startPosition = testLerpCube.transform.position;

        // UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        chargeTimerCurrent = chargeTimerMax;
        startingPosition = enemy.transform.position;
        phase = Phase.Roll;
        enemy.GetComponent<Animator>().Play("Idle Ball");
        playerCharacter.GetComponent<Animator>().Play("Improved Idle");
    }

    private void Update()
    {

        startUpTimer = startUpTimer + Time.deltaTime;
        if (startUpTimer < 2f)
        {

        }
        else if (startUpTimer < 3f)
        {
            three.SetActive(true);
        }
        else if (startUpTimer < 4f)
        {
            three.SetActive(false);
            two.SetActive(true);
        }
        else if (startUpTimer < 5f)
        {
            two.SetActive(false);
            one.SetActive(true);
        }
        else if (startUpTimer < 6f)
        {
            rolling = true;
            one.SetActive(false);
        }

        if (startUpTimer > 5f)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;
            testLerpCube.transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));
            if (phase == Phase.Roll)
            {

                timeCounter += Time.deltaTime * speed;

                float x = Mathf.Cos(timeCounter) * width;
                float y = 0;
                float z = Mathf.Sin(timeCounter) * height;

                enemy.transform.position = new Vector3(x, y, z) + new Vector3(-width, 0f, 0f) + startingPosition;
                phantomT.transform.position = new Vector3(x, y, z) + new Vector3(-width, 0f, 0f) + startingPosition;

                //enemy.transform.position = new Vector3(x, y, z) + playerCharacter.transform.position + new Vector3(-width, 0f, 0f)+startingPosition;
                //phantomT.transform.position = new Vector3(x, y, z) + playerCharacter.transform.position + new Vector3(-width, 0f, 0f) + startingPosition;

                //  enemy.transform.position = new Vector3(x, y, z) + startingPosition;
                //phantomT.transform.position = new Vector3(x, y, z) + startingPosition;

                direction = enemy.transform.position - lastPosition;
                directionT = phantomT.transform.position - lastPositionT;

                //var localDirection = phantomT.transform.InverseTransformDirection(direction);
                // enemy.transform.rotation = Quaternion.Euler(localDirection);
                // enemy.transform.rotation = Quaternion.Euler(direction);
                testAngle = Vector3.SignedAngle(direction, phantomT.transform.forward, Vector3.up);
                Quaternion angledDirection = Quaternion.AngleAxis(testAngle, phantomT.transform.forward);
                testQuaternion = angledDirection;

                phantomT.transform.rotation = angledDirection;

                enemy.transform.rotation = Quaternion.Euler(new Vector3(0f, -angledDirection.eulerAngles.z, 0f));
                // enemy.transform.rotation = Quaternion.Euler(angledDirection);
                lastPosition = enemy.transform.position;
                lastPositionT = phantomT.transform.position;


                chargeTimerCurrent = chargeTimerCurrent - Time.deltaTime;

                if (chargeTimerCurrent <= 0f)
                {
                    direction = enemy.transform.position - playerCharacter.transform.position;
                    chargeStartPosition = enemy.transform.position;
                    chargeEndPosition = playerCharacter.transform.position - enemy.transform.position;
                    chargeElapsedTime = 0f;
                    goingIn = true;
                    phase = Phase.Charge;
                }
            }
            if (phase == Phase.Charge)
            {
                chargeElapsedTime += Time.deltaTime;
                float chargePercentageComplete = chargeElapsedTime / chargeDesiredDuration;
                enemy.transform.position = Vector3.Lerp(chargeStartPosition, chargeEndPosition, Mathf.SmoothStep(0, 1, chargePercentageComplete));

                if ((Vector3.Distance(enemy.transform.position, playerCharacter.transform.position) <= jumpStartDistance) && !jumping && goingIn)
                {
                    Debug.Log("Jump Started");
                    jumping = true;
                    goingIn = false;
                    enemy.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.red;
                }

                if (jumping)
                {
                    jumpTime = jumpTime + Time.deltaTime;
                    jumpTimeTracker = Mathf.PingPong(jumpTime * jumpSpeed, 1);
                    // enemy.transform.position = new Vector3(enemy.transform.position.x, Mathf.PingPong(Time.time*jumpSpeed, jumpHeight), enemy.transform.position.z);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Mathf.Lerp(0f, jumpHeight, Mathf.PingPong(jumpTime * jumpSpeed, 1)), enemy.transform.position.z);

                    if (enemy.transform.position.y >= 0.5f)
                    {
                        goingDown = true;
                    }

                    if (goingDown && enemy.transform.position.y <= 0.1f)
                    {

                        Debug.Log("Jump Ended");
                        jumping = false;
                        goingDown = false;
                    }
                }

                if (enemy.transform.position == chargeEndPosition)
                {
                    float distanceBetween = Vector3.Distance(enemy.transform.position, playerCharacter.transform.position);
                    timeCounter = 0f;
                    chargeTimerCurrent = chargeTimerMax;
                    startingPosition = enemy.transform.position;
                    phase = Phase.Roll;
                }
            }
            else if (phase == Phase.JumpDive)
            {
                //En desarrollo
            }
            else if (phase == Phase.AimAndShoot)
            {
                //En desarrollo
            }
        }
        if (rolling == true)
        {
            Debug.Log("Start Rolling");
            enemy.GetComponent<Animator>().Play("PermaRoll");
            playerCharacter.GetComponent<Animator>().Play("Right Turn");
            rolling = false;
        }
    }

    public enum Phase
    {
        Roll,
        Charge,
        JumpDive,
        AimAndShoot
    }
}
