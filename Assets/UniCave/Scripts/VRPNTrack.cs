﻿//MIT License
//Copyright 2016-Present 
//Ross Tredinnick
//Brady Boettcher
//Living Environments Laboratory
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
//to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, 
//sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using UnityEngine;
using System.Collections;

public class VRPNTrack : MonoBehaviour
{
    [SerializeField]
    private string trackerAddress = "Isense900@C6_V1_HEAD";
    [SerializeField]
    private int channel = 0;
    [SerializeField]
    private bool trackPosition = true;
    [SerializeField]
    private bool trackRotation = true;

    public bool debugOutput = false;

    public int Channel
    {
        get { return channel; }
        set
        {
            channel = value;
        }
    }

    public bool TrackPosition
    {
        get { return trackPosition; }
        set
        {
            trackPosition = value;
            StopCoroutine("Position");
            if (trackPosition && Application.isPlaying)
            {
                StartCoroutine("Position");
            }
        }
    }

    public bool TrackRotation
    {
        get { return trackRotation; }
        set
        {
            trackRotation = value;
            StopCoroutine("Rotation");
            if (trackRotation && Application.isPlaying)
            {
                StartCoroutine("Rotation");
            }
        }
    }

    private void Start()
    {
        //this gets rid of this object from non-head nodes...we only want this running on the machine that connects to VRPN...
        //this assumes a distributed type setup, where one machine connects to the tracking system and distributes information
        //to other machines...
        //some setups may try to connect each machine to vrpn...
        //in that case, we wouldn't want to destroy this object..
        if (System.Environment.MachineName != MasterTrackingData.HeadNodeMachineName)
        {
                Debug.Log("Removing tracker settings from " + gameObject.name + " on " + System.Environment.MachineName);
                Destroy(this);
                return;
        }

        if (trackPosition)
        {
            StartCoroutine("Position");
        }

        if (trackRotation)
        {
            StartCoroutine("Rotation");
        }
    }

    private IEnumerator Position()
    {
        while (true)
        {
            transform.localPosition = VRPN.vrpnTrackerPos(trackerAddress, channel);
            yield return null;
        }
    }

    private IEnumerator Rotation()
    {
        while (true)
        {
            transform.localRotation = VRPN.vrpnTrackerQuat(trackerAddress, channel);
            yield return null;
        }
    }
}
