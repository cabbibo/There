using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class controllerCast : MonoBehaviour{

  public GameObject Ea;


  public Vector3 directionVector;

  public Vector3 targetPos;
  public Vector3 hitPos;
  public Vector3 targetDir;
  public Vector3 v1;
  public float targetDist;



  SteamVR_TrackedObject trackedObj;

  void Awake()
  {
    trackedObj = GetComponent<SteamVR_TrackedObject>();

    directionVector = new Vector3( 0 , 0 , 1 );

    cast();
  
  }

  void cast(){

    RaycastHit hit;
    Vector3 rotation = transform.localToWorldMatrix.MultiplyVector( directionVector );
    Ray ray = new Ray( transform.position , rotation );

    if( Physics.Raycast( ray , out hit ) ){ 
    
      if( hit.collider.tag == "astralPlane"){
        hitPos = hit.point;
      }else{
        hitPos = new Vector3(0 , 0 , 0);
      }
    }

  }

  void FixedUpdate(){

    cast();
    var device = SteamVR_Controller.Input((int)trackedObj.index);
    if ( device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
    {
      print("CHEck");
      Ea.GetComponent<Ea>().updateTarget( hitPos );
    }
  }
}
