///scr_DestroyObjectMenu(objects, number of objects)
var ObjectsToDestroy = argument0;
var NumObjects = argument1;

for (var i = 0; i < NumObjects; i++)
{
    if (instance_exists(ObjectsToDestroy[i]))
    {
        with (ObjectsToDestroy[i])
        {
            instance_destroy();
        }
    }
}
