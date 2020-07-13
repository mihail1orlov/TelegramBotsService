set host=mihail1orlov
set name=tbs
docker build --tag %host%/%name%:sdk .
set /p skip="Skip push step (y/n): "
IF "%skip%"=="y" (docker push %host%/%name%)