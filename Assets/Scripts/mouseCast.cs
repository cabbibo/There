using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class mouseCast : MonoBehaviour{

  public GameObject Ea;


  public Vector3 directionVector;

  public Vector3 targetPos;
  public Vector3 hitPos;
  public Vector3 targetDir;
  public Vector3 v1;
  public float targetDist;


  void Start()
  {
    
    directionVector = new Vector3( 0 , 0 , 1 );

    cast();
  
  }

  void cast(){

    RaycastHit hit;
    
    Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);

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

    if (Input.GetMouseButtonDown(0))
      Ea.GetComponent<Ea>().updateTarget( hitPos );

  }
}
