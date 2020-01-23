///scr_SaveOptions
if (file_exists('Save.sav'))
{
    file_delete('Save.sav');
}

ini_open('Save.sav');
var MusicVolume = global.MusicVolume;
var SoundVolume = global.SoundVolume;
var Fullscreen = global.Fullscreen;
var Music = global.Music;
ini_write_real('Options', 'MusicVolume', MusicVolume);
ini_write_real('Options', 'SoundVolume', SoundVolume);
ini_write_real('Options', 'Fullscreen', Fullscreen);
ini_write_real('Options', 'Music', Music);
ini_close();
