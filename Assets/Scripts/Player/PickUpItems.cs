using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public float grabDistance = 3f;
    public float minGrabDistance = 1f;
    public float maxGrabDistance = 10f;
    public float scrollSpeed = 2f;

    public float maxVelocity = 10f;
    public float maxAngularVelocity = 20f;

    private Transform grabAnchor;
    private Rigidbody grabbedRb;
    private SpringJoint joint;

    public LayerMask lm;

    public bool objGrabbed;
    public AudioSource audio;
    void Start()
    {
        grabAnchor = new GameObject("GrabAnchor").transform;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            grabDistance = Mathf.Clamp(grabDistance + scroll * scrollSpeed, minGrabDistance, maxGrabDistance);
        }

        grabAnchor.position = Camera.main.transform.position + Camera.main.transform.forward * grabDistance;

        if (Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Release();
        }

        objGrabbed = grabbedRb != null;
        if(objGrabbed)
            audio.Play();
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb != null)
                {
                    grabbedRb = rb;

                    joint = grabbedRb.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grabAnchor.position;

                    joint.spring = 300f;
                    joint.damper = 20f;
                    joint.maxDistance = 0.1f;

                    joint.connectedBody = null;
                    joint.anchor = grabbedRb.transform.InverseTransformPoint(hit.point);
                }
            }
            else if (hit.collider.CompareTag("Door"))
            {
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb != null)
                {
                    Debug.Log("Grabbed a door: " + rb.name);
                    grabbedRb = rb;

                    joint = grabbedRb.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grabAnchor.position;

                    joint.spring = 1200f;
                    joint.damper = 80f;
                    joint.maxDistance = 0.05f;

                    joint.connectedBody = null;
                    joint.anchor = grabbedRb.transform.InverseTransformPoint(hit.point);
                }
            }
            else
                objGrabbed = false;
        }
    }

    void Release()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }

        if (grabbedRb != null)
        {
            grabbedRb.linearVelocity *= 0f;
            grabbedRb.angularVelocity *= 0f;
        }

        grabbedRb = null;
    }

    void FixedUpdate()
    {
        if (grabbedRb != null)
        {
            if (grabbedRb.linearVelocity.magnitude > maxVelocity)
            {
                grabbedRb.linearVelocity = grabbedRb.linearVelocity.normalized * maxVelocity;
            }

            if (grabbedRb.angularVelocity.magnitude > maxAngularVelocity)
            {
                grabbedRb.angularVelocity = grabbedRb.angularVelocity.normalized * maxAngularVelocity;
            }
        }
    }

    void LateUpdate()
    {
        if (joint != null && grabbedRb != null)
        {
            joint.connectedAnchor = grabAnchor.position;
        }
    }
}
