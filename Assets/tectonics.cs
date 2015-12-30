using UnityEngine;
using System.Collections;

public class tectonics : MonoBehaviour {

  public float gridSize;
  public GameObject[] plates;
  public int numPlates;
  public Material mat;


  private Vector3 dif;

	void Start () {
    numPlates = (int)gridSize * (int)gridSize;

    plates = new GameObject[numPlates];

    for( float i = 0; i < gridSize * gridSize; i++ ){
      float y = (Mathf.Floor( i / gridSize ))/gridSize;
      float x = (i % gridSize)/gridSize;

      Vector3 p =new Vector3( 2.0f * x - 1.0f , -0.1f , 2.0f * y - 1.0f );
      GameObject plate = GameObject.CreatePrimitive(PrimitiveType.Plane);
      plate.tag = "astralPlane";
      plate.GetComponent<Renderer>().material = mat;
      plate.transform.parent = transform;
      plate.transform.position = p;
      updatePlatePosition( plate , p );
      plate.transform.localScale *= .2f * 1.0f / gridSize;


      plates[(int)i] = plate;

    }
	
	}
	
	// Update is called once per frame
	void Update () {


	}

  public float getHeight( Vector3 nP ){
    float h = 0;

    nP *= .1f;

    h += Mathf.Sin( nP.x + nP.z * 3.0f);
    h += Mathf.Sin( nP.x * 4.0f+ nP.z * 20.0f * Mathf.Sin( nP.x * 30.0f));
    h += Mathf.Sin( nP.x * 20.0f + nP.z * 10.0f);

    return h * .1f;

  }


  void updatePlatePosition( GameObject plate , Vector3 nP ){

    float h = getHeight( nP );
    plate.transform.position = new Vector3( nP.x , h , nP.z );

  }


  public void updatePlates( Vector3 p ){

    Vector3 nP;
    for( int i = 0; i <numPlates; i++ ){
      dif = plates[i].transform.position - p;
      //nP = Vector3()
      nP = plates[i].transform.position;

      bool trance = false;

      if( dif.x > 1.0f ){
        trance = true;
        nP -= new Vector3( 2.0f , 0f,0f);
      }

      if( dif.x < -1.0f ){
        trance = true;
        nP += new Vector3( 2.0f , 0f,0f);
      }


      if( dif.z > 1.0f ){
        trance = true;
        nP -= new Vector3( 0,0,2);
      }

       if( dif.z < -1.0f ){
          trance = true;
        nP += new Vector3(0,0,2);
      }

      if( trance == true ){ updatePlatePosition( plates[i] , nP ); }

    }

  }


}
