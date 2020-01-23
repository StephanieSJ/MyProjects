///scr_RandomizeArray(array)
var Array = argument0;

for (var i = 0; i < array_length_1d(Array); i++)
{
    var NewPos = irandom(array_length_1d(Array) - 1);
    var Temp = Array[NewPos];
    Array[NewPos] = Array[i];
    Array[i] = Temp;
}

return Array;
