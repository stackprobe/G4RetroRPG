IF NOT EXIST AutoRelease.bat GOTO END
CLS
rem リリースして qrum します。★要バージョン入力
PAUSE

CALL newcsrr

CALL ff
cx **

CD /D %~dp0.

IF NOT EXIST AutoRelease.bat GOTO END

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\* S:\リリース物\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
