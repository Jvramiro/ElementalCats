using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataController{
    public class Data{

        private static string keyString_Points = "ECPoints";
        private static string keyString_Progress = "ECProgress";

        public static void AddPoints(int quantity){
            int getPointsData = PlayerPrefs.GetInt(keyString_Points);
            PlayerPrefs.SetInt(keyString_Points, getPointsData + quantity);
        }
        public static void RemovePoints(int quantity){
            int getPointsData = PlayerPrefs.GetInt(keyString_Points);
            PlayerPrefs.SetInt(keyString_Points, getPointsData - quantity);
        }
        public static void SetPoints(int quantity){
            PlayerPrefs.SetInt(keyString_Points, quantity);
        }
        public static void ResetPoints(){
            PlayerPrefs.SetInt(keyString_Points, 0);
        }
        public static int GetPoints(){
            return PlayerPrefs.GetInt(keyString_Points);
        }

        public static void SetProgress(int progressStage){
            PlayerPrefs.SetInt(keyString_Progress, progressStage);
        }
        public static void ResetProgress(){
            PlayerPrefs.SetInt(keyString_Progress, 0);
        }
        public static int GetProgress(){
            return PlayerPrefs.GetInt(keyString_Progress);
        }

        public static void ResetData(){
            ResetPoints();
            ResetProgress();
        }
    }
}
