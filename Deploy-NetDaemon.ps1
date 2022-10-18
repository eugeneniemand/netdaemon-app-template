dotnet publish .\daemonapp.csproj -o \\192.168.1.3\ha\netdaemon -c release
ssh -t pi@192.168.1.3 docker restart netdaemon