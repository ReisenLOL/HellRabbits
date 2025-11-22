using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    [Header("Attacks")]
    public EnemyAttack mainAttack;
    public EnemyAttack mainAttackInstance;
    
    [Header("Movement")]
    public Transform[] possibleDirections;
    [Header("Distance")]
    public float closestTargetDistance;
    public float maximumTargetDistance;
    public float targetBuffer;
    public float targetBufferMax;
    [Header("Dot Percentages")]
    public float forwardPercent;
    public float strafePercent;
    public float xSign = -1f;
    public float ySign = 1f;

    private void Start()
    {
        targetBuffer = Random.Range(targetBuffer, targetBufferMax);
        mainAttackInstance = Instantiate(mainAttack, transform);
        mainAttackInstance.thisEnemy = this;
    }

    private void Update()
    {
        if (mainAttackInstance.TryAttack() && Random.Range(0f,2f) > 1f)
        {
            xSign = -xSign;
            ySign = -ySign;
        }
    }

    private void FixedUpdate()
    {
        if (closestTarget)
        {
            float distanceToTarget = Vector3.Distance(closestTarget.transform.position, transform.position);
            Vector3 directionToTarget = (closestTarget.transform.position - transform.position).normalized;
            Vector3 directionToMove = directionToTarget;
            float currentHighestScore = float.NegativeInfinity;
            Vector3 strafeDirection = new Vector3(directionToTarget.y * ySign, directionToTarget.x * xSign);
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
                }
            }
            if (distanceToTarget < closestTargetDistance - targetBuffer)
            {
                rb.linearVelocity = -directionToTarget * speed;
            }
            else if (distanceToTarget > maximumTargetDistance + targetBuffer)
            {
                forwardPercent = 0.5f; 
                strafePercent = 0.5f;
                rb.linearVelocity = directionToMove * speed;
            }
            else
            {
                forwardPercent = 0.1f; 
                strafePercent = 0.9f;
                rb.linearVelocity = directionToMove * speed;
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
