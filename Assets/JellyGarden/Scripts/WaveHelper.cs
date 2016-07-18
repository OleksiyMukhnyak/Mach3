using UnityEngine;
using System.Collections.Generic;

public class WaveHelper : MonoBehaviour
{
	
	public float StartHeight 
	{
		get {return _startHeight;} 
		set {_startHeight = value;}
	}
	public float MaxHeight 
	{
		get {return _maxHeight;} 
		set{_maxHeight = value;}
	}
	public float SpeedAnimation 
	{
		get {return _speedAnimation;}
		set {if(value < 0) value = 0f; _speedAnimation = value;}
	}
		public float TimeAnimation 
	{
		get {return _timeAnimation;}
		set {if(value < 0) value = 0f; _timeAnimation = value;}
	}
	
    List<Impuls> WaveImpuls = new List<Impuls>();
    List<Vector2> WaveCenters = new List<Vector2>();
    List<float> WaveTime = new List<float>();

	
	float _startHeight = 0.6f;
	float _maxHeight = 2.0f;
	
    float _height = 0;
    float _speedAnimation = 5.0f;
    float _timeAnimation = 0.4f;

    void Start()
    {

    }


    void Update()
    {
        if (WaveImpuls.Count == 0)
            return;

        _height = StartHeight;

        for (int i = 0; i < WaveImpuls.Count; i++)
        {
            _height += WaveImpuls[i].GetImpuls(new Vector2(WaveCenters[i].x, WaveCenters[i].y),
                new Vector2(transform.localPosition.x, transform.localPosition.y), Time.time - WaveTime[i]);

            if (Time.time - WaveTime[i] > TimeAnimation)
            {
                WaveImpuls.Remove(WaveImpuls[i]);
                WaveCenters.Remove(WaveCenters[i]);
                WaveTime.Remove(WaveTime[i]);
                _height = StartHeight;
				
				//це тільки для дракончиків
                transform.localScale = new Vector3(_height, _height, 0);
            }
        }

        if (_height > MaxHeight)
            _height = MaxHeight; 

        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(_height, _height, 0), Time.deltaTime * SpeedAnimation);

    }

    public void AddCenter(Vector2 center)
    {
		// тут імпульс має 2 реалізації конструктора можна задати свою частоту амплітуду і тд.
		// Impuls(float amplitude, float lenght, float omega) тут omega це частота зміни амплітуди
		// імпульс являє собою функцію затухаючу експоненту  А * Sin (omega * time ) / exp (time * lenght * (pos - center.pos)^2 / (2pi))
		// lenght - чим більше, тим менше буде взаємодія з дальніми обєктами
		
        WaveImpuls.Add(new Impuls());
		
        WaveCenters.Add(center);
        WaveTime.Add(Time.time);
    }
}
