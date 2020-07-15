set host=mihail1orlov
set name=tbs

@echo -+ start build +-
docker build --tag %host%/%name%:sdk .
set /p skip="Skip the push step (y/n): "
IF "%skip%"=="n" (docker push %host%/%name%)