dotnet publish .\daemonapp.csproj -o \\10.10.40.14\public\netdaemon -c release
ssh -t eugene@10.10.40.14 docker restart netdaemon
get-date
# username eugene pw short strong