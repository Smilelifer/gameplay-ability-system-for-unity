/******************************************************************************
* Created by: XuHongWei
* Date: 2024-04-23 11:48:46
* Des: 2d角色移动组件
*******************************************************************************/
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement2D : MonoBehaviour 
{
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public IsometricCharacterRenderer isoRenderer;
    [SerializeField]
    public Camera mainCamera;

    private Vector2 targetPosition;
    private NavMeshPath path;
    private Vector2 curPos;
    private int curPathPoint;

    public void Awake()
    {
        path = new NavMeshPath();
    }

    public void Update()
    {
        curPos = (Vector2) transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            NavMesh.CalculatePath(curPos, targetPosition, NavMesh.AllAreas, path);
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                curPathPoint = 0;
            }
        }
    }

    public void FixedUpdate()
    {
        if (path.corners.Length > 0)
        {
            Vector2 moveDir = new Vector2(path.corners[curPathPoint].x - curPos.x, path.corners[curPathPoint].y - curPos.y);
            Vector2 deltaPos = moveDir.normalized * speed * Time.deltaTime;
            curPos += deltaPos;
            transform.position = curPos;
        
            if (Vector2.Distance(curPos, path.corners[curPathPoint]) < 0.1f)
            {
                curPathPoint++;
                if (curPathPoint >= path.corners.Length)
                {
                    curPathPoint = 0;
                    path.ClearCorners();
                    isoRenderer.SetDirection(Vector3.zero);
                }
            }
        
            isoRenderer.SetDirection(deltaPos);
        }
    }
    
    public void OnDrawGizmos()
    {
        if (path != null && path.corners.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < path.corners.Length; i++)
            {
                Gizmos.DrawSphere(path.corners[i], 0.2f);
            }
        }
    }
}