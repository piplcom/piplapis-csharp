#!/bin/bash

dotnet restore ./UnitTests/UnitTests.csproj
dotnet publish -c Release -o ./publish ./UnitTests/UnitTests.csproj