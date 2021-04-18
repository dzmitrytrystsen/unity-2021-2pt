using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private List<GameObject> _cubesCreated = new List<GameObject>(3);

    private bool _isSliced;
    private bool _isSplitted;
    private bool _isReadyToSlice;

    private void Start()
    {
        _isReadyToSlice = true;
        _cubesCreated.Add(GenerateCube());
    }

    private void Update()
    {
        //_cube.transform.Rotate(0f, 15f * Time.deltaTime, 0f);

        if (Input.GetButtonDown("Jump") && _isReadyToSlice)
        {
            _cubesCreated[0].SetActive(false);

            _cubesCreated.Insert(1, GenerateCube(0.5f, 1f, 1f));
            _cubesCreated[1].transform.position = new Vector3(-0.25f, 0f, 0f);

            _cubesCreated.Insert(2, GenerateCube(0.5f, 1f, 1f));
            _cubesCreated[2].transform.position = new Vector3(0.25f, 0f, 0f);
            _isSliced = true;
            _isReadyToSlice = false;
        }

        if (_isSliced)
        {
            _cubesCreated[1].transform.position = Vector3.MoveTowards(_cubesCreated[1].transform.position,
                Vector3.left, 1f * Time.deltaTime);

            _cubesCreated[2].transform.position = Vector3.MoveTowards(_cubesCreated[2].transform.position,
                Vector3.right, 1f * Time.deltaTime);

            _isSplitted = true;
        }

        if (_isSplitted && _cubesCreated[1].transform.position == Vector3.left &&
            _cubesCreated[2].transform.position == Vector3.right)
        {
            if (!_cubesCreated[2].GetComponent<Rigidbody>())
            {
                _cubesCreated[1].AddComponent<Rigidbody>().AddForce((Vector3.left + Vector3.up) * 2f, ForceMode.Impulse);
                _cubesCreated[2].AddComponent<Rigidbody>().AddForce((Vector3.right + Vector3.up) * 2f, ForceMode.Impulse);
            }
        }

        if (_isSliced && _cubesCreated[1].transform.position.y < -4f)
        {
            Destroy(_cubesCreated[1]);

            Destroy(_cubesCreated[2]);

            _cubesCreated[0].transform.position = new Vector3(0f, 2.5f, 0f);
            _cubesCreated[0].SetActive(true);

            StartCoroutine(MoveToStart());
            _isSplitted = false;
            _isSliced = false;
        }
    }

    private IEnumerator MoveToStart()
    {
        while (_cubesCreated[0].transform.position != Vector3.zero)
        {
            _cubesCreated[0].transform.position = Vector3.MoveTowards(_cubesCreated[0].transform.position,
               Vector3.zero, 3f * Time.deltaTime);

            yield return null;
        }

        yield return _isReadyToSlice = true;
    }

    private GameObject GenerateCube(float length = 1f, float width = 1f, float height = 1f)
    {
        GameObject cube = new GameObject("Cube");
        cube.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        Vector3[] c = new Vector3[8];

        c[0] = new Vector3(-length * .5f, -width * .5f, height * .5f);
        c[1] = new Vector3(length * .5f, -width * .5f, height * .5f);
        c[2] = new Vector3(length * .5f, -width * .5f, -height * .5f);
        c[3] = new Vector3(-length * .5f, -width * .5f, -height * .5f);

        c[4] = new Vector3(-length * .5f, width * .5f, height * .5f);
        c[5] = new Vector3(length * .5f, width * .5f, height * .5f);
        c[6] = new Vector3(length * .5f, width * .5f, -height * .5f);
        c[7] = new Vector3(-length * .5f, width * .5f, -height * .5f);

        Vector3[] vertices = new Vector3[]
        {
            c[0], c[1], c[2], c[3], // Bottom
	        c[7], c[4], c[0], c[3], // Left
	        c[4], c[5], c[1], c[0], // Front
	        c[6], c[7], c[3], c[2], // Back
	        c[5], c[6], c[2], c[1], // Right
	        c[7], c[6], c[5], c[4]  // Top
        };

        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 forward = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;


        Vector3[] normals = new Vector3[]
        {
            down, down, down, down,             // Bottom
	        left, left, left, left,             // Left
	        forward, forward, forward, forward,	// Front
	        back, back, back, back,             // Back
	        right, right, right, right,         // Right
	        up, up, up, up	                    // Top
        };


        //6) Define each vertex's UV co-ordinates
        Vector2 uv00 = new Vector2(0f, 0f);
        Vector2 uv10 = new Vector2(1f, 0f);
        Vector2 uv01 = new Vector2(0f, 1f);
        Vector2 uv11 = new Vector2(1f, 1f);

        Vector2[] uvs = new Vector2[]
        {
            uv11, uv01, uv00, uv10, // Bottom
	        uv11, uv01, uv00, uv10, // Left
	        uv11, uv01, uv00, uv10, // Front
	        uv11, uv01, uv00, uv10, // Back	        
	        uv11, uv01, uv00, uv10, // Right 
	        uv11, uv01, uv00, uv10  // Top
        };

        int[] triangles = new int[]
        {
            3, 1, 0,        3, 2, 1,        // Bottom	
	        7, 5, 4,        7, 6, 5,        // Left
	        11, 9, 8,       11, 10, 9,      // Front
	        15, 13, 12,     15, 14, 13,     // Back
	        19, 17, 16,     19, 18, 17,	    // Right
	        23, 21, 20,     23, 22, 21,	    // Top
        };


        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.Optimize();

        Material cubeMaterial = new Material(Shader.Find("Standard"));
        cubeMaterial.SetColor("_Color", new Color(0f, 0.7f, 0f)); //green main color
        cube.GetComponent<Renderer>().material = cubeMaterial;

        return cube;
    }
}
