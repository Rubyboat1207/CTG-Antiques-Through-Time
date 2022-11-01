using System;

class ConUtils {
    public static dynamic InterpretFromString(string input, Type t) {
        if(t == typeof(int)) {
            int ret = -1;
            if(int.TryParse(input, out ret)) {
                return ret;
            }
        }else if(t == typeof(float)){
            float ret = -1;
            if(float.TryParse(input, out ret)) {
                return ret;
            }
        }else if(t == typeof(double)){
            double ret = -1;
            if(double.TryParse(input, out ret)) {
                return ret;
            }
        }else if(t == typeof(string)){
            return input;
        }else if(t == typeof(bool)){
            if(input == "true") {
                return true;
            }else if(input == "false") {
                return false;
            }
        }
        throw new InvalidCastException($"The input \"{input}\" can not be resloved to a \"{t.Name}\".");
    }
}