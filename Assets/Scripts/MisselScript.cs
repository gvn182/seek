using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MisselScript : MonoBehaviour
{

    // Use this for initialization
    public float MissileSpeed;
    public Camera MyCam;
    public GameObject ObjTrail;
    public TrailRenderer Trail;
    List<Vector2> LstMovimentos = new List<Vector2>();
    int CurrentIndex = 0;
    public float Traveled = 0;
    bool Seeking = false;
    public float MinMove;
    Vector2 LastClickedPosition;
    Vector2 StartPos;
    Quaternion StartRotation;
    void Start()
    {
        LastClickedPosition = Vector2.zero;
        StartPos = this.transform.position;
        StartRotation = this.transform.rotation;
    }

    //Update is called once per frame
    void Update()
    {
        Track();
        MoveMissel();
    }

    private void Track()
    {
        if (Input.GetMouseButton(0))
        {
            if (!Seeking)
            {
                Trail.enabled = true;
                Vector2 MousePos = Input.mousePosition;
                ObjTrail.transform.position = new Vector3(MyCam.ScreenToWorldPoint(MousePos).x, MyCam.ScreenToWorldPoint(MousePos).y, -1f);

                Vector2 WantedPos = MyCam.ScreenToWorldPoint(MousePos);
                float Amount = Mathf.Abs(WantedPos.x - LastClickedPosition.x) + Mathf.Abs(WantedPos.y - LastClickedPosition.y);

                if (Amount >= MinMove)
                {
                    LastClickedPosition = WantedPos;             
                    LstMovimentos.Add(WantedPos);
                }

                if (LstMovimentos.Count > 1)
                    Traveled += Mathf.Abs(LstMovimentos[LstMovimentos.Count - 1].x - LstMovimentos[LstMovimentos.Count - 2].x) + Mathf.Abs(LstMovimentos[LstMovimentos.Count - 1].y - LstMovimentos[LstMovimentos.Count - 2].y);
            }
        }
        else
        {
            if (LstMovimentos.Count > 0)
                Seeking = true;
        }
    }
    private void MoveMissel()
    {
        if (Seeking)
        {
            if (CurrentIndex < LstMovimentos.Count)
            {
                Seeking = true;
                
                Vector3 diff = new Vector3(LstMovimentos[CurrentIndex].x,LstMovimentos[CurrentIndex].y,0) - transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                this.transform.position = Vector2.MoveTowards(this.transform.position, LstMovimentos[CurrentIndex], Time.deltaTime * MissileSpeed);
                if (this.transform.position.x == LstMovimentos[CurrentIndex].x && this.transform.position.y == LstMovimentos[CurrentIndex].y)
                    CurrentIndex++;
            }
            else
            {
                transform.rotation = StartRotation;
                transform.position = StartPos;
                Trail.enabled = false;
                Seeking = false;
                CurrentIndex = 0;
                LstMovimentos.Clear();
            }
        }

    }
}
