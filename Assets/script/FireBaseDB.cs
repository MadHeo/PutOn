using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class FireBaseDB : MonoBehaviour
{
    public string DBurl = "https://puton-b6e8a-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    void start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        WriteDB();
        ReadDB();
    }

    public void WriteDB()
    {

    }

    public void ReadDB()
    {

    }

    public class GPSdata
    {
        public string name = "";
        public float latitude_data = 0;
        public float longitude_data = 0;
        public float altitude_data = 0;

        public GPSdata(string Name, float Lat, float Lon, float ALT)
        {
            name = Name;
            latitude_data = Lat;
            longitude_data = Lon;
            altitude_data = ALT;
        }
        
    }

    //public class danny
    //{
    //    public string name = "";
    }
}
