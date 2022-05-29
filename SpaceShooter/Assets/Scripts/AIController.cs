using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            Points
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;
        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [SerializeField] private List<Transform> _points;
        private int _indexPoints;

        [Range(0.0f, 1.0f)] [SerializeField] private float m_NavigationLinear;
        [Range(0.0f, 1.0f)] [SerializeField] private float m_NavigationAngular;
        [SerializeField] private float m_RandomSelectMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_ShootDelayTime;
        [SerializeField] private float m_EvadeRayLength;

        float t;
        private SpaceShip m_Ship;
        private Vector3 m_MovePosition;
        private Destructible m_SelectedTarget;
        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        private void Start()
        {
            m_Ship = GetComponent<SpaceShip>();

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();

        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviour();
            }
            if (m_AIBehaviour == AIBehaviour.Points)
            {
                UpdateBehaviour();  
            }


        }

        public void UpdateBehaviour()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                                m_MovePosition = newPoint;
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }

                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }   
                }
            }
            if (m_AIBehaviour == AIBehaviour.Points)
            {
                m_MovePosition = _points[_indexPoints].position;
                if (Vector3.Distance(transform.position, m_MovePosition) < 9.5f)
                {
                    _indexPoints = (_indexPoints + 1) % _points.Count; 
                }
            }
        }

        private void ActionEvadeCollision()
        {
            if(Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f; 
            }
        }

        private void ActionControlShip()
        {
            m_Ship.ThrustControl = m_NavigationLinear;
            m_Ship.TorqueControl = ComputeAllignTorqueNormalized(m_MovePosition, m_Ship.transform) * m_NavigationLinear;
        }

        private const float Max_Angle = 45.0f;
        private static float ComputeAllignTorqueNormalized(Vector3 TargetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(TargetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
            angle = Mathf.Clamp(angle, -Max_Angle, Max_Angle) / Max_Angle;
            return -angle;
        }
        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestTarget();
                m_FindNewTargetTimer.Start(m_ShootDelayTime);
            }    
        }   
        private void ActionFire()
        {
            if(m_SelectedTarget != null)
            {
                if(m_FireTimer.IsFinished == true)
                {
                    m_Ship.Fire(TurretMode.Primary);
                    m_FireTimer.Start(m_ShootDelayTime);
                }
            }
        }

        private Destructible FindNearestTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;
                
            foreach(var v in Destructible.AllDestructbles)
            {
                if (v.GetComponent<SpaceShip>() == m_Ship) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_Ship.TeamId) continue;

                float dist = Vector2.Distance(m_Ship.transform.position, v.transform.position);

                if(dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
                
                        
        }

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelayTime);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.ReduceTime(Time.deltaTime);
            m_FireTimer.ReduceTime(Time.deltaTime);
            m_FindNewTargetTimer.ReduceTime(Time.deltaTime);
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        #endregion

    }
}
