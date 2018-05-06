using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public float delayToFindTarget;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public Transform target;

	void Start ()
	{
		StartCoroutine("FindTargetWithDelay");
	}

	IEnumerator FindTargetWithDelay()
	{
		while(true)
		{
			FindVisibleTarget();
			yield return new WaitForSeconds(delayToFindTarget);
		}
	}

	void FindVisibleTarget()
	{
		Collider2D targetInViewRadius = Physics2D.OverlapCircle(transform.position, viewRadius, targetMask);

		target = null;

		if(targetInViewRadius != null)
		{
			Vector3[] dirToTarget = new Vector3[5];
			dirToTarget[0] = (targetInViewRadius.transform.position - transform.position).normalized;
			dirToTarget[1] = (targetInViewRadius.transform.position - targetInViewRadius.transform.localScale / 2 - transform.position).normalized;
			dirToTarget[2] = (targetInViewRadius.transform.position + targetInViewRadius.transform.localScale / 2 - transform.position).normalized;
			dirToTarget[3] = (targetInViewRadius.transform.position + new Vector3(-targetInViewRadius.transform.localScale.x, targetInViewRadius.transform.localScale.y, 0f) / 2 - transform.position).normalized;
			dirToTarget[4] = (targetInViewRadius.transform.position + new Vector3(targetInViewRadius.transform.localScale.x, -targetInViewRadius.transform.localScale.y, 0f) / 2 - transform.position).normalized;

			foreach(Vector3 dir in dirToTarget)
			{
				if(Vector3.Angle(transform.up, dir) < viewAngle / 2f) // The target is in the view Angle
				{
					float dstToTarget = Vector2.Distance((Vector2)targetInViewRadius.transform.position, (Vector2)transform.position);

					if(!Physics2D.Raycast(transform.position, dir, dstToTarget, obstacleMask)) // There is no obstacle to the target
					{
						target = targetInViewRadius.transform;
					}
				}
			}
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if(!angleIsGlobal)
		{
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0f);
	}
}
