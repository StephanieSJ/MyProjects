///scr_GetWeapons
TotalWeapons++;
WeaponList[0] = 'Fists';

var Guns;
Guns[4] = 0;
Guns[0] = 'Assault Rifle';
Guns[1] = 'Shotgun';
Guns[2] = 'Handgun';
Guns[3] = 'Sub-machine Gun';
Guns[4] = 'Light-machine Gun';

TotalWeapons += instance_number(obj_WeaponRack) * 3;
for (var i = 1; i < TotalWeapons; i++)
{
    WeaponList[i] = Guns[irandom(4)];
}

var MeleeWeapons;
MeleeWeapons[4] = 0;
MeleeWeapons[0] = 'Cricket Bat';
MeleeWeapons[1] = 'Baseball Bat';
MeleeWeapons[2] = 'Hockey Stick';
MeleeWeapons[3] = 'Tennis Racket';
MeleeWeapons[4] = 'Golf Club';

TotalWeapons += instance_number(obj_SportsEquipment) * 3;
for (var i = array_length_1d(WeaponList); i < TotalWeapons; i++)
{
    WeaponList[i] = MeleeWeapons[irandom(4)];
}
