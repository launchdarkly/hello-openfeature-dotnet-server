using System;
using System.Threading.Tasks;
using LaunchDarkly.OpenFeature.ServerProvider;
using LaunchDarkly.Sdk.Server;
using OpenFeature.Model;

namespace HelloOpenFeatureDotnetServer
{
    internal static class Hello
    {
        public static async Task Main(string[] args)
        {
            var sdkKey = Environment.GetEnvironmentVariable("LAUNCHDARKLY_SDK_KEY");
            var featureFlagKey = Environment.GetEnvironmentVariable("LAUNCHDARKLY_FLAG_KEY") ?? "sample-feature";

            if (string.IsNullOrEmpty(sdkKey))
            {
                Console.WriteLine("Please set an SDK key using the LAUNCHDARKLY_SDK_KEY environment variable.");
                Environment.Exit(1);
            }

            var config = Configuration.Builder(sdkKey).Build();

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

            var flagValue = await client.GetBooleanValueAsync(featureFlagKey, false, context);

            Console.WriteLine($"The {featureFlagKey} feature flag evaluates to {(flagValue ? "true" : "false")}");

            // Here we ensure that the SDK shuts down cleanly and has a chance to deliver analytics
            // events to LaunchDarkly before the program exits. If analytics events are not delivered,
            // the user properties and flag usage statistics will not appear on your dashboard. In a
            // normal long-running application, the SDK would continue running and events would be
            // delivered automatically in the background.
            await OpenFeature.Api.Instance.ShutdownAsync();
        }
    }
}
