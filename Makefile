ifeq ($(OS),Windows_NT)
        BASE=$(shell cygpath "$(WINDIR)")
        CSC?=$(shell ls -1d $(BASE)/Microsoft.NET/Framework/v*|sort -rn|head -1)/csc.exe
else
        CSC?=csc
endif

inotifywait.exe: src/*.cs
        ifneq (,$(findstring CYGWIN,$(shell uname -s)))
		$(CSC) '/o' '/nologo' '/target:exe' '/out:$@' 'src\\*.cs'
        else
		$(CSC) //o //nologo //target:exe //out:$@ src\\*.cs
        endif

clean:
	-rm inotifywait.exe
