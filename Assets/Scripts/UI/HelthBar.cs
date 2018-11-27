using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KingDOM.Platformer2D
{ 
    public class HelthBar : MonoBehaviour {


        public UnitData target = null;
        public Slider progress = null;
	
	    // Update is called once per frame
	    void Update () {
            if (progress && target) progress.value = target.Energy;
	    }
    }
}
