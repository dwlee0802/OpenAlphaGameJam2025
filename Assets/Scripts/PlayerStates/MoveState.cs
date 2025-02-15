using UnityEngine;

public class MoveState : State
{
    [SerializeField]
    private State idleState = null;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    float moveSpeed = 10f;
    private float sinTime = 0f;

    bool isValidMove = false;


    public override void Enter()
    {
        print("Entered move state");

        Vector3 moveDirection = parent.transform.rotation * Vector3.right;
        startPosition = parent.transform.position;
        targetPosition = startPosition + moveDirection;

        sinTime = 0f;

        isValidMove = CheckValidMove();
    }

    public override State UpdateProcess(float delta)
    {
        if (isValidMove == false)
        {
            return idleState;
        }

        if (Vector3.Distance(parent.transform.position, targetPosition) > 0.01f)
        {
            sinTime += delta * moveSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);

            float t = Evaluate(sinTime);
            parent.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            return null;
        }

        parent.transform.position = targetPosition;
        print("Movement complete, switching to idle state.");
        return idleState;
    }

    public override void Exit()
    {
        print("Exited move state");

        // make position into int
        Vector3 pos = transform.position;
        print("raw pos" + pos);
        transform.position = new Vector3(Mathf.RoundToInt(pos.x), 0, Mathf.RoundToInt(pos.z));
        print("new pos" + transform.position);
    }

    private float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }

    /* Valid move conditions
     * Within board
     * No other player on new tile
     * No walls in new tile
     */
    bool CheckValidMove()
    {
        Collider[] colliders = Physics.OverlapBox(targetPosition, new Vector3(0.1f, 0.5f, 0.2f));
        GameObject tile = null;
        GameObject wall = null;
        GameObject unit = null;

        foreach(var item in colliders)
        {
            if (item.CompareTag("Player"))
            {
                unit = item.gameObject;
            }
            if (item.CompareTag("Wall"))
            {
                wall = item.gameObject;
            }
            if (item.CompareTag("Tile"))
            {
                tile = item.gameObject;
            }
        }

        if (tile == null)
        {
            print("No tile! Cant move there.");
            return false;
        }

        if (wall != null)
        {
            print("Wall there! Cant move there.");
            return false;
        }

        if (unit != null)
        {
            print("Other Player there! Cant move there.");
            return false;
        }

        return true;
    }
}