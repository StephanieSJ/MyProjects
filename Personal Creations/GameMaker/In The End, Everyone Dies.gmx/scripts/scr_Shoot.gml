///scr_Shoot(Spread, Speed, Direction, Distance, Damage, Type)
var Spread = argument0;
var NewSpeed = argument1;
var OldDirection = argument2;
var NewDistance = argument3;
var NewDamage = argument4;
var BulletType = argument5;

var NewDirection = OldDirection + ((irandom(2) - 1) * (irandom(Spread) + 1));

scr_LoudNoise(x, y);

var Bullet = instance_create(x, y, obj_Bullet);
with (Bullet)
{
    Speed = NewSpeed;
    Direction = NewDirection;
    Distance = NewDistance;
    Damage = NewDamage;
    Type = BulletType;
}
