using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Xamarin.Essentials;

namespace preference
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText editText;
        Button button;
        Switch s1;
        SeekBar bar;
        const string Name = "name";
        const string StreamSelection = "streamselection";
        const string Volume = "volume";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            UIReferences();
            UIClickEvents();
            ShowUserPreferencesIfAlreadySaved();
        }

        private void ShowUserPreferencesIfAlreadySaved()
        {
            if (Preferences.ContainsKey(Name)) {
                editText.Text = Preferences.Get(Name, string.Empty); 
            }
            if (Preferences.ContainsKey(StreamSelection))
            {
                s1.Checked = Preferences.Get(StreamSelection, defaultValue: false);
            }

            int volume = Preferences.Get(Volume,defaultValue: 0);
            bar.SetProgress(volume, animate: true);

        }
       

        private void UIClickEvents()
        {
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string username = editText.Text;
            bool shouldStreamOnWifi = s1.Checked;
            int volume = bar.Progress;

            Preferences.Set(Name, username);
            Preferences.Set(StreamSelection, shouldStreamOnWifi);
            Preferences.Set(Volume, volume);
            Toast.MakeText(this, text:"User preferences saved successfully", ToastLength.Short).Show();

        }

        private void UIReferences()
        {
            editText = FindViewById<EditText>(Resource.Id.editText1);
            button = FindViewById<Button>(Resource.Id.button1);
            s1 = FindViewById<Switch>(Resource.Id.switch1);
            bar = FindViewById<SeekBar>(Resource.Id.seekBar1);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}