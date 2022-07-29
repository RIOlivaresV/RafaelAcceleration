using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Hardware;
using Android.Runtime;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using RafaelAcceleration.Interfaces;

namespace RafaelAcceleration.Droid.Service
{
    public class StepCounter : Java.Lang.Object, IStepCounterRafa, ISensorEventListener
    {
        private int StepsCounter = 0;
        private SensorManager sensorManager;
        private Sensor sensor;
        private Context context;
        private static readonly int NOTIFICATION_ID = 100;
        private static readonly string CHANNEL_ID = "activity_notification";
        private int count = 0;

        public int Steps { get { return StepsCounter; } set { StepsCounter = value; } }

        public StepCounter()
        {
            sensorManager = null;
            context = Android.App.Application.Context;
        }

        public void InitService()
        {
            sensorManager = Android.App.Application.Context.GetSystemService(Context.SensorService) as SensorManager;
            if (sensorManager.GetDefaultSensor(SensorType.StepCounter)!=null)
            {
                sensor = sensorManager.GetDefaultSensor(SensorType.StepCounter);
                bool isRegistered = sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);
                //ActivityTransitionRequest request = new ActivityTransitionRequest(InitActivityTransitions());


                //var task = ActivityRecognition.GetClient(context)
                //    .RequestActivityTransitionUpdates(request, null);

                //Console.WriteLine(task.Result);
            }

        }


        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            Console.WriteLine("OnAccuracyChanged called");
        }
            
        public void OnSendNotification()
        {
            var builder = new Notification.Builder(context, CHANNEL_ID)
                .SetAutoCancel(true)
                .AddAction(null)
                .SetContentTitle("Activity notification")
                .SetNumber(count)
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_36dp) 
                .SetContentText("");

            //var ncm:NotificationManagerCompat = NotificationManagerCompat.From(context);
            //ncm.Notifi
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (sensor == e.Sensor)
            {
                StepsCounter = (int)e.Values[0];
            }
            Console.WriteLine(e.ToString());
        }

        public void StopService()
        {
            if (sensorManager.GetDefaultSensor(SensorType.StepCounter) != null)
            {
                sensorManager.UnregisterListener(this);
            }
            
        }

        public bool IsAvailable()
        {

            return sensorManager == null ? false : true;
        }

        private List<ActivityTransition> InitActivityTransitions()
        {
            List<ActivityTransition> transitions = new List<ActivityTransition>();
            transitions.Add(
        new ActivityTransition.Builder()
          .SetActivityType(DetectedActivity.InVehicle)
          .SetActivityTransition(ActivityTransition.ActivityTransitionEnter)
          .Build());

            transitions.Add(
                    new ActivityTransition.Builder()
                      .SetActivityType(DetectedActivity.InVehicle)
                      .SetActivityTransition(ActivityTransition.ActivityTransitionExit)
                      .Build());

            transitions.Add(
                    new ActivityTransition.Builder()
                      .SetActivityType(DetectedActivity.Walking)
                      .SetActivityTransition(ActivityTransition.ActivityTransitionExit)
                      .Build());

            transitions.Add(
                    new ActivityTransition.Builder()
                      .SetActivityType(DetectedActivity.OnBicycle)
                      .SetActivityTransition(ActivityTransition.ActivityTransitionExit)
                      .Build());

            transitions.Add(
                    new ActivityTransition.Builder()
                      .SetActivityType(DetectedActivity.Running)
                      .SetActivityTransition(ActivityTransition.ActivityTransitionExit)
                      .Build());

            transitions.Add(
                    new ActivityTransition.Builder()
                      .SetActivityType(DetectedActivity.OnFoot)
                      .SetActivityTransition(ActivityTransition.ActivityTransitionExit)
                      .Build());
            return transitions;
        }

    }
}
