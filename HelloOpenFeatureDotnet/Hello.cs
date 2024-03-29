﻿using System;
using System.Threading.Tasks;
using LaunchDarkly.OpenFeature.ServerProvider;
using LaunchDarkly.Sdk.Server;
using OpenFeature.Model;

namespace HelloOpenFeatureDotnetServer
{
    internal static class Hello
    {
        // Set SdkKey to your LaunchDarkly SDK key.
        private const string SdkKey = "";

        // Set FeatureFlagKey to the feature flag key you want to evaluate.
        private const string FeatureFlagKey = "my-boolean-flag";

        public static async Task Main(string[] args)
        {
            if (string.IsNullOrEmpty(SdkKey))
            {
                Console.WriteLine("Please edit Hello.cs to set SdkKey to your LaunchDarkly SDK key first");
                Environment.Exit(1);
            }

            var config = Configuration.Builder(SdkKey)
                // Allow for asynchronous initialization, the SetProviderAsync call will return when initialization is
                // complete.
                .StartWaitTime(TimeSpan.Zero)
                .Build();

            var provider = new Provider(config);

            await OpenFeature.Api.Instance.SetProviderAsync(provider);

            var client = OpenFeature.Api.Instance.GetClient();

            // Set up the user properties. This user should appear on your LaunchDarkly users dashboard
            // soon after you run the demo.
            // Remember when using OpenFeature to use `targetingKey` instead of `key`.
            var context = EvaluationContext.Builder()
                .Set("kind", "user")
                .Set("targetingKey", "example-user-key")
                .Set("name", "Sandy")
                .Build();

            var flagValue = await client.GetBooleanValue(FeatureFlagKey, false, context);

            Console.WriteLine($"Feature flag '{FeatureFlagKey}' is {flagValue} for this user");

            // Here we ensure that the SDK shuts down cleanly and has a chance to deliver analytics
            // events to LaunchDarkly before the program exits. If analytics events are not delivered,
            // the user properties and flag usage statistics will not appear on your dashboard. In a
            // normal long-running application, the SDK would continue running and events would be
            // delivered automatically in the background.
            await OpenFeature.Api.Instance.Shutdown();
        }
    }
}
