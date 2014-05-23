ifeq ($(OS),Windows_NT)
        BASE=$(shell cygpath "$(WINDIR)")
        CSC?=$(shell ls -1d $(BASE)/Microsoft.NET/Framework/v*|sort -rn|head -1)/csc.exe
else
        CSC?=csc
endif
CSC_OPT=/nologo /target:exe

inotifywait.exe: src/*.cs
	$(CSC) $(CSC_OPT) /out:$@ src\\*.cs

clean:
	-rm inotifywait.exe