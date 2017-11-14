setlocal 
 set DEMOPATH=c:\temp\UniCAVE\RD-test-Jungle\RD-test-Jungle
rem set DEMOPATH=y:\UniCAVE
rem set DEMOEXE=TryRD-new.exe
rem set DEMOEXE=TryRD-2-nodes.exe
set DEMOEXE=RD-FrontWall.exe
set LOGIN=-u Administrator -p Vislab1234
set PSEXEC=start \\visdc1\RealityDeck\Tools\win\psexec

set START1=start /MIN psexec -s -i 1 -w %DEMOPATH% %LOGIN%
rem %START1% \\vis-rdhn %DEMOPATH%\%DEMOEXE%
%START1% \\vis-rd1  %DEMOPATH%\%DEMOEXE%
%START1% \\vis-rd2  %DEMOPATH%\%DEMOEXE%
%START1% \\vis-rd3  %DEMOPATH%\%DEMOEXE%
%START1% \\vis-rd4  %DEMOPATH%\%DEMOEXE%
%START1% \\vis-rd5  %DEMOPATH%\%DEMOEXE%

rem Master locally
rem start psexec -w %DEMOPATH% %LOGIN% \\vis-rdhn %DEMOPATH%\%DEMOEXE%
rem start psexec -w %DEMOPATH% %LOGIN% \\vis-rdhn %DEMOPATH%\%DEMOEXE% 
rem start psexec -w %DEMOPATH% %LOGIN% \\vis-rd1 %DEMOPATH%\%DEMOEXE%
rem start psexec -w %DEMOPATH% %LOGIN% \\vis-rdhn %DEMOPATH%\%DEMOEXE%