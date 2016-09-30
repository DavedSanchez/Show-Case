using UnityEngine;
using UnityEditor;
using System.Collections;

public class DunMobGenScr : MonoBehaviour {

	//used to generate randomly sized dungeons
	public float length;
	public float width;

	public GameObject spawnPoint;
	public GameObject wall;
	public GameObject floor;
	public int radius;

	void Awake(){
		//Instantiate (floor);
		floor.transform.position = new Vector3 (length * 5f, 0f, width * 5f);
		floor.transform.localScale = new Vector3 (length, 1f, width);

		NavMeshBuilder.BuildNavMesh ();
	}

	// Use this for initialization
	void Start () {
		//NavMesh.CalculateTriangulation();
		//Instantiate (floor, floor.transform.position, Quaternion.identity);

		/*GameObject floor = new GameObject ();
		Mesh floorMesh = generateMesh(length, width);

		floor.GetComponent<MeshFilter> ().mesh = floorMesh;
		floor.GetComponent<MeshCollider> ().sharedMesh = floorMesh;
		floor.GetComponent<Transform> ().position = new Vector3 (55f, 0f, 55f);
		floor.GetComponent<Transform> ().rotation = new Quaternion(0f, 0f, 0f, 1f);
		floor.GetComponent<Transform> ().localScale = new Vector3 (length, 1f, width);

		floor.GetComponent<MeshRenderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		floor.GetComponent<MeshRenderer> ().receiveShadows = true;
		floor.GetComponent<MeshRenderer> ().motionVectors = true;
		floor.GetComponent<MeshRenderer> ().material = UnityEngine.Rendering.ShadowCastingMode.On;
		*/

		for (int i = 0; i < floor.transform.localScale.x; i++) {
			Instantiate (wall, new Vector3 (transform.position.x + (i * 10) + 5, 3f, transform.position.z), transform.rotation);
		}

		for (int i = 0; i < floor.transform.localScale.z; i++) {
			Instantiate (wall, new Vector3 (transform.position.x, 3f, transform.position.z + (i * 10) + 5), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
		}

		for (int i = 0; i < 10; i++) {
			Instantiate (spawnPoint, new Vector3(Random.Range(0,length * 10), 0f, Random.Range(0, width * 10)), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Mesh generateMesh(float lenght, float width){
		Mesh mesh = new Mesh();
		mesh.name = "Floor";
		mesh.vertices = new Vector3[] {
			new Vector3 (-lenght, -width, 0.01f),
			new Vector3 (lenght, -width, 0.01f),
			new Vector3 (lenght, width, 0.01f),
			new Vector3 (-lenght, width, 0.01f)
		};

		mesh.uv = new Vector2[]{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(1, 0)
		};

		mesh.triangles = new int[]{ 0, 1, 2, 0, 2, 3 };
		mesh.RecalculateNormals ();

		return mesh;
	}
}
