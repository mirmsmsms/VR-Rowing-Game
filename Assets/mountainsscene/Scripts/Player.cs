
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    //   public Image fogUI;
    public float rotateSpeed = 2f;
    private float boatAngle;
    private Color c;
    public static Vector3 player;
    public static Vector3 boatpos;
    private static int xtra = 0;
    public Material fog;
    private Vector3 playerMov;
    private float timer1 = 0.0f;
    private float distance;
    public static float hp = 1f;
    public static float fixtime = 1f;
    private bool fix;
    private static float density = 0.0f;
    private int seconds;
    private bool isfinished = false;
    private Transform fxParent;
    public TextMeshProUGUI disttext;
    public TextMeshProUGUI xtraTxt;
    public TextMeshProUGUI sectext;
    public TextMeshProUGUI disttext2;
    public TextMeshProUGUI sectext2;
    public TextMeshProUGUI starttimer;
    public TextMeshProUGUI gamefinishtext;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI playername;
    public TextMeshProUGUI playername2;
    public TextMeshProUGUI toexit;
    public GameObject dismarkers;
    public GameObject penguins;
    public GameObject a;
    public GameObject b;
    public ParticleSystem finished;
    private bool start = false;
    public float startS = 5f;
    private int score;
    private float perfMultiplier = 1;
    private float timeleft = 10f;

    private void Start()
    {
        c = fog.GetColor("_TintColor");
        c.a = 0;
        fog.SetColor("_TintColor", c);
        player = transform.position;
        playername.SetText(PlayerPrefs.GetString("username"));
        playername2.SetText(PlayerPrefs.GetString("username"));
    }

    private void Update()
    {
        startS -= (int)Math.Ceiling((double)Time.deltaTime);
        starttimer.SetText(startS.ToString());
        StartCoroutine(timer());
        if (start)
        {
            if (hp >= 0.0)
            {
                timer1 += Time.deltaTime;
                seconds = (int)(timer1);
                boatpos = transform.position;
                distance = Vector3.Distance(boatpos, player);
                distance = (float)(int)Math.Ceiling((double)distance);
                disttext.SetText(distance.ToString() + " m");
                sectext.SetText(seconds.ToString() + "(s)");
            }
            if (isfinished)
            {
                Debug.Log("isfinished");
                timeleft -= Time.deltaTime;
                toexit.SetText(((int)timeleft).ToString());


            }



            if (Input.GetKey("space") && !isfinished)
            {
                Vector3 vector3 = transform.rotation * (Vector3.forward + new Vector3(0.0f, 0.0f, speed));
                GetComponent<Rigidbody>().velocity = vector3 * speed;
                playerMov = vector3 * speed;
                if (c.a > 0)
                {
                    c.a -= 0.0005f;
                    fog.SetColor("_TintColor", c);

                }
            }
            if (c.a <= 147.0 && !isfinished)
            {
                c.a += 0.1f * Time.deltaTime;
                fog.SetColor("_TintColor", c);
            }
        }

    }

    private void RotateBoat()
    {
        boatAngle += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(boatAngle, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "obstacle") hp -= 0.2f;
        if (other.tag == "enemy")
        {
            isfinished = true;
            a.SetActive(false);
            b.SetActive(true);
            disttext2.SetText(distance.ToString() + " m");
            sectext2.SetText(seconds.ToString() + "(s)");
            gamefinishtext.SetText("Game Over! :(");
        }
        if (other.tag == "toolbox")
        {
            Destroy(other.gameObject);
            fix = true;
        }
        if (other.tag == "finish")
        {
            isfinished = true;
            a.SetActive(false);
            b.SetActive(true);
            score = (int)Math.Ceiling((double)((distance * 100 / seconds) * perfMultiplier));
            Debug.Log(score);
            disttext2.SetText(distance.ToString() + " m");
            sectext2.SetText(seconds.ToString() + "(s)");
            scoretxt.SetText(score.ToString());
            gamefinishtext.SetText("Congratulations!");
            finished.Play();
            StartCoroutine(countdowntoexit());

        }
        if (other.tag == "firstmark")
        {
            int perf = Performance((int)distance, seconds, dismarkers.transform.GetChild(int.Parse(other.gameObject.name)));
            Debug.Log(perf);
            penguins.SetActive(true);
            if (perf == 1 || perf == 2)
            {
                other.transform.GetChild(2).gameObject.SetActive(true);
                perfMultiplier += 0.5f;
                if (perf == 2)
                {
                    other.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
                    other.transform.GetChild(4).GetComponent<AudioSource>().Play();
                    other.transform.GetChild(4).GetChild(0).GetComponent<AudioSource>().Play();
                    perfMultiplier += 1f;
                }
            }

            other.transform.GetChild(3).gameObject.SetActive(true);
            // Debug.Log(fxParent.childCount);
            // foreach (Transform child in fxParent)
            // {
            //     Debug.Log("send help");
            //     child.GetComponent<ParticleSystem>().Play();
            // }

        }


    }

    private void FixTime()
    {
    }

    public int Performance(int d, int s, Transform st)
    {
        int perf = 0;

        if ((d / s) < 2)
        {
            perf = 0;
            st.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        else if ((d / s) > 2 && (d / s) < 3)
        {
            perf = 1;
            st.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
        else if ((d / s) > 3)
        {
            perf = 2;
            st.GetChild(1).GetChild(2).gameObject.SetActive(true);
        }

        return perf;
    }

    public Vector3 GetMovement() => this.playerMov;

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(startS);
        start = true;
        starttimer.SetText("GO!");
    }

    private IEnumerator countdowntoexit()
    {

        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("main menu");
    }

    private IEnumerator Stars()
    {
        yield return new WaitForSeconds(3f);
    }
}
