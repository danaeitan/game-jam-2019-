using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDropletsSlpatOnCollision : MonoBehaviour
{
	public ParticleSystem oilDroplets;
	public ParticleSystem splashDroplets;

	List<ParticleCollisionEvent> collisionEvents;


	void Start()
	{
		collisionEvents = new List<ParticleCollisionEvent>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			BeginPour();
		}
	}

	public void BeginPour()
	{
		print("pouring");
		var coll = oilDroplets.collision;
		coll.enabled = true;
		oilDroplets.Play();
	}

	void OnParticleCollision(GameObject other)
	{
		ParticlePhysicsExtensions.GetCollisionEvents(oilDroplets, other, collisionEvents);
		for (int i = 0; i < collisionEvents.Count; i++)
		{
			EmitAtLocation(collisionEvents[i]);
			Debug.Log("collided with" + other.name);
		}

	}

	private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
	{
		splashDroplets.transform.position = particleCollisionEvent.intersection;
		splashDroplets.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
		splashDroplets.Emit(1);
		var coll = oilDroplets.collision;
		coll.enabled = false;
	}
}
