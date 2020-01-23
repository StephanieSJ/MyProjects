//scr_UpdateAmmo
var AmmoChange = 0;

for (var i = 0; i < instance_number(obj_BulletPrinter); i++)
{
    if (instance_find(obj_BulletPrinter, i).Active)
    {
        AmmoChange += 10;
    }
}

return AmmoChange;
