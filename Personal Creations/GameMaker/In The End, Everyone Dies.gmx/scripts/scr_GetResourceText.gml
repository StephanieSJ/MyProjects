///scr_GetResourceText(Resource)
var Resource = argument0;
var Text = '';

if (Resource > 999999)
{
    Text = '999999';
}
else if (Resource > 99999)
{
    Text = string(Resource);
}
else if (Resource > 9999)
{
    Text = '0' + string(Resource);
}
else if (Resource > 999)
{
    Text = '00' + string(Resource);
}
else if (Resource > 99)
{
    Text = '000' + string(Resource);
}
else if (Resource > 9)
{
    Text = '0000' + string(Resource);
}
else
{
    Text = '00000' + string(Resource);
}

return Text;
