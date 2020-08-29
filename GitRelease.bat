@rem このフォルダのローカル名は Repo または x99999999_Repo (Repo=リリース先のリポジトリ名) であること。

IF NOT EXIST .\GitRelease.bat GOTO END
CALL qq
C:\Factory\SubTools\GitFactory.exe /ow . C:\huge\GitHub\G4RetroRPG
rem CALL C:\var\go.bat
:END
