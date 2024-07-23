#!/bin/sh
#apt-get install python3 brotli -y
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 8.0 -InstallDir ./dotnet
./dotnet/dotnet --version
./dotnet/dotnet workload restore
./dotnet/dotnet restore
./dotnet/dotnet publish Examples/Nodify.Shapes.Web -c Release -o output
#find AppBundle/ -type f -exec brotli {} \;