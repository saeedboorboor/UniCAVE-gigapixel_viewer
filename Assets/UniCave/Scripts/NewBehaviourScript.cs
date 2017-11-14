using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewBehaviourScript : MonoBehaviour {

	Texture2D img;
	public GameObject textureQuad;
	Texture2D img2;
	int x_offset;
	int y_offset;
	double zoom_factor = 1.4;
	int scale_w = 3;
	int scale_h = 2;
	List<Texture2D> texture_list;

	// Use this for initialization
	void Start () {
		//Camera test = 
		if (System.Environment.MachineName == transform.parent.GetComponent<ProjectionPlane> ().MachineName) {
			Debug.Log ("this is for camera: " + transform.parent.name);
			x_offset = getXOffsetForCurrDisplay ();
			y_offset = getYOffsetForCurrDisplay ();
			Debug.Log ("---ENTERED INTO THE SCRIPT");
			LoadTexture ();
		}
	}
		

	void LoadTexture(){
		Debug.Log ("Method called");

		byte[] fileData;
		Texture2D temp;
		Camera cam = GetComponent<Camera>();
		Camera cam2 = GetComponentInParent<Camera> ();
		Transform tf = cam.transform;
		int screen_width = (int)(Screen.width*zoom_factor);
		int screen_height = (int)( Screen.height*zoom_factor);


		Debug.Log ("the screen_width" + screen_width);
		Debug.Log ("the screen_height" + screen_height);

		int start_x;
		int start_y;
		if (x_offset == 8) { // Corner node RD 6 condition
			if (y_offset == 2)
				start_y = screen_height / 256;
			else
				start_y = 0;
			start_x = (int)((x_offset * 7960 * zoom_factor) / 256);
		} else if (x_offset == 9) {
			if (y_offset == 2)
				start_y = screen_height / 256;
			else
				start_y = 0;
			start_x = (int)(((8 * 7960 * zoom_factor) + 2560*zoom_factor) / 256);
		} else if (x_offset >= 10 && x_offset < 13) {
			start_x = (int)( (((8 * 7960 * zoom_factor) + (2 * 2560 * zoom_factor)) + ((x_offset - 10) * screen_width))/ 256);
			start_y = y_offset * screen_height / 256;
		}
		else {
			start_x = x_offset * screen_width / 256;
			start_y = y_offset * screen_height/256;
		}
	

		int num_images_per_row = screen_width/256;
		int num_image_rows = screen_height/256;

		//Debug.Log ("number of images per row are"+num_images_per_row);
		//Debug.Log ("number of rows are"+num_image_rows);
		//Debug.Log("Total # of images: "+num_image_rows*num_images_per_row);

		int image_start_x = start_x + 8;
		int image_start_y = start_y + 10;

		texture_list = new List<Texture2D> ();

		Debug.Log ("image start y is" + image_start_y + " and image start x is :" + image_start_x + " for camera: "+ cam.transform.parent.name);

		for (int i = 0; i <= num_image_rows; i++) {
			for (int j = 0; j <= num_images_per_row; j++) {
				int curr_x = image_start_x + j;
				int curr_y = image_start_y + i;
				//Debug.Log ("C:/temp/NVSGQtViewer/media/textures/GigaPixel/Dubai/tile_0_"+curr_y+"_"+curr_x);
				fileData = File.ReadAllBytes ("C:/temp/NVSGQtViewer/media/textures/GigaPixel/Dubai/tile_0_" + curr_x + "_" + curr_y+".jpg");
				temp = new Texture2D (1,1);
				temp.LoadImage(fileData);
				texture_list.Add (temp);
			}
		}
		//Debug.Log ("texture list size is" + texture_list.Count);
		//go = GameObject.Find("testQuad");

		applyTexture ();

	}
	public int getYOffsetForCurrDisplay(){
		Camera cam = GetComponent<Camera> ();
		int display = cam.targetDisplay;
		string cameraName = cam.transform.parent.name;


		List<string> firstList = new List<string>(new string[] { "cam - 1 (top)" ,"cam - 1 (top) (RD2)","cam - 1 (top) (RD3)","cam - 1 (top) (RD4)","cam - 1 (top) (RD5)","cam - 1 (top) (RD6)","cam - 1 (top) (RD6)(1)","cam - 1 (top) (RD7)","cam - 1 (top) (RD8)","cam - 1 (top) (RD9)","cam - 1 (top) (RD16)","cam - 1 (top) (RD17)","cam - 1 (top) (RD18)"});
		List<string> secondList = new List<string>(new string[] { "cam - 2" ,"cam - 2 (RD2)","cam - 2 (RD3)","cam - 2 (RD4)" ,"cam - 2 (RD5)","cam - 2 (RD6)","cam - 2 (RD6)(1)","cam - 2 (RD7)","cam - 2 (RD8)","cam - 2 (RD9)","cam - 2 (RD16)","cam - 2 (RD17)","cam - 2 (RD18)" });
		List<string> thirdList = new List<string>(new string[] { "cam - 3" ,"cam - 3 (RD2)" , "cam - 3 (RD3)","cam - 3 (RD4)" ,"cam - 3 (RD5)","cam - 3 (RD7)","cam - 3 (RD8)","cam - 3 (RD9)","cam - 3 (RD16)","cam - 3 (RD17)","cam - 3 (RD18)" });
		List<string> fourthList = new List<string>(new string[] { "cam - 4" , "cam - 4 (RD2)", "cam - 4 (RD3)" , "cam - 4 (RD4)" , "cam - 4 (RD5)", "cam - 4 (RD7)", "cam - 4 (RD8)", "cam - 4 (RD9)", "cam - 4 (RD16)", "cam - 4 (RD17)", "cam - 4 (RD18)"});

		int offset;
		//matches camera name
		if (firstList.Contains (cameraName))
			return 0;
		else if (secondList.Contains (cameraName))
			return 2;
		else if (thirdList.Contains (cameraName))
			return 3;
		else if (fourthList.Contains(cameraName))
			return 1;
		else {
			Debug.Log ("****ERROR WITH OFFSET****** for:"+cameraName);
			return -1;
		}
	}


	//matches current
	public int getXOffsetForCurrDisplay(){
		Camera cam = GetComponent<Camera> ();
		int display = cam.targetDisplay;
		//Debug.Log ("the current display value is" + display);
		//Debug.Log ("name of the object is" + cam.name);
		//Debug.Log ("name of the camera is" + cam.transform.parent.name);
		//return 0;

		string cameraName = cam.transform.parent.name;

		List<string> firstList = new List<string>(new string[] { "cam - 1 (top) (RD16)", "cam - 2 (RD16)", "cam - 3 (RD16)","cam - 4 (RD16)" });
		List<string> secondList = new List<string>(new string[] { "cam - 1 (top) (RD17)", "cam - 2 (RD17)", "cam - 3 (RD17)","cam - 4 (RD17)" });
		List<string> thirdList = new List<string>(new string[] { "cam - 1 (top) (RD18)", "cam - 2 (RD18)", "cam - 3 (RD18)","cam - 4 (RD18)" });
		List<string> fourthList = new List<string>(new string[] { "cam - 1 (top)", "cam - 2", "cam - 3","cam - 4" });
		List<string> fifthList = new List<string>(new string[] { "cam - 1 (top) (RD2)", "cam - 2 (RD2)", "cam - 3 (RD2)","cam - 4 (RD2)" });
		List<string> sixthList = new List<string>(new string[] { "cam - 1 (top) (RD3)", "cam - 2 (RD3)", "cam - 3 (RD3)","cam - 4 (RD3)" });
		List<string> seventhList = new List<string>(new string[] { "cam - 1 (top) (RD4)", "cam - 2 (RD4)", "cam - 3 (RD4)","cam - 4 (RD4)" });
		List<string> eighthList = new List<string>(new string[] { "cam - 1 (top) (RD5)", "cam - 2 (RD5)", "cam - 3 (RD5)","cam - 4 (RD5)" });
		List<string> ninthList = new List<string>(new string[] { "cam - 1 (top) (RD6)", "cam - 2 (RD6)"});
		List<string> tenthList = new List<string>(new string[] { "cam - 1 (top) (RD6)(1)", "cam - 2 (RD6)(1)"});
		List<string> eleventhList = new List<string>(new string[] { "cam - 1 (top) (RD7)", "cam - 2 (RD7)", "cam - 3 (RD7)","cam - 4 (RD7)" });
		List<string> twelvethList = new List<string>(new string[] { "cam - 1 (top) (RD8)", "cam - 2 (RD8)", "cam - 3 (RD8)","cam - 4 (RD8)" });
		List<string> thirteenthList = new List<string>(new string[] { "cam - 1 (top) (RD9)", "cam - 2 (RD9)", "cam - 3 (RD9)","cam - 4 (RD9)" });


		int offset;
		//matches camera name
		if (firstList.Contains (cameraName))
			return 0;
		else if (secondList.Contains (cameraName))
			return 1;
		else if (thirdList.Contains (cameraName))
			return 2;
		else if (fourthList.Contains (cameraName))
			return 3;
		else if (fifthList.Contains (cameraName))
			return 4;
		else if (sixthList.Contains (cameraName))
			return 5;
		else if (seventhList.Contains (cameraName))
			return 6;
		else if (eighthList.Contains (cameraName))
			return 7;
		else if (ninthList.Contains (cameraName))
			return 8;
		else if (tenthList.Contains (cameraName))
			return 9;
		else if (eleventhList.Contains (cameraName))
			return 10;
		else if (twelvethList.Contains (cameraName))
			return 11;
		else if (thirteenthList.Contains (cameraName))
			return 12;
		else {
			Debug.Log ("****ERROR WITH OFFSET****** for:"+cameraName);

			return -1;
		}
	}


	//sets pixel in the target texture using the source texture, need to do range check in this
	//function otherwise the image data overlaps in the texture
	public void addTextureToParent(Texture2D target, Texture2D source, int i_start, int j_start ){
		for(int i = 0 ; i < (int)(256/zoom_factor) ; i++){
			for(int j = 0 ; j < (int)(256/zoom_factor) ; j++){
				if(i_start+i < Screen.width && ((Screen.height - j_start) + j) > 0)
					//target.SetPixel(i_start + i , j_start + j, source.GetPixel(i,j));
					target.SetPixel(i_start + i , (Screen.height - j_start) + j, source.GetPixel((int)(i*zoom_factor),(int)(j*zoom_factor)));
			}
		}
	}

	public void applyTexture(){
		int x = 0;
		int y = (int) (256/zoom_factor);

		int screen_width = (int)(Screen.width*zoom_factor);
		int screen_height = (int)(Screen.height*zoom_factor);

		int num_images_per_row = screen_width / 256;
		int num_image_rows = screen_height / 256;
		int curr_idx = 0;

		Texture2D result = new Texture2D(Screen.width, Screen.height);
		//textureQuad.GetComponent<Renderer> ().material = null;
		textureQuad.GetComponent<Renderer>().material.mainTexture = result;

		for (int i = 0; i <= num_image_rows; i++,y+=(int)(256/zoom_factor)) {
			for (int j = 0; j <= num_images_per_row; j++,x+=(int)(256/zoom_factor)) {
				Texture2D tex = texture_list[curr_idx];
				curr_idx++;
				//addTextureToParent (result, tex, x,y);
				addTextureToParent (result, tex, x,y);
			}
			x = 0;
		}
		result.Apply();
		//GUI.DrawTexture(new Rect(0, 0, screen_width, screen_height), result);s
	}

	/*public void OnGUI(){
		int x = 0;
		int y = 256;

		int screen_width = Screen.width;
		int screen_height = Screen.height;

		int num_images_per_row = screen_width / 256 * zoom_factor;
		int num_image_rows = screen_height / 256 * zoom_factor;
		int curr_idx = 0;

		Texture2D result = new Texture2D(screen_width, screen_height);
		textureQuad.GetComponent<Renderer>().material.mainTexture = result;

		for (int i = 0; i <= num_image_rows; i++,y+=256) {
			for (int j = 0; j <= num_images_per_row; j++,x+=256) {
				Texture2D tex = texture_list[curr_idx];
				curr_idx++;
				//addTextureToParent (result, tex, x,y);
				addTextureToParent (result, tex, x,y);
			}
			x = 0;
		}
		result.Apply();
		//GUI.DrawTexture(new Rect(0, 0, screen_width, screen_height), result);s
	}*/
}
