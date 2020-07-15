set imageName=mongo
@echo -+ stop %imageName% +-
set containerName=my-mongo
docker stop %containerName%

@echo -+ run %imageName% +-
set networkName=tbs-nat
:docker run --rm -d -p 27017:27017 --name %containerName% %imageName%
docker run --rm -d --network %networkName% --name %containerName% %imageName%

:@echo -+ get %imageName% ip +-
:set ip=docker inspect -f "{{ .NetworkSettings.Networks.bridge.IPAddress }}" %containerName%
:@echo ip=%ip%

:set tbsImageName=mihail1orlov/tbs
set tbsImageName=tbs
@echo -+ start tbs +-
docker run --rm -it --network %networkName% --name tbs %tbsImageName%