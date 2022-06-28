using UnityEngine;

public class ScullControl : MonoBehaviour
{
    public enum InputMethod { Mouse, Controller };
    public InputMethod inputMethod;

    public enum RowAxis { One, Two };
    public RowAxis rowAxis;

    public int inputLayer;

    public float oarSpeed; // visual angle speed for oars
    public float oarForceMultiplier; // how much force per stroke
    public float oarBrakeMultiplier; // brake when oar is in water
    public Vector2 oarAngleLimit;

    public float seatOarAngle; // oars degree when seat starts to move
    public float seatDistance;

    Scull scull;
    Rigidbody rbody;

    public Vector2 oar;
    public Vector2 oarOld;
    public Vector2 oarDelta;
    float pathPos;

    public GameObject controller;
    float newPos, oldPos, changePos;

    // Start is called before the first frame update
    void Start()
    {
        scull = GetComponent<Scull>();
        rbody = GetComponent<Rigidbody>();


        if (inputMethod == InputMethod.Controller)
        {
            newPos = controller.transform.position.z;
            oldPos = controller.transform.position.z;
        }

    }

    // Update is called once per frame
    void Update()
    {
      
            if (inputMethod == InputMethod.Mouse)
            {
                if (rowAxis == RowAxis.One)
                {
                    oar.x += Input.GetAxis("Mouse X") * oarSpeed;
                    oar.y -= Input.GetAxis("Mouse X") * oarSpeed * 2;
                    //Debug.Log("oar x : " + oar.x);
                }
                else if (rowAxis == RowAxis.Two)
                {
                    oar.x += Input.GetAxis("Mouse X") * oarSpeed;
                    oar.y -= Input.GetAxis("Mouse Y") * oarSpeed;
                }
            }
            else if (inputMethod == InputMethod.Controller)
            {
                if (rowAxis == RowAxis.One)
                {
                    newPos = controller.transform.position.z;
                    changePos = newPos - oldPos;
                    oar.x += changePos * oarSpeed;
                    oar.y -= changePos * oarSpeed * 2;
                    oldPos = newPos;
                    //Debug.Log(newPos);
                }
                else if (rowAxis == RowAxis.Two)
                {
                    oar.x += controller.transform.position.x * oarSpeed;
                    oar.y -= controller.transform.position.x * oarSpeed * 2;
                }
            }
        


        oar.x = Mathf.Clamp(oar.x, -oarAngleLimit.x, oarAngleLimit.x);
        oar.y = Mathf.Clamp(oar.y, -oarAngleLimit.y, oarAngleLimit.y);

        oarDelta.x = oar.x - oarOld.x;
        oarDelta.y = oar.y - oarOld.y;

        oarOld.x = oar.x;
        oarOld.y = oar.y;
        //Debug.Log(oarDelta.x + "-" + oarDelta.y);

        for (int i = 0; i < scull.rowers.Length; i++)
        {
            // oar position
            scull.rowers[i].collarL.transform.localEulerAngles = new Vector3(0f, -oar.x, 0f);
            scull.rowers[i].collarR.transform.localEulerAngles = new Vector3(0f, oar.x, 0f);
            //oar position
            scull.rowers[i].oarL.localEulerAngles = new Vector3(0f, 0f, oar.y);
            scull.rowers[i].oarR.localEulerAngles = new Vector3(0f, 0f, oar.y);

            //force that make scull move(oarDelta)
            if (scull.rowers[i].bladeL.position.y < 30) // if blade underwater
            {
                rbody.AddForceAtPosition(scull.transform.forward * oarDelta.x * oarForceMultiplier, scull.rowers[i].bladeL.position);
                rbody.AddForceAtPosition(-oarBrakeMultiplier * rbody.velocity, scull.rowers[i].bladeL.position);
            }

            if (scull.rowers[i].bladeR.position.y < 30) // if blade underwater
            {
                rbody.AddForceAtPosition(scull.transform.forward * oarDelta.x * oarForceMultiplier, scull.rowers[i].bladeR.position);
                rbody.AddForceAtPosition(-oarBrakeMultiplier * rbody.velocity, scull.rowers[i].bladeR.position);
            }

            // seat
            if (oar.x < seatOarAngle)
            {
                scull.rowers[i].seat.localPosition = new Vector3(0f, 0f, Mathf.Abs(seatOarAngle - oar.x) * seatDistance);
            }
        }

    }

    float MCos(float value)
    {
        return Mathf.Cos(Mathf.Deg2Rad * value);
    }

    float MSin(float value)
    {
        return Mathf.Sin(Mathf.Deg2Rad * value);
    }



}
