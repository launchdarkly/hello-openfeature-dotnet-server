# LaunchDarkly Sample OpenFeature .Net Server application

We've built a simple console that demonstrates how LaunchDarkly's OpenFeature provider works.

## Build instructions

This is a .NET Core application that can be built on any platform where .NET is available. It can be run either in Visual Studio, or from the command line if you have installed the .NET command-line tools.

1. Set the environment variable `LAUNCHDARKLY_SDK_KEY` to your LaunchDarkly SDK key. If there is an existing boolean feature flag in your LaunchDarkly project that you want to evaluate, set `LAUNCHDARKLY_FLAG_KEY` to the flag key; otherwise, a boolean flag of `sample-feature` will be assumed.

    ```bash
    export LAUNCHDARKLY_SDK_KEY="1234567890abcdef"
    export LAUNCHDARKLY_FLAG_KEY="my-boolean-flag"
    ```

2. If you are using Visual Studio, open `HelloDotNet.sln` and run the application. Or, to run from the command line, type the following command:

```
     dotnet run --project HelloOpenFeatureDotnet/HelloOpenFeatureDotnet.csproj --framework net6.0
```

You should see the message `"The <flag key> feature flag evaluates to <true/false>"`.
