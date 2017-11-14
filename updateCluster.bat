rem Copy entire folder
setlocal

set OPT=/MIR /R:0 /W:10 /XA:SH
set PSEXEC=start \\visdc1\RealityDeck\Tools\win\psexec
set LOGIN=-u Administrator -p Vislab1234
set LOGINDOMAIN=-u VISDOM\rdadmin -p Vislab1234
set CMD=robocopy \\visdc1\RealityDeck\UniCAVE\RD-test-Jungle\RD-test-Jungle\final\ C:\temp\UniCAVE\final\ %OPT%

%PSEXEC% \\vis-rdhn %LOGIN% %CMD%
%PSEXEC% \\vis-rd1 %LOGIN% %CMD%
%PSEXEC% \\vis-rd2 %LOGIN% %CMD%
%PSEXEC% \\vis-rd3 %LOGIN% %CMD%
%PSEXEC% \\vis-rd4 %LOGIN% %CMD%
%PSEXEC% \\vis-rd5 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd6 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd7 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd8 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd9 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd10 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd11 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd12 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd13 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd14 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd15 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd16 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd17 %LOGIN% %CMD%
rem %PSEXEC% \\vis-rd18 %LOGIN% %CMD%
