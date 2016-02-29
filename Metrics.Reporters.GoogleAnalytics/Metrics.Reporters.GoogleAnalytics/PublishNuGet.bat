del *.nupkg

nuget.exe pack .\Metrics.Reporters.GoogleAnalytics.csproj -Prop Configuration=Release -IncludeReferencedProjects

nuget.exe setApiKey 79ccf130-386b-41b8-a8e2-6ce17f0459ca

nuget.exe push ".\*.nupkg"
