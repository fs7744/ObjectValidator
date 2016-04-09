"%cd%\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" -target:"%cd%\packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe" -targetargs:"%cd%\UnitTest\bin\Debug\UnitTest.dll" -filter:"+[ObjectValidator]*" -register:Path64 -output:"CodeCoverage.xml"
"%cd%\packages\ReportGenerator.2.4.4.0\tools\ReportGenerator.exe" -reports:"CodeCoverage.xml"  -targetdir:"%cd%\report"  
