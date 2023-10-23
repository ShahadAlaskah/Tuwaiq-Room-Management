dotnet publish -c Release

cd 'bin/Release'
FILE=$(ls -1 *.nupkg | tail -n 1)
echo $FILE

dotnet nuget push -s http://65.1.97.146:5555/v3/index.json -k MyApiSecretKeyPassword007 $FILE 