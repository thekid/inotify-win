ifeq ($(OS),Windows_NT)
        BASE=$(shell cygpath "$(WINDIR)")
        CSC?=$(BASE)/Microsoft.NET/Framework/v3.5/csc
else
        CSC?=csc
endif
CSC_OPT=/nologo /target:exe

inotify-wait.exe: src/main/csharp/*.cs
	$(CSC) $(CSC_OPT) /out:$@ src\\main\\csharp\\*.cs

clean:
	-rm inotify-wait.exe