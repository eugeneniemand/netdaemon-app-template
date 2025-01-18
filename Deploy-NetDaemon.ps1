dotnet publish .\daemonapp.csproj -o \\192.168.1.3\sambashare\ha\netdaemon -c release
ssh -t admin@192.168.1.3 docker restart netdaemon