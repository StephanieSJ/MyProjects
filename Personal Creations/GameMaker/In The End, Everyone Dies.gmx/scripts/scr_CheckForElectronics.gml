///scr_CheckForElectronics(Instance)
var Instance = argument0;

with (obj_Electronics)
{
    for (var i = 0; i < NumberElectronics; i++)
    {
        if (Electronics[i] == Instance)
        {
            NumberElectronics--;
            for (var j = i; j < NumberElectronics; j++)
            {
                Electronics[j] = Electronics[j + 1];
            }
            break;
        }
    }
}
