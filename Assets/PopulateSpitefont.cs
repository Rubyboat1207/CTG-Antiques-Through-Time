using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateSpitefont
{
    public static string populateFont(string preString) {
        bool italics = false;
        string populated = "";
        var ca = preString.ToCharArray();
        for(int i = 0; i < ca.Length; i++) {
            var ch = ca[i];
            if (ch == ' ') {
                populated += " ";
            }
            else if(ch == '.') {
                populated += "<sprite name=dot>";
            }
            else if(ch == '{') {
                italics = true;
            }
            else if(ch == '}') {
                italics = false;
            }else if(ch == '(' && ca[i + 1] == ':') {
                populated += "<sprite name=(:>";
                i++;
            }
            else if(ch == ')' && ca[i + 1] == ':') {
                populated += "<sprite name=):>";
                i++;
            }
            else if(ch == ')' && ca[i + 1] == ':' && ca[i + 2] == '<') {
                populated += "<sprite name=):|>";
                i += 2;
            }
            else if(ch == '(' && ca[i + 1] == ':' && ca[i + 2] == '<') {
                populated += "<sprite name=(:|>";
                i += 2;
            }
            else if(ch == '(' && ca[i + 1] == ';') {
                populated += "<sprite name=(;>";
                i++;
            }
            else if(ch == ')' && ca[i + 1] == ';') {
                populated += "<sprite name=);>";
                i++;
            }
            else if(italics) {
                populated += $"<sprite name=i-{ch}>";
            }else{
                Debug.Log("" + ch);
                populated += $"<sprite name={ch}>";
            }
        }
        return populated;
    }
}
