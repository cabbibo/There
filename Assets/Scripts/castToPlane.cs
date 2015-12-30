using UnityEngine;
using System.Collections;

//Astral Plane

public class castToPlane : MonoBehaviour {

  public GameObject Plane;
  public GameObject Marker;
  public GameObject Wand;


  public Vector3 directionVector;

  private Vector3 targetPos;
  private Vector3 hitPos;
  private Vector3 targetDir;
  private Vector3 v1;
  private float targetDist;

	// Use this for initialization
	void Start () {

    directionVector = new Vector3( 0 , 0 , 1 );

    cast();

    //targetPos = hitPos;
	
	}

  void cast(){

    RaycastHit hit;
    Vector3 rotation = Wand.transform.localToWorldMatrix.MultiplyVector( directionVector );
    Ray ray = new Ray( Wand.transform.position , rotation );

    if( Physics.Raycast( ray , out hit ) ){
      if( hit.collider.tag == "astralPlane"){
        hitPos = hit.point;
        Marker.transform.position = hitPos;
      }
    }

  }
	
	// Update is called once per frame
	void Update () {

    targetDir = targetPos - transform.position;
    targetDist = targetDir.magnitude;

    v1 = transform.position + targetDir * targetDist / 10;
    transform.position = v1;

    cast();
    SteamVR_TrackedObject obj = Wand.GetComponent<SteamVR_TrackedObject>();
    //print( obj.index );
//    SteamVR_Controller controller = SteamVR_Controller.Input((int)obj.index);
   /* SteamVR_TrackedObject obj = Wand.GetComponent<SteamVR_TrackedObject>();
    SteamVR_Controller controller = SteamVR_Controller.Input((int)obj.index);
    //SteamVR_TrackedObject controller = Wand.GetComponent<SteamVR_TrackedObject>();

    print(controller);
    if (controller != null && controller.triggerPressed){
      print("YAY");
      targetPos = hitPos;      
    }*/


	}
}
