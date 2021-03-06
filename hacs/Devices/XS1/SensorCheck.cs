using System;
using System.Collections.Generic;
using System.Threading;

namespace hacs
{	
	public class SensorCheck
	{
		private bool _Shutdown = false;
		private Dictionary<string,DateTime> SensorCache = new Dictionary<string, DateTime>();
        private ConsoleOutputLogger ConsoleOutputLogger;

		/// <summary>
		/// Initializes a new instance of the <see cref="xs1_data_logging.SensorCheck"/> class.
		/// </summary>
		public SensorCheck (ConsoleOutputLogger Logger)
		{
            ConsoleOutputLogger = Logger;
		}

		/// <summary>
		/// Updates the sensor.
		/// </summary>
		/// <param name='SensorName'>
		/// Sensor name.
		/// </param>
		public void UpdateSensor(string SensorName)
		{
            lock(SensorCache)
            {
			    if (SensorCache.ContainsKey(SensorName))
			    {
				    SensorCache[SensorName] = DateTime.Now;
			    }
			    else
			    {
				    SensorCache.Add(SensorName,DateTime.Now);
			    }
            }
		}

		public void Shutdown()
		{
			_Shutdown = true;
		}
			
		/// <summary>
		/// Run this instance.
		/// </summary>
        public void Run()
        {
            ConsoleOutputLogger.WriteLine("Starting Automated Sensor Check.");
			TimeSpan temp = new TimeSpan();
            while (!_Shutdown)
            {
                try
                {
				    // check...
				    foreach(string SensorName in SensorCache.Keys)
				    {
					    temp = new TimeSpan(DateTime.Now.Ticks-SensorCache[SensorName].Ticks);

					    if (temp.TotalMinutes > hacs.Properties.Settings.Default.AutomatedSensorCheck_ResponseTimeWindow)
                        {
                            if (!SensorCheckIgnoreConfiguration.SensorCheckIgnoreList.Contains(SensorName))
			                    ConsoleOutputLogger.WriteLine("#WARNING# An outdated sensor was detected: "+SensorName);
                        }
				    }
                }
                catch(Exception)
                {
                }

				Thread.Sleep(Properties.Settings.Default.SensorCheckIntervalSec*1000); // just check every 6 seconds...
			}
		}
	}
}

