# inotify-win

[![Build status on GitHub](https://github.com/thekid/inotify-win/actions/workflows/build/badge.svg)](https://github.com/thekid/inotify-win/actions/workflows/build)

A port of the **inotifywait** tool for Windows, see https://github.com/rvoicilas/inotify-tools

## Downloads

There are two ways to download the current release binary:

* From the releases page: https://github.com/thekid/inotify-win/releases
* Via the community-contributed *WinGet* package: https://github.com/PolarGoose/inotifywait-for-Windows

## Usage
The command line arguments are similar to the original one's:

```sh
$ inotifywait.exe
Usage: inotifywait [options] path [...]

Options:
-h/--help:       Shows this usage
-v/--version:    Shows version number
-r/--recursive:  Recursively watch all files and subdirectories inside path
-m/--monitor:    Keep running until killed (e.g. via Ctrl+C)
-q/--quiet:      Do not output information about actions
-e/--event list: Which events (create, modify, delete, move) to watch, comma-separated. Default: all
--format format: Format string for output.
--exclude:       Do not process any events whose filename matches the specified regex
--excludei:      Ditto, case-insensitive
--include:       Only process events whose filename matches the specified regex
--includei:      Ditto, case-insensitive

Formats:
%e             : Event name
%f             : File name
%w             : Path name
%T             : Current date and time
%%             : Percent sign
```

## Contributing
Pull requests welcome ☺️

### Compiling
If you have Cygwin installed, just run `sh build.sh` in this directory. This will create the executable, `inotifywait.exe`.

Manual complilation goes as follows:

```sh
$ %WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe /o /t:exe /out:inotifywait.exe src\*.cs
```

### Known issues
When moving files, not all events are reported consistently with the original. See [issue #7](https://github.com/thekid/inotify-win/issues/7) for an explanation.
