using UnityEngine;
using System.Collections;

public class moveToTarget : MonoBehaviour {

  public GameObject Ea;

  public GameObject Hip;
  public GameObject FootL;
  public GameObject FootR;
  public GameObject Debug;
  public GameObject DebugL;
  public GameObject DebugR;

  public Vector3 targetPos;
  public Vector3 targetVec;
  public Vector3 towardsVec;
  public Vector3 oTowardsVec;
  public Vector3 nVec; 
  public Vector3 oVec; 

  public bool liftedL;
  public bool liftedR;

  public bool justDownL;
  public bool justDownR;

  public Vector3 targetL;
  public Vector3 targetR;
  public Vector3 newPositionStartVec;

  public Vector3 ogL;
  public Vector3 ogR;

  public Quaternion rot;
  public bool turning;

  public float rotationVal;

  public Ea EaSript;


  private Vector3 hipStartPos;

	// Use this for initialization
	void Start () {
  
    hipStartPos = Hip.transform.position;	
     liftedR = true;
    rotationVal = 0;

	}
	
	// Update is called once per frame
	void Update () {

    Vector3 ogPos = transform.position;
    targetVec = (ogPos - targetPos);

    //targetDist = ogPos - towardsVec;

    //Hip.transform.position = ((targetVec * -.1f) + ogPos) + hipStartPos;
    transform.position =  ((targetVec * -.1f) + ogPos);
   // print(transform.position);


// Checking to see if we need to be rotating
    nVec = towardsVec;
    oVec = oTowardsVec;
    nVec.Normalize();
    oVec.Normalize();

    float dif = Vector3.Dot(nVec,oVec);

    float rad = Mathf.Atan2(nVec.z , nVec.x);
    rad *=  (180 / Mathf.PI);

    float oRad = Mathf.Atan2(oVec.z , oVec.x);
    oRad *=  (180 / Mathf.PI);


    Quaternion qT = Quaternion.AngleAxis(-rad , Vector3.up);
    Quaternion qF = Quaternion.AngleAxis(-oRad , Vector3.up);

    rot =  Quaternion.Slerp(qF, qT, rotationVal);

    rotationVal += .01f * (dif+3.0f);

    if( rotationVal > .7f ){
      turning = false;
    }

    Hip.transform.rotation = rot;



   Debug.transform.position = newPositionStartVec;

   Vector3 currentPos = transform.worldToLocalMatrix.MultiplyVector( new Vector3(0,0,0) );

   float h = getHeight( transform.position );
   print( transform.localPosition );

  // print( h );

   Debug.transform.localPosition = transform.localPosition;

    /*
      Checks if we need to move foot, and moves it
    */
    if( turning == false ){

      Vector3 distance1 = transform.position - FootL.transform.position;
      Vector3 distance2 = transform.position - FootR.transform.position;  

      if( distance1.magnitude > .35f && liftedR == false && justDownL == false ){
        getNewFootPos( true ); 
      }

      if( distance2.magnitude > .35f && liftedL == false && justDownR == false){
        getNewFootPos( false );
      }

    }else{

      print("t");
      if( liftedR == false && justDownL == false ){
        print("LL");
        getNewFootPos( true ); 
      }

      if( liftedL == false && justDownR == false ){
        print("RR");
        getNewFootPos( false );
      }



    }

    /*
      Moves foot toward target Position
    */
    if( liftedL == true ) FootL.transform.position -= ( FootL.transform.position - targetL ) * .1f;
    if( liftedR == true ) FootR.transform.position -= ( FootR.transform.position - targetR ) * .1f;


    /*
      Checks to see if we are close enough to target Position
    */
    justDownL = false;
    justDownR = false;

    float checkVal =.05f;
    if( turning == true ){ checkVal = .15f; }

    if( ( FootL.transform.position - targetL ).magnitude < checkVal ){
      justDownL = true;
      liftedL = false;
    }

    if( ( FootR.transform.position - targetR ).magnitude < checkVal ){
      justDownR = true;
      liftedR = false;
    }


	}


  public float getHeight( Vector3 nP ){
    float h = 0;

    nP *= .1f;

    h += Mathf.Sin( nP.x + nP.z * 3.0f);
    h += Mathf.Sin( nP.x * 4.0f+ nP.z * 20.0f * Mathf.Sin( nP.x * 30.0f));
    h += Mathf.Sin( nP.x * 20.0f + nP.z * 10.0f);

    return h * .1f;

  }

  void getNewFootPos( bool isLeft ){

    Vector3 left = rot * Vector3.forward;
    Vector3 right = rot * Vector3.back;

    float multVal = 1.8f;

    
    if(isLeft == true ){


      FootL.transform.rotation = rot;
    
      Vector3 dir = FootL.transform.position - targetPos;
      ogL = FootL.transform.position;
      targetL = FootL.transform.position - multVal * dir;
      targetL += left * .2f;

      float h = getHeight( targetL );
      targetL = new Vector3( targetL.x , h , targetL.z );

      DebugL.transform.position = targetL;
      liftedL = true;


    }else{

      FootR.transform.rotation = rot;

      Vector3 dir = FootR.transform.position - targetPos;
      ogR = FootR.transform.position;
      targetR = FootR.transform.position - multVal * dir ;
      targetR += right * .2f;

     


      float h = getHeight( targetR );
      targetR = new Vector3( targetR.x , h , targetR.z );
       DebugR.transform.position = targetR;


      liftedR = true;

    }
      


  }

  public void setNewTarget( Vector3 tarPos , Vector3 tarVec ){
    newPositionStartVec = transform.position;
    oTowardsVec = towardsVec;
    targetPos = tarPos;
    towardsVec = tarVec;
    rotationVal = 0;
    turning = true;
  }


 /* Matrix4x4 getRotationMatrix( Vector3 dir ){


    Vector3 up = new Vector3( 0 , 1 , 0);
    }
//    Vector3 right = new Vector3().Cross(dir , up);

    return Matrix4x4(
      dir.x , dir.y

    );
  }*/
}
