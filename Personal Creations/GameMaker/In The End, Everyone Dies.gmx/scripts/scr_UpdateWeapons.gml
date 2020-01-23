///scr_UpdateWeapons(Weapon)
var WeaponToRemove = argument0;

if (WeaponToRemove == 'Fists')
{
    if (global.SurvivorSelected.Weapon == 'Fists')
    {
        exit;
    }
    else
    {
        obj_Weapons.WeaponList[obj_Weapons.TotalWeapons] = global.SurvivorSelected.Weapon;
        obj_Weapons.TotalWeapons++;
        global.SurvivorSelected.Weapon = WeaponToRemove;
    }
}
else
{
    if (global.SurvivorSelected.Weapon == 'Fists')
    {
        global.SurvivorSelected.Weapon = WeaponToRemove;
    }
    else
    {
        obj_Weapons.WeaponList[obj_Weapons.TotalWeapons] = global.SurvivorSelected.Weapon;
        obj_Weapons.TotalWeapons++;
        global.SurvivorSelected.Weapon = WeaponToRemove;
    }
    
    for (var i = 0; i < obj_Weapons.TotalWeapons; i++)
    {
        if (obj_Weapons.WeaponList[i] == WeaponToRemove)
        {
            obj_Weapons.TotalWeapons--;
            for (var j = i; j < obj_Weapons.TotalWeapons; j++)
            {
                obj_Weapons.WeaponList[j] = obj_Weapons.WeaponList[j + 1];
            }
            break;
        }
    }
}
