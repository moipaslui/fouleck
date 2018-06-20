using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public float viewRadius = 10f;
	[Range(0,360)]
	public float viewAngle = 80f;
	public float delayToFindTarget = 0.05f;

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
			Vector3 dirToTarget = (targetInViewRadius.transform.position - transform.position).normalized;

			if(Vector2.Angle(-transform.right, dirToTarget) < viewAngle / 2f) // The target is in the view Angle
			{
				float dstToTarget = Vector2.Distance((Vector2)targetInViewRadius.transform.position, (Vector2)transform.position);

				if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // There is no obstacle to the target
				{
					target = targetInViewRadius.transform;
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
