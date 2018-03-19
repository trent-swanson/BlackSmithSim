using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	GameObject swordMeshBase;
	GameObject swordMeshFinal;
	SkinnedMeshRenderer skinnedMeshRenderer1;
	SkinnedMeshRenderer skinnedMeshRenderer2;
	//base blends
	public float blendBot;
	public float blendTop;

	//final blends
	public float blendFlat;
	public float blendBevel;
	public float blendSharp;

	public bool finalMesh = false;

	void Start() {
		swordMeshBase = transform.GetChild (0).gameObject;
		swordMeshFinal = transform.GetChild (1).gameObject;
		swordMeshFinal.SetActive (false);
		skinnedMeshRenderer1 = swordMeshBase.GetComponent<SkinnedMeshRenderer> ();
		skinnedMeshRenderer2 = swordMeshFinal.GetComponent<SkinnedMeshRenderer> ();
	}

	public void HammerHit(float hitStrength, float localHitPostZ) {
		if (!finalMesh) {
			if (localHitPostZ <= -5) {
				blendTop -= hitStrength;
				skinnedMeshRenderer1.SetBlendShapeWeight (1, blendTop);
			} else if (localHitPostZ >= 5) {
				blendBot -= hitStrength;
				skinnedMeshRenderer1.SetBlendShapeWeight (0, blendBot);
			} else {
				blendTop -= hitStrength;
				blendBot -= hitStrength;
				skinnedMeshRenderer1.SetBlendShapeWeight (1, blendTop);
				skinnedMeshRenderer1.SetBlendShapeWeight (0, blendBot);
			}
			if (blendBot <= 0 && blendTop <= 0 && !finalMesh) {
				swordMeshBase.SetActive (false);
				swordMeshFinal.SetActive (true);
				finalMesh = true;
			}
		} else {
			blendFlat -= hitStrength;
			skinnedMeshRenderer2.SetBlendShapeWeight (1, blendFlat);
		}
	}

	public void HammerHit(float hitStrength) {
		if (!finalMesh) {
			blendTop -= hitStrength;
			blendBot -= hitStrength;
			skinnedMeshRenderer1.SetBlendShapeWeight (1, blendTop);
			skinnedMeshRenderer1.SetBlendShapeWeight (0, blendBot);
			if (blendBot <= 0 && blendTop <= 0 && !finalMesh) {
				swordMeshBase.SetActive (false);
				swordMeshFinal.SetActive (true);
				finalMesh = true;
			}
		} else {
			blendFlat -= hitStrength;
			skinnedMeshRenderer2.SetBlendShapeWeight (1, blendFlat);
		}
	}
}
