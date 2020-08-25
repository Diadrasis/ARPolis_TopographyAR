using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPolis.TopographyAR
{

    public class GpsPosition : MonoBehaviour
    {

        public GpsPosition() { }

        public static float playerHeight;

        /*Calculate position with functions and dimensions*/
        public static Vector2 FindPosition(Vector2 gpsPos)
        {
            float posX = 0;
            float posY = 0;

            posX = (gpsPos.y - MoveSettings.gpsRefLoc.y) * ((Mathf.Abs(MoveSettings.posPointA.x) + Mathf.Abs(MoveSettings.posPointB.x))) / (Mathf.Abs(MoveSettings.gpsPointB.y - MoveSettings.gpsPointA.y));
            posX += MoveSettings.gpsOffsetX;
            posY = (gpsPos.x - MoveSettings.gpsRefLoc.x) * ((Mathf.Abs(MoveSettings.posPointA.y) + Mathf.Abs(MoveSettings.posPointB.y))) / (Mathf.Abs(MoveSettings.gpsPointB.x - MoveSettings.gpsPointA.x));
            posY += MoveSettings.gpsOffsetY;

            Vector2 pos = new Vector3(posX, posY);
            return pos;
        }

        public static float FindDistance(Vector2 gpsPos)
        {
            float dist;
            Vector3 pos = FindPosition(gpsPos);
            float x = pos.x;
            float z = pos.z;

            //TODO
            //+= center of scene !!!

            dist = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2));
            return dist;
        }

        static float prevPosY = 0f;
        static bool isHole;

        public static bool isheightManual;

        public static bool firstEntrance;
        static float raycastHeight = 0f;

        public static float FindHeight(Vector2 pos)
        {

            //declare 2 values
            float rayHeight = 0f;
            //float terrainHeight = 0f;

            raycastHeight = 200f;

            //get last y position of person to make a raycast
            prevPosY = GlobalManager.Instance.person.transform.position.y + raycastHeight;       //Debug.Log("prevPosY = "+prevPosY);

            //hit down from last y position of player
            Ray downRay = new Ray(new Vector3(pos.x, prevPosY, pos.y), -Vector3.up);

            if (Physics.Raycast(downRay, out RaycastHit hit, Mathf.Infinity /* ,Diadrasis.Instance.menuUI.xartis.rayLayer*/))
            {
                //get hit distance and add person height
                rayHeight = (prevPosY - hit.distance) + playerHeight;
            }

            if (GlobalManager.Instance.user == GlobalManager.User.onAir)
            {
                return MoveSettings.personOnAirAltitude;
            }

            return rayHeight;
        }

        public static CSnapPosition FindSnapPosition(Vector2 pnt, List<CLineSegment> path)
        {
            //calculate the distances from all line segments
            List<CSnapPosition> sp = new List<CSnapPosition>();
            for (int i = 0; i < path.Count; i++)
            {
                sp.Add(FindSnapPosition(path[i].StartOfLine, path[i].EndOfLine, pnt, path[i].HasPathFreeMove));
            }

            //Find the segment with the minimum sqr distance
            float minDist = sp[0].SqrDistance;
            int index = 0;
            bool hasLimits = false;

            for (int i = 0; i < sp.Count; i++)
            {
                if (sp[i].SqrDistance < minDist)
                {
                    minDist = sp[i].SqrDistance;
                    index = i;
                    hasLimits = sp[i].LimitsOn;
                }
            }
            CSnapPosition snap = new CSnapPosition();
            snap.Position = sp[index].Position;
            snap.SqrDistance = minDist;
            snap.LimitsOn = hasLimits;
            return snap;
        }

        public static bool PlayerInsideArea(Vector2 pos, List<CArea> areas)
        {
            //count the odd nodes
            //if odd is inside else is outside
            bool oddNodes = false;
            if (areas.Count > 0)
            {
                for (int k = 0; k < areas.Count; k++)
                {
                    CArea area = areas[k];
                    int i, j = area.Points.Count - 1;
                    float x = pos.x;
                    float z = pos.y;

                    for (i = 0; i < area.Points.Count; i++)
                    {
                        if (area.Points[i].z < z && area.Points[j].z >= z || area.Points[j].z < z && area.Points[i].z >= z)
                        {
                            if (area.Points[i].x + (z - area.Points[i].z) / (area.Points[j].z - area.Points[i].z) * (area.Points[j].x - area.Points[i].x) < x)
                            {
                                oddNodes = !oddNodes;
                                //							Debug.Log(ar.points[i]);
                                //							MovePerson.currentArea=ar;
                            }
                        }
                        j = i;
                    }
                    if (oddNodes)
                    {
                        return true;
                    }
                }
                // if none returned true then it is  false!
                return oddNodes;

            }
            else
            {
                return true; //if no areas set to true to find nearest path
            }

        }

        public static bool PlayerInsideDeadArea(Vector2 pos, List<CDeadSpot> deadAreas)
        {
            //count the odd nodes
            //if odd is inside else is outside
            bool oddNodes = false;
            if (deadAreas.Count > 0)
            {
                for (int k = 0; k < deadAreas.Count; k++)
                {
                    CDeadSpot deadArea = deadAreas[k];
                    int i, j = deadArea.Points.Count - 1;
                    float x = pos.x;
                    float z = pos.y;

                    for (i = 0; i < deadArea.Points.Count; i++)
                    {
                        if (deadArea.Points[i].z < z && deadArea.Points[j].z >= z || deadArea.Points[j].z < z && deadArea.Points[i].z >= z)
                        {
                            if (deadArea.Points[i].x + (z - deadArea.Points[i].z) / (deadArea.Points[j].z - deadArea.Points[i].z) * (deadArea.Points[j].x - deadArea.Points[i].x) < x)
                            {
                                oddNodes = !oddNodes;
                            }
                        }
                        j = i;
                    }
                    if (oddNodes)
                    {
                        return true;
                    }
                }
                // if none returned true then it is  false!
                return oddNodes;

            }
            else
            {
                return false; //if no areas
            }

        }

        public static bool PlayerInsideDeadArea(out CDeadSpot deadArea, Vector2 pos, List<CDeadSpot> deadAreas)
        {
            //count the odd nodes
            //if odd is inside else is outside
            bool oddNodes = false;
            if (deadAreas.Count > 0)
            {
                for (int k = 0; k < deadAreas.Count; k++)
                {
                    CDeadSpot deadSpot = deadAreas[k];
                    int i, j = deadSpot.Points.Count - 1;
                    float x = pos.x;
                    float z = pos.y;

                    for (i = 0; i < deadSpot.Points.Count; i++)
                    {
                        if (deadSpot.Points[i].z < z && deadSpot.Points[j].z >= z || deadSpot.Points[j].z < z && deadSpot.Points[i].z >= z)
                        {
                            if (deadSpot.Points[i].x + (z - deadSpot.Points[i].z) / (deadSpot.Points[j].z - deadSpot.Points[i].z) * (deadSpot.Points[j].x - deadSpot.Points[i].x) < x)
                            {
                                oddNodes = !oddNodes;
                            }
                        }
                        j = i;
                    }
                    if (oddNodes)
                    {
                        deadArea = deadAreas[k];
                        return true;
                    }
                }


            }
            // if none returned true then it is  false!
            deadArea = null;
            return oddNodes;

        }

        private static CSnapPosition FindSnapPosition(Vector2 v, Vector2 w, Vector2 p, bool hasLimits)
        {
            CSnapPosition sp = new CSnapPosition();
            Vector2 s;
            float d;

            float projection;
            projection = Vector2.Dot(w - v, p - v) / ((w - v).sqrMagnitude);
            if (projection > 1)
            {
                s = w;
            }
            else if (projection < 0)
            {
                s = v;
            }
            else
            {
                s = projection * (w - v) + v;
            }
            d = (s - p).sqrMagnitude;
            sp.Position = s;
            sp.SqrDistance = d;
            sp.LimitsOn = hasLimits;
            return sp;
        }


    }


}