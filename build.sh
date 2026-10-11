#!/bin/sh

if [ "$OS" = "Windows_NT" ] ; then
  BASE=$(cygpath "$WINDIR")
  CSC=$(ls -1d $BASE/Microsoft.NET/Framework/v*|sort -rn|head -1)/csc.exe
  FILES='src\*.cs'
else
  CSC=$(which csc)
  FILES='src/*.cs'
fi

echo "Compiling with $CSC"
$CSC /o /nologo /target:exe /out:inotifywait.exe $FILES && ls -al inotifywait.exe