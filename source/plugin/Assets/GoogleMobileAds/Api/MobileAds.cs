// Copyright (C) 2017 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

using GoogleMobileAds;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
    public class MobileAds
    {
        public static class Utils {
            // Returns the device's scale.
            public static float GetDeviceScale() {
                return Instance.client.GetDeviceScale();
            }

            // Returns the safe width for the current device.
            public static int GetDeviceSafeWidth() {
                return Instance.client.GetDeviceSafeWidth();
            }
        }

        private readonly IMobileAdsClient client = GetMobileAdsClient();

        private static MobileAds instance;

        public static MobileAds Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MobileAds();
                }
                return instance;
            }
        }

        public static void Initialize(string appId)
        {
            Instance.client.Initialize(appId);
            MobileAdsEventExecutor.Initialize();
        }

        public static void Initialize(Action<InitializationStatus> initCompleteAction)
        {
            Instance.client.Initialize((initStatusClient) => {
                if (initCompleteAction != null)
                {
                    initCompleteAction.Invoke(new InitializationStatus(initStatusClient));
                }
            });
            MobileAdsEventExecutor.Initialize();
        }

        public static void SetApplicationMuted(bool muted)
        {
            Instance.client.SetApplicationMuted(muted);
        }

        public static void SetApplicationVolume(float volume)
        {
            Instance.client.SetApplicationVolume(volume);
        }

        public static void SetiOSAppPauseOnBackground(bool pause)
        {
            Instance.client.SetiOSAppPauseOnBackground(pause);
        }

        private static IMobileAdsClient GetMobileAdsClient()
        {
          return GoogleMobileAdsClientFactory.MobileAdsInstance();
        }
    }
}
