using UnityEngine;
using System.Collections;

//Astral Plane

public class Ea : MonoBehaviour {

  public GameObject Plane;
  public GameObject Marker;
  public GameObject Wand1;
  public GameObject Wand2;
  public GameObject Center;
  public GameObject Beast;


  public Vector3 directionVector;

  private Vector3 targetPos;
  private Vector3 targetVec;
  private Vector3 hitPos;
  private Vector3 targetDir;
  private Vector3 v1;
  private float targetDist;

  // Use this for initialization
  void Start () {

    directionVector = new Vector3( 0 , 0 , 1 );
    targetPos = -transform.position;

  
  }

  // Update is called once per frame
  void Update () {

    targetDir = -targetPos - transform.position;
    targetDist = targetDir.magnitude;



    v1 = transform.position + targetDir * (((targetDist / 100) * .01f) + .01f);
    transform.position = v1;

    Center.transform.position = transform.worldToLocalMatrix.MultiplyVector( new Vector3(0,0,0) );
    //print( Center.transform.localPosition );

    if( Wand1.GetComponent<controllerCast>() != null ){
      hitPos = Wand1.GetComponent<controllerCast>().hitPos;
    }else{
      hitPos = Wand1.GetComponent<mouseCast>().hitPos;
    }

    if( Wand2 != Wand1 ){
      hitPos = Wand2.GetComponent<controllerCast>().hitPos;
    }
  
    Marker.transform.position = hitPos;

    Plane.GetComponent<tectonics>().updatePlates( Center.transform.position );



  }

  public void updateTarget( Vector3 t ){
    targetVec = -transform.position; 
    targetPos = -transform.position + t; 
    targetVec -= targetPos; 
    targetVec = -targetVec; 

    Beast.GetComponent<moveToTarget>().setNewTarget( Center.transform.position , targetVec ); 
  }

}
