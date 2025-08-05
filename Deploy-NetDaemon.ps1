dotnet publish .\daemonapp.csproj -o \\192.168.1.3\public\netdaemon -c release
ssh -t eugene@192.168.1.3 docker restart netdaemon
get-date