using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSwitcher : MonoBehaviour {

	public Material Mat;
	public Texture[] Textures;
	private int Index;

	public SteamVR_TrackedController Controller;

	// Use this for initialization
	void Start () {
		Controller.MenuButtonClicked += (sender, args) => {
			Next()
		};
	}
	
	void Next() {
		Index = (Index+1)%Textures.Length;
		Mat.mainTexture = Textures[Index];
	}
}
