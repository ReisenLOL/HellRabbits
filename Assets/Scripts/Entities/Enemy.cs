using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //public EnemyAttack mainAttack;
    
    [Header("Movement")]
    public Transform[] possibleDirections;
    public float closestTargetDistance;
    public float maximumTargetDistance;
    public float forwardPercent;
    public float strafePercent;
    private void Update()
    {
        //mainAttack.TryAttack();
    }

    private void FixedUpdate()
    {
        if (closestTarget)
        {
            float distanceToTarget = Vector3.Distance(closestTarget.transform.position, transform.position);
            Vector3 directionToTarget = (closestTarget.transform.position - transform.position).normalized;
            Vector3 directionToMove = directionToTarget;
            float currentHighestScore = float.NegativeInfinity;
            Vector3 strafeDirection = new Vector3(directionToTarget.y, -directionToTarget.x);
            Vector3 directionToStrafe = strafeDirection;
            foreach (Transform direction in possibleDirections)
            {
                Vector3 directionToPoint = (direction.position - transform.position).normalized;
                float forwardScore = Vector3.Dot(directionToPoint, directionToTarget);
                float strafeScore = Vector3.Dot(directionToPoint, strafeDirection);
                float finalScore = (forwardScore * forwardPercent) + (strafeScore * strafePercent);
                //Debug.Log($"Forward: {forwardScore} Strafe: {strafeScore} Final: {finalScore}");
                if (finalScore > currentHighestScore)
                {
                    //Debug.Log($"{finalScore} is higher than current score: {currentHighestScore}");
                    currentHighestScore = finalScore;
                    directionToMove = directionToPoint;
                    directionToStrafe = new Vector3(directionToPoint.y, -directionToPoint.x);;
                }
            }
            if (distanceToTarget < closestTargetDistance)
            {
                rb.linearVelocity = -directionToTarget * speed;
            }
            else if (distanceToTarget >= closestTargetDistance && distanceToTarget < maximumTargetDistance)
            {
                forwardPercent = 0.1f; 
                strafePercent = 0.9f;
                rb.linearVelocity = directionToStrafe * speed;
            }
            else
            {
                forwardPercent = 0.5f; 
                strafePercent = 0.5f;
                rb.linearVelocity = directionToMove * speed;
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
