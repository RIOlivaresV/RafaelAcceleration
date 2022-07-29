using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using RafaelAcceleration.Droid.Service;
using Android.Hardware;
using Android.Content;
using Xamarin.Forms;
using RafaelAcceleration.Interfaces;
using AndroidX.Core.Content;
using Android;
using AndroidX.Core.App;

namespace RafaelAcceleration.Droid
{
    [Activity(Label = "RafaelAcceleration", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private SensorManager sensorManager;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.DependencyService.Register<StepCounter>();
            LoadApplication(new App());
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ActivityRecognition) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ActivityRecognition }, (int)Permission.Granted);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
