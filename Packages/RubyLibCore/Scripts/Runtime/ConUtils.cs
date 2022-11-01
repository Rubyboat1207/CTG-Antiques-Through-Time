using System;

class ConUtils {
    public static void InterpretFromString(string input, Type t, ConVar cv) {
        if(t == typeof(int)) {
            int ret;
            if(int.TryParse(input, out ret)) {
                ((TypedConVar<int>) cv).value = ret;
                return;
            }
        }else if(t == typeof(float)){
            float ret = -1;
            if(float.TryParse(input, out ret)) {
                ((TypedConVar<float>)cv).value = ret;
                return;
            }
        }else if(t == typeof(double)){
            double ret = -1;
            if(double.TryParse(input, out ret)) {
                ((TypedConVar<double>)cv).value = ret;
                return;
            }
        }else if(t == typeof(string)){
            ((TypedConVar<string>)cv).value = input;
            return;
        }
        else if(t == typeof(bool)){
            if(input == "true" || input == "1") {
                ((TypedConVar<bool>)cv).value = true;
                return;
            }
            else if(input == "false" || input == "0") {
                ((TypedConVar<bool>)cv).value = false;
                return;
            }
        }else
        {
            throw new InvalidCastException($"The type \"{t.Name}\" is not a vaild ConVar type.");
        }
        throw new InvalidCastException($"The input \"{input}\" can not be resloved to a \"{t.Name}\".");
    }
}