# inotify-win ChangeLog

## ?.?.? / ????-??-??

## 2.0.0 / 2026-10-11

Contributors: @thekid, @Keith-S-Thompson

* Added support for `-v` / `--version` command line switches
* Added support for `-h` command line switch as an alias for `-?`
* Refactored format string parsing, addressing issue reported in issue #44:
  *First character of --format argument is expanded*.
* Fixed issue #43: File path is displayed incorrectly

## 1.7.0 / 2023-05-13

Contributors: @thekid, @pasha-zzz

* Optimize during compilation
* Merge pull request #39: Implement `--include[i]` with regexes

## 1.6.0 / 2023-05-02

Contributors: @thekid

* Yield better error messages for unknown options or non-existant paths

## 1.5.0 / 2020-12-06

Contributors: @thekid, @creallfluhart

* Merge pull request #32: Stop Monitoring on Ctrl+C rather than EOF

## 1.4.1 / 2017-07-03

Contributors: @thekid, @Aldecar

* Merge pull request #27: Missing semicolon for processor function

## 1.4.0 / 2017-06-15

Contributors: @thekid, @yeerkkiller1

* Merge pull request #26: Allow filenames as well as directories

## 1.3.2 / 2016-09-12

Contributors: @thekid, @seanf, @PeterMosmans

* Fix issue #23: Allow multiple -e arguments
* Merge pull request #22: disable path mangling on MSYS2

## 1.3.1 / 2016-06-05

Contributors: @thekid, @josevalim

* Fix issue #19: Use background threads for polling standard input

## 1.2.0 / 2015-03-11

Contributors: @thekid, @josevalim

* Merge pull request #18: Exit when STDIN shuts down

## 1.1.0 / 2015-01-17

Contributors: @thekid, @LHCGreg

* Merge pull request #16: Use Created, Changed, etc events instead of WaitForChanged().

## 1.0.0 / 2014-07-17

Contributors: @thekid, @lehnerpat, @TheGleM

* Adjust path and filenames to match original, see #12
* Append "ISDIR" to type when argument is a directory
* Always use full path while matching excludes
* Use `MOVE*D*` instead of *MOVE*
* Fix issue #14: ReleaseMutex when excluded files match

## 0.5.0 / 2014-07-05

Contributors: @thekid, @nevik

* Merge pull request #10: Support '--<option>=<value>' form for long options

## 0.4.1 / 2014-05-23

Contributors: @thekid

* Add missing --exclude[i] options to usage
* Add howto manual compile inotifywait.exe

## 0.4.0 / 2014-05-09

Contributors: @thekid, @hdijkema

* Implement --exclude and --excludei options, see #6

## 0.3.0 / 2014-05-09

Contributors: @thekid, @ashi009

* Merge pull request #3: Added mutex to put outputs in order, and avoid race condition

## 0.2.0 / 2014-05-09

Contributors: @thekid, @ashi009

* Changed Changed default output format to `watched_filename EVENT_NAMES event_filename`.
* Merge pull request #2: Support watching multiple paths, compact short-hands in arguments

## 0.1.0 / 2012-06-17

Contributors: @thekid

* Fix parsing --event arg
* Include what is monitored (modify,create,delete,move,all) in verbose output