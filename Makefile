ifeq ($(OS),Windows_NT)
        BASE=$(shell cygpath "$(WINDIR)")
        CSC?=$(shell ls -1d $(BASE)/Microsoft.NET/Framework/v*|sort -rn|head -1)/csc.exe
else
        CSC?=csc
endif

inotifywait.exe: src/*.cs
	$(CSC) /nologo /target:exe /out:$@ src\\*.cs

clean:
	-rm inotifywait.exe
