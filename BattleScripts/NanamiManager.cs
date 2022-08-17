using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanamiManager : MonoBehaviour
{
    [SerializeField] public Action currentAction;
    [SerializeField] public State currentState;

    //General Data
    public Vector3 newNanamiPosition;

    //Blast Data
    public float blastCastTime;
    public float blastCastTimeMax;
    public float blastExecTime;
    public float blastExecTimeMax;
    public float blastACastTime;
    public float blastACastTimeMax;
    public float blastSpeed;
    public float distanceFromBorderF;
    public Vector3 nanamiStartPosition;
    public Vector3 targetLocation;
    public Collider arenaCollider;

    public GameObject tamakiObject;
    public GameObject tamakiController;
    public Transform tamakiChestPoint;

    public Transform nanamiTransform;
    public bool recoiling;
    public float recoilTime;
    public float recoilMaxTime;

    public Vector3 knockbackNewLocation;
    public float knockedBackDistance;
    public float knockBackSpeed;

    public int blastCounter;

    //JumpDeathBlowData
    public float jumpDCastTime;
    public float jumpDCastTimeMax;
    public float jumpDExecTime;
    public float jumpDExecTimeMax;
    public float jumpDACastTime;
    public float jumpDACastTimeMax;
    public float jumpDMoveSpeed;
    public float jumpDMaxHeight;
    public float jumpDRiseSpeed;
    public float nanamiY;
    public float startDistanceBetween;
    public bool jumping;
    public bool falling;

    //Charge Shot Variables
    public bool chargedShotExecuted;
    public float blastChargedShotPerfectDistance;

    //ReBlastVariables
    public float reBlastCastTime;
    public float reBlastCastTimeMax;
    public float reBlastExecTime;
    public float reBlastExecTimeMax;
    public float reBlastACastTime;
    public float reBlastACastTimeMax;

    //Smash Variables
    public float smashCastTime;
    public float smashCastTimeMax;
    public float smashExecTime;
    public float smashExecTimeMax;
    public float smashACastTime;
    public float smashACastTimeMax;

    //ReSmash Variables
    public float reSmashCastTime;
    public float reSmashCastTimeMax;
    public float reSmashExecTime;
    public float reSmashExecTimeMax;
    public float reSmashACastTime;
    public float reSmashACastTimeMax;

    //FinalSmash Variables
    public float fSmashCastTime;
    public float fSmashCastTimeMax;
    public float fSmashExecTime;
    public float fSmashExecTimeMax;
    public float fSmashACastTime;
    public float fSmashACastTimeMax;

    //Variables de SpearShot
    public float spearShotCastTime;
    public float spearShotCastTimeMax;
    public float spearShotExecTime;
    public float spearShotExecTimeMax;
    public float spearShotACastTime;
    public float spearShotACastTimeMax;

    public float spearShotSpearCreationTiming;
    public float spearShotSpearSpeed;
    public float spearShotSpearSpawnTimer;
    public float spearShotSpearSpawnTimerMax;
    public GameObject spearShotSpearPrefab;
    public bool spearCreated;
    public bool isSpearFlying;
    public Vector3 spearTargetPosition;
    public GameObject spearObject;

    public float sideJumpDistanceSide;
    public float sideJumpDistanceIn;
    //public float spearDestructionTimer;
    //public float spearDestructionTimerMax;

    //KnockedBack Variables
    public float knockedBackTime;
    public float knockedBackTimeMax;

    //Variables de hit
    public bool hitLanded;
    // public float parryTimer;

    public bool win;
    public int winCon;
    public GameObject winPic;
    public GameObject losePic;

    public GameObject floor;
    public float maxFloorTime;
    public float floorTimer;
    public float maxFloorReturnTimer;
    public float floorReturnTimer;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 75;
    }

    void Start()
    {
        nanamiTransform.GetComponent<Animator>().Play("Charging");
        FaceTarget();
        win = false;
        winCon = 0;
       // parryTimer = 0;
        spearCreated = false;
        isSpearFlying = false;
        blastCounter = 0;
        nanamiY = 0f;
        nanamiTransform.GetComponent<Rigidbody>().freezeRotation = true;
        //nanamiTransform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        //nanamiTransform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        currentAction = Action.Blast;
        currentState = State.Casting;
        blastCastTime = blastCastTimeMax;
    }

    void Update()
    {
        transform.LookAt(tamakiObject.transform.position);
        if(nanamiTransform.position.y < 0f)
        {
            nanamiTransform.position = new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z);
        }

        if (currentAction == Action.Blast)
        {
            if (currentState == State.Casting)
            {
                //newBlastLocation = CalculateBlastNewPosition();
                blastCastTime = blastCastTime-Time.deltaTime;
                if(blastCastTime <=0f)
                {
                    currentState = State.Executing;
                    nanamiStartPosition = nanamiTransform.position;
                    newNanamiPosition= CalculateNewPosition();
                    CalculateKnockbackLocation();
                    CalculateNewDodgeLocation();
                    blastExecTime = blastExecTimeMax;
                    FaceTarget();
                    //Debug.Log("Executing!");
                }
            }
            else if(currentState == State.Executing)
            {
                //targetlocation = intersection between T and N's line and the map limit - a bit of room
                //nanamiTransform.position = Vector3.Lerp(nanamiTransform.position, newBlastLocation, blastSpeed*Time.deltaTime);
                nanamiTransform.position = Vector3.MoveTowards(nanamiTransform.position, newNanamiPosition, blastSpeed * Time.deltaTime);

                blastExecTime = blastExecTime-Time.deltaTime;
                //Lerp from point a to point b
                if (blastExecTime <= 0f)
                {
                    blastCounter = blastCounter + 1;
                    //nanamiTransform.position = new Vector3(newBlastLocation.x, 0f, newBlastLocation.z);
                    blastACastTime = blastACastTimeMax;
                    nanamiTransform.GetComponent<Animator>().Play("Improved Idle");
                   // Debug.Log("Playing Improved Idle");
                    currentState = State.ACasting;
                }

                if(chargedShotExecuted)
                {
                    blastExecTime = 0f;
                    if(Vector3.Distance(nanamiTransform.position, tamakiObject.transform.position) > blastChargedShotPerfectDistance)
                    {
                        //Debug.Log("Normal Stagger");
                       // Debug.Log("In ReBlast");
                        reBlastCastTime = reBlastCastTimeMax;
                        currentAction = Action.ReBlast;
                        currentState = State.Casting;
                        nanamiTransform.GetComponent<Animator>().Play("Charging");
                    }
                    else if(Vector3.Distance(nanamiTransform.position, tamakiObject.transform.position)< blastChargedShotPerfectDistance)
                    {
                        //Debug.Log("Perfect Stagger");
                        //Debug.Log("In Smash");
                        /*
                        smashCastTime = smashCastTimeMax;
                        currentAction = Action.Smash;
                        currentState = State.Casting;
                        */
                        currentAction = Action.KnockedBack;
                        currentState = State.Casting;
                    }
                    chargedShotExecuted = false;
                }

                if (recoiling)
                {
                    nanamiTransform.GetComponent<Animator>().Play("Improved Idle");
                    blastExecTime = 0f;
                    recoilTime = recoilMaxTime;
                    currentState = State.Recoiling;
                    recoiling = false;
                }

                if(hitLanded)
                {
                    //lo que pasa cuando el ataque conecta con T
                }
            }
            else if(currentState == State.ACasting)
            {
                blastACastTime = blastACastTime - Time.deltaTime;
                //if(firstBlast) 
                //else if(secondBlast)
                //else if(thirdBlast) currentAction = Action.SpearShot;


                if (blastACastTime <= 0f)
                {
                    if(blastCounter == 1)
                    {
                        blastCounter = 0;
                        currentAction = Action.SpearShot;
                        currentState = State.Casting;
                        FaceTarget();
                        nanamiTransform.GetComponent<Animator>().Play("Charging");
                        spearShotCastTime = spearShotCastTimeMax;
                        Debug.Log("Casting SpearShot");
                    }
                    else
                    {
                        blastCastTime = blastCastTimeMax;
                        currentState = State.Casting;
                        FaceTarget();
                    }
                }
            }
            else if(currentState == State.Recoiling)
            {
                recoilTime = recoilTime - Time.deltaTime;
                if (recoilTime <= 0f)
                {
                    //Debug.Log("JumpDCast");
                    currentAction = Action.JumpDeathBlow;
                    jumpDCastTime = jumpDCastTimeMax;
                    currentState = State.Casting;
                }
            }
        }
        else if (currentAction == Action.ReBlast)
        {
            if (currentState == State.Casting)
            {
                reBlastCastTime = reBlastCastTime - Time.deltaTime;

                if (reBlastCastTime <= 0f)
                {
                    //Debug.Log("Casting ReBlast");
                    currentState = State.Executing;
                    nanamiStartPosition = nanamiTransform.position;
                    newNanamiPosition = CalculateNewPosition();
                    CalculateKnockbackLocation();
                    CalculateNewDodgeLocation();
                    reBlastExecTime = reBlastExecTimeMax;
                    FaceTarget();
                }
            }
            else if (currentState == State.Executing)
            {
                nanamiTransform.position = Vector3.MoveTowards(nanamiTransform.position, newNanamiPosition, blastSpeed * Time.deltaTime);

                reBlastExecTime = reBlastExecTime - Time.deltaTime;

                if (reBlastExecTime <= 0f)
                {
                    reBlastACastTime = reBlastACastTimeMax;
                    nanamiTransform.GetComponent<Animator>().Play("Improved Idle");
                    currentState = State.ACasting;
                }

                if (recoiling)
                {
                    nanamiTransform.GetComponent<Animator>().Play("Improved Idle");
                    blastExecTime = 0f;
                    recoilTime = recoilMaxTime;
                    currentState = State.Recoiling;
                    recoiling = false;
                }

                if(hitLanded)
                {
                    //lo que pasa cuando conecta el hit
                }
            }
            else if (currentState == State.ACasting)
            {
                reBlastACastTime = reBlastACastTime - Time.deltaTime;
                //if(firstBlast) 
                //else if(secondBlast)
                //else if(thirdBlast) currentAction = Action.SpearShot;

                if (reBlastACastTime <= 0f)
                {
                    blastCastTime = blastCastTimeMax;
                    currentAction = Action.Blast;
                    currentState = State.Casting;
                }
            }
            else if (currentState == State.Recoiling)
            {
                recoilTime = recoilTime - Time.deltaTime;
                if (recoilTime <= 0f)
                {
                    currentAction = Action.JumpDeathBlow;
                    jumpDCastTime = jumpDCastTimeMax;
                    currentState = State.Casting;
                }
            }
        }
        else if (currentAction == Action.SpearShot)
        {
            if(currentState == State.Casting)
            {
                spearShotCastTime = spearShotCastTime - Time.deltaTime;

                if(spearShotCastTime<0f)
                {
                    newNanamiPosition = CalculateNewPosition();
                    CalculateKnockbackLocation();
                    CalculateNewDodgeLocation();
                    startDistanceBetween = Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), new Vector3(newNanamiPosition.x, 0f, newNanamiPosition.z));
                    spearShotExecTime = spearShotExecTimeMax;
                    spearShotSpearSpawnTimer = spearShotSpearSpawnTimerMax;
                    currentState = State.Executing;
                    nanamiTransform.GetComponent<Animator>().Play("Improved Idle");
                    jumping = true;
                }
            }
            else if(currentState == State.Executing)
            {
                if (jumping)
                {
                    if (Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), newNanamiPosition) > (startDistanceBetween / 2))
                    {
                        nanamiY = nanamiY + jumpDRiseSpeed * Time.deltaTime;
                    }
                    else if (Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), newNanamiPosition) < (startDistanceBetween / 2))
                    {
                        if (falling)
                        {
                            nanamiTransform.GetComponent<Animator>().Play("NanamiCharge");
                            falling = false;
                        }
                        nanamiY = nanamiY - jumpDRiseSpeed * Time.deltaTime;
                        if (nanamiY <= 0f)
                        {
                            jumping = false;
                        }
                    }
                }
                nanamiTransform.position = Vector3.MoveTowards(nanamiTransform.position, new Vector3(newNanamiPosition.x, nanamiY, newNanamiPosition.z), jumpDMoveSpeed * Time.deltaTime);

                spearShotExecTime = spearShotExecTime - Time.deltaTime;
                spearShotSpearSpawnTimer = spearShotSpearSpawnTimer - Time.deltaTime;

                if(spearShotSpearSpawnTimer <=0f && spearCreated==false)
                {
                    spearCreated = true;
                    spearTargetPosition = new Vector3(tamakiChestPoint.position.x, tamakiChestPoint.position.y, tamakiChestPoint.position.z);
                    Debug.Log("YRotation: " + transform.eulerAngles.y);
                    spearObject = Instantiate(spearShotSpearPrefab, nanamiTransform.position, spearShotSpearPrefab.transform.rotation);
                    spearObject.transform.LookAt(tamakiChestPoint);
                    spearObject.transform.Rotate(new Vector3(90f, 0f, 0f));
                    isSpearFlying = true;
                }

                if(isSpearFlying)
                {
                    spearObject.transform.position = Vector3.MoveTowards(spearObject.transform.position, spearTargetPosition, spearShotSpearSpeed * Time.deltaTime);

                    if(spearObject.transform.position == spearTargetPosition)
                    {
                        isSpearFlying = false;
                        Destroy(spearObject, 3f);
                    }
                }
                

                if(spearShotExecTime <0f)
                {
                    spearShotACastTime = spearShotACastTimeMax;
                    currentState = State.ACasting;
                    spearCreated = false;
                }

                if(hitLanded)
                {
                    //acciones de Nanami si choco con la lanza o no.
                }
            }
            else if (currentState == State.ACasting)
            {
                spearShotACastTime = spearShotACastTime - Time.deltaTime;

                if(spearShotACastTime<0f)
                {
                    blastCastTime = blastCastTimeMax;
                    currentAction = Action.Blast;
                    currentState = State.Casting;
                    nanamiTransform.GetComponent<Animator>().Play("Charging");
                }
            }

            if(hitLanded)
            {

            }
        }
        else if (currentAction == Action.Smash)
        {
            //Debug.Log("Smashing");
            if (currentState == State.Casting)
            {
                smashCastTime = smashCastTime - Time.deltaTime;
                if(smashCastTime<0f)
                {
                    CalculateKnockbackLocation();
                    CalculateNewDodgeLocation();
                    smashExecTime = smashExecTimeMax;
                    currentState = State.Executing;
                    nanamiTransform.GetComponent<Animator>().Play("NanamiUpwardsSwing");
                }
            }
            else if (currentState == State.Executing)
            {
                smashExecTime = smashExecTime - Time.deltaTime;

                if(smashExecTime<0f)
                {
                    smashACastTime = smashACastTimeMax;
                    currentState = State.ACasting;
                }
                
                //Dodged
                if (smashExecTime < 0f)
                {
                    //if dodged == true
                }

                /*
                //Parried (con buen tempo)
                if (chargedShotExecuted)
                {
                    currentAction = Action.ReSmash;
                    currentState = State.Casting;
                    reSmashCastTime = reSmashCastTimeMax;
                    chargedShotExecuted = false;
                }
                //Parried (con mal tempo)
                else if (chargedShotExecuted)
                {
                    currentAction = Action.ReBlast;
                    currentState = State.Casting;
                    reBlastCastTime = reBlastCastTimeMax;
                    chargedShotExecuted = false;
                }
                */

                if (chargedShotExecuted)
                {
                    currentAction = Action.ReSmash;
                    currentState = State.Casting;
                    reSmashCastTime = reSmashCastTimeMax;
                    chargedShotExecuted = false;
                }

                if (hitLanded)
                {
                    //lo que pasa cuando conecta el hit
                }
            }
            else if (currentState == State.ACasting)
            {
                smashACastTime = smashACastTime - Time.deltaTime;
            }
        }
        else if(currentAction == Action.ReSmash)
        {
            //Debug.Log("ReSmashing");
            if (currentState == State.Casting)
            {
                reSmashCastTime = reSmashCastTime - Time.deltaTime;
                if (reSmashCastTime < 0f)
                {
                    CalculateKnockbackLocation();
                    CalculateNewDodgeLocation();
                    reSmashExecTime = reSmashExecTimeMax;
                    currentState = State.Executing;
                    nanamiTransform.GetComponent<Animator>().Play("NanamiForwardSwing");
                }
            }
            else if (currentState == State.Executing)
            {
                reSmashExecTime = reSmashExecTime - Time.deltaTime;

                if (reSmashExecTime < 0f)
                {
                    reSmashACastTime = reSmashACastTimeMax;
                    currentState = State.ACasting;
                }

                //Dodged
                if (reSmashACastTime < 0f)
                {

                }

                //Dodged (con mal tempo

                /*
                //Parried (con mal tempo)
                if (chargedShotExecuted)
                {
                    currentAction = Action.ReBlast;
                    currentState = State.Casting;
                    reBlastCastTime = reBlastCastTimeMax;
                    chargedShotExecuted = false;
                }
                //Parried (con buen tempo)
                else if (chargedShotExecuted)
                {
                    currentAction = Action.ReSmash;
                    currentState = State.Casting;
                    reSmashCastTime = reSmashCastTimeMax;
                    chargedShotExecuted = false;
                }
                */

                if (chargedShotExecuted)
                {
                    currentAction = Action.KnockedBack;
                    currentState = State.Executing;
                    chargedShotExecuted = false;
                }

                if (hitLanded)
                {
                    //lo que pasa cuando conecta el hit
                }
            }
            else if (currentState == State.ACasting)
            {
                smashACastTime = smashACastTime - Time.deltaTime;
            }
        }
        else if(currentAction == Action.FinalSmash)
        {
            //Debug.Log("FinalSmashing");
            if (currentState == State.Casting)
            {
                smashCastTime = smashCastTime - Time.deltaTime;
                if (smashCastTime < 0f)
                {
                    newNanamiPosition = CalculateNewPosition();
                    CalculateKnockbackLocation();
                    CalculateNewDodgeLocation();
                    smashExecTime = smashExecTimeMax;
                    currentState = State.Executing;
                }
            }
            else if (currentState == State.Executing)
            {
                smashExecTime = smashExecTime - Time.deltaTime;

                if (smashExecTime < 0f)
                {
                    smashACastTime = smashACastTimeMax;
                    currentState = State.ACasting;
                }

                //Dodged
                if (smashACastTime < 0f)
                {

                }

                //Dodged (con mal tempo

                /*
                //Parried (con mal tempo)
                if (chargedShotExecuted)
                {
                    currentAction = Action.ReBlast;
                    currentState = State.Casting;
                    reBlastCastTime = reBlastCastTimeMax;
                    chargedShotExecuted = false;
                }
                //Parried (con buen tempo)
                else if (chargedShotExecuted)
                {
                    currentAction = Action.ReSmash;
                    currentState = State.Casting;
                    reSmashCastTime = reSmashCastTimeMax;
                    chargedShotExecuted = false;
                }
                */

                if (chargedShotExecuted)
                {
                    currentAction = Action.ReSmash;
                    currentState = State.Casting;
                    reSmashCastTime = reSmashCastTimeMax;
                    chargedShotExecuted = false;
                }

                if (hitLanded)
                {
                    //lo que pasa cuando conecta el hit
                }
            }
            else if (currentState == State.ACasting)
            {
                smashACastTime = smashACastTime - Time.deltaTime;

             
            }
        }
        else if (currentAction == Action.JumpDeathBlow)
        {
            if (currentState == State.Casting)
            {
                jumpDCastTime = jumpDCastTime - Time.deltaTime;

                if (jumpDCastTime <= 0f)
                {
                    newNanamiPosition = CalculateNewPosition();
                    CalculateNewDodgeLocation();
                    startDistanceBetween = Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), new Vector3(tamakiObject.transform.position.x, 0f, tamakiObject.transform.position.z));
                    //Debug.Log("JumpDExec");
                    jumpDExecTime = jumpDExecTimeMax;
                    currentState = State.Executing;
                    jumping = true;
                    falling = true;
                    floorTimer = maxFloorTime;
                    floorReturnTimer = maxFloorReturnTimer;
                }
            }
            else if (currentState == State.Executing)
            {
                if (jumping)
                {
                    floorTimer = floorTimer - Time.deltaTime;
                    if (floorTimer <= 0f && floorTimer >=-0.1f)
                    {
                        floor.GetComponent<MeshRenderer>().enabled = false;
                    }
                    if (Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), newNanamiPosition) > (startDistanceBetween / 2))
                    {
                        //Debug.Log("Rising: " + Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), new Vector3(tamakiObject.transform.position.x, 0f, tamakiObject.transform.position.z)));
                        nanamiY = nanamiY + jumpDRiseSpeed * Time.deltaTime;
                    }
                    else if (Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), newNanamiPosition) < (startDistanceBetween / 2))
                    {
                        if(falling)
                        {
                            nanamiTransform.GetComponent<Animator>().Play("NanamiCharge");
                            falling = false;
                        }
                        //Debug.Log("UnRising: " + Vector3.Distance(new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z), new Vector3(tamakiObject.transform.position.x, 0f, tamakiObject.transform.position.z)));
                        nanamiY = nanamiY - jumpDRiseSpeed * Time.deltaTime;
                        if(nanamiY<=0f)
                        {
                            jumping = false;
                        }
                    }
                }
                else
                {
                    floorReturnTimer = floorReturnTimer - Time.deltaTime;
                    if (!floor.GetComponent<MeshRenderer>().enabled)
                    {
                        if (floorReturnTimer <= 0f)
                        {
                            floor.GetComponent<MeshRenderer>().enabled = true;
                        }
                        Debug.Log("FloorReturn");
                    }
                }
                nanamiTransform.position = Vector3.MoveTowards(nanamiTransform.position, new Vector3(newNanamiPosition.x, nanamiY, newNanamiPosition.z), jumpDMoveSpeed * Time.deltaTime);

                jumpDExecTime = jumpDExecTime - Time.deltaTime;

                if (jumpDExecTime <= 0f)
                {
                    Debug.Log("JumpDACast");
                    jumpDACastTime = jumpDACastTimeMax;
                    currentState = State.ACasting;
                }

                if(hitLanded)
                {

                }
            }
            else if (currentState == State.ACasting)
            {
                jumpDACastTime = jumpDACastTime - Time.deltaTime;

                if (jumpDACastTime <= 0f)
                {
                    //Debug.Log("Blast Again");
                    blastCastTime = blastCastTimeMax;
                    currentAction = Action.Blast;
                    currentState = State.Casting;
                }
            }
        }
        else if(currentAction == Action.KnockedBack)
        {
            nanamiTransform.GetComponent<Animator>().Play("Spear_Knockdown");
            win = true;
            winPic.SetActive(true);
        }
    }

    public Vector3 CalculateNewPosition()
    {
        if(currentAction == Action.Blast || currentAction == Action.ReBlast)
        {
            Ray ray = new Ray(transform.position, tamakiController.transform.position - transform.position);
            ray.origin = ray.GetPoint(150);
            ray.direction = -ray.direction;
            RaycastHit hitinfo;
            // Debug.DrawRay(ray.origin, ray.direction*100f, Color.red);
            if (Physics.Raycast(ray, out hitinfo, 200f))
            {
                //Debug.Log("Hit Something: " + hitinfo.point);
                Debug.DrawRay(ray.origin, ray.direction * hitinfo.distance, Color.red);
            }
            else
            {
                //Debug.Log("Hit Nothing");
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
            }

            Vector3 distanceFromBorderV = (tamakiController.transform.position - transform.position).normalized * distanceFromBorderF;
            //Debug.Log("distanceFromBorderV: " + distanceFromBorderV);
            Vector3 blastTargetPosition = hitinfo.point - distanceFromBorderV;
            //Debug.Log("blastTargetPosition: " + blastTargetPosition);
            //Debug.Log("Blast Target Position: " + blastTargetPosition);
            return new Vector3(blastTargetPosition.x, 0f, blastTargetPosition.z);
        }
        else if(currentAction == Action.JumpDeathBlow)
        {
            Vector3 tamakiVec = new Vector3(tamakiObject.transform.position.x, 0f, tamakiObject.transform.position.z);
            Vector3 nanamiVec = new Vector3(nanamiTransform.position.x, 0f, nanamiTransform.position.z);
            Vector3 tempVec = tamakiVec - nanamiVec;
            Debug.Log("tamakiVec: " + tamakiVec);
            tempVec = tempVec.normalized;
            Debug.Log("tempVec: " + tempVec);
            Vector3 finalVec = tamakiVec - (2f*tempVec);
            Debug.Log("finalVec: " + finalVec);
            //return new Vector3(tamakiObject.transform.position.x, 0f, tamakiObject.transform.position.z);
            return finalVec;
        }
        else if(currentAction == Action.SpearShot)
        {
            //Finetunear despues para que el salto siempre caiga a una distancia constante del borde.
            Vector3 sideJumpNewLocation = nanamiTransform.position + (transform.right * sideJumpDistanceSide);
            Debug.Log("SideJumpNewLocation: " + sideJumpNewLocation);
            return new Vector3(sideJumpNewLocation.x, 0f, sideJumpNewLocation.z);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void CalculateKnockbackLocation()
    {
        if(currentAction == Action.Blast)
        {
            knockbackNewLocation = tamakiObject.transform.position - (tamakiObject.transform.forward * knockedBackDistance);
        }
        else if(currentAction == Action.SpearShot)
        {
            knockbackNewLocation = tamakiObject.transform.position;
        }
    }

    public void CalculateNewDodgeLocation()
    {
        if(currentAction == Action.Blast || currentAction == Action.SpearShot)
        {
            Vector3 sideStepRight = tamakiObject.transform.right * tamakiObject.GetComponent<BattleParryScript>().sideSteppingDistance;
            Vector3 sideStepLeft = -tamakiObject.transform.right * tamakiObject.GetComponent<BattleParryScript>().sideSteppingDistance;
           // Debug.Log("SideStepRight: " + sideStepRight);
            //Debug.Log("SideStepLeft: " + sideStepLeft);
            bool right;
            if (sideStepRight.magnitude > sideStepLeft.magnitude)
            {
                right = false;
            }
            else
            {
                right = true;
            }

            if (right)
            {
                tamakiObject.gameObject.GetComponent<BattleParryScript>().sideStepEndPosition = sideStepRight;
            }
            else if (!right)
            {
                tamakiObject.gameObject.GetComponent<BattleParryScript>().sideStepEndPosition = sideStepLeft;
            }
        }
        else if(currentAction == Action.JumpDeathBlow)
        {
            tamakiObject.gameObject.GetComponent<BattleParryScript>().sideStepEndPosition = new Vector3(0f, 0f, 0f);
        }
    }

    void FaceTarget()
    {
        nanamiTransform.LookAt(tamakiObject.transform.position);
        nanamiTransform.GetComponent<Animator>().Play("NanamiCharge");
        //Debug.Log("Executing FaceTarget");
    }

    public enum Action
    {
        Blast,
        ReBlast,
        SpearShot,
        Smash,
        ReSmash,
        FinalSmash,
        JumpDeathBlow,
        KnockedBack
    }

    public enum State
    {
        Casting,
        Executing,
        ACasting,
        Recoiling
    }

    public enum Phase
    {
        High,
        Low
    }
}

/*
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                ray.origin = ray.GetPoint(100);
                ray.direction = -ray.direction;
                if (arenaCollider.Raycast(ray, out hit, 100))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
                */
//RaycastHit hit;
// Does the ray intersect any objects excluding the player layer
/*
if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
{
    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    Debug.Log("Did Hit");
}
*/
