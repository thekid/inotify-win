inotify-win
===========
A port of the **inotifywait** tool for Windows, see https://github.com/rvoicilas/inotify-tools

Compiling
=========
Run `make` in this directory. This will create the executable, `inotifywait.exe`.

Usage
=====
The command line arguments are similar to the original one's:

```sh
$ ./inotifywait.exe
Usage: inotifywait [options] path [...]

Options:
-r/--recursive:  Recursively watch all files and subdirectories inside path
-m/--monitor:    Keep running until killed (e.g. via Ctrl+C)
-q/--quiet:      Do not output information about actions
-e/--event list: Which events (create, modify, delete, move) to watch, comma-separated. Default: all
--format format: Format string for output.

Formats:
%e             : Event name
%f             : File name
%w             : Path name
%T             : Current date and time
```
