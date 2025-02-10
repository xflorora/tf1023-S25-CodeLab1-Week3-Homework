using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

	public enum RotationAxes { MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseX;
	public bool invertY = false;
	
	public float sensitivityX = 10F;
	public float sensitivityY = 9F;
 
	public float minimumX = -360F;
	public float maximumX = 360F;
 
	public float minimumY = -85F;
	public float maximumY = 85F;
 
	float rotationX = 0F;
	float rotationY = 0F;
 
	private List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;	
 
	private List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
 
	public float framesOfSmoothing = 5;
 
	Quaternion originalRotation;

    public float distanceFromPlayer=2f;
	
	void Start ()
	{			
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		
		originalRotation = transform.localRotation;
	}
 
	void Update ()
	{
		if (axes == RotationAxes.MouseX)
		{			
			rotAverageX = 0f;
 
			rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.timeScale;
 
			rotArrayX.Add(rotationX);
 
			if (rotArrayX.Count >= framesOfSmoothing)
			{
				rotArrayX.RemoveAt(0);
			}
			for(int i = 0; i < rotArrayX.Count; i++)
			{
				rotAverageX += rotArrayX[i];
			}
			rotAverageX /= rotArrayX.Count;
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
 
			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;			
		}
		else
		{			
			rotAverageY = 0f;
 
 			float invertFlag = 1f;
 			if( invertY )
 			{
 				invertFlag = -1f;
 			}
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY * invertFlag * Time.timeScale;
			
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
 	
			rotArrayY.Add(rotationY);
 
			if (rotArrayY.Count >= framesOfSmoothing)
			{
				rotArrayY.RemoveAt(0);
			}
			for(int j = 0; j < rotArrayY.Count; j++)
			{
				rotAverageY += rotArrayY[j];
			}
			rotAverageY /= rotArrayY.Count;
 
			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;

            float positionZ=-distanceFromPlayer*Mathf.Cos(rotAverageY*Mathf.PI/180f);
            float positionY=-distanceFromPlayer*Mathf.Sin(rotAverageY*Mathf.PI/180f);

            Debug.Log(rotAverageY);
            Debug.Log(rotAverageY*Mathf.PI/180f);
            

            transform.localPosition=new Vector3(transform.localPosition.x,positionY,positionZ);
		}
	}
	
	public void SetSensitivity(float s)
	{
		sensitivityX = s;
		sensitivityY = s;
	}
 
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}
